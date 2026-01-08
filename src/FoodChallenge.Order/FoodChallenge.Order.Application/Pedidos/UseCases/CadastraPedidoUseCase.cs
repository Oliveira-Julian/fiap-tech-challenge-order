using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Clientes;
using FoodChallenge.Order.Application.Pagamentos;
using FoodChallenge.Order.Application.Pedidos.Interfaces;
using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Domain.Clientes;
using FoodChallenge.Order.Domain.Clientes.ValueObjects;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Pedidos;
using Serilog;

namespace FoodChallenge.Order.Application.Pedidos.UseCases;

public class CadastraPedidoUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IClienteGateway clienteGateway,
    IPedidoGateway pedidoGateway,
    IPagamentoGateway pagamentoGateway,
    IProdutoGateway produtoGateway) : ICadastraPedidoUseCase
{
    private readonly ILogger logger = Log.ForContext<CadastraPedidoUseCase>();

    public async Task<Pedido> ExecutarAsync(string cpf, IEnumerable<PedidoItem> itens, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(CadastraPedidoUseCase), nameof(ExecutarAsync));

        try
        {
            var idProdutos = itens?.Select(i => i.IdProduto.Value);

            var cliente = await ObterOuCadastrarPorCpfAsync(cpf, cancellationToken);

            var pedido = new Pedido();
            pedido.Cadastrar(cliente?.Id);
            await AtualizaItensPedido(pedido, itens, cancellationToken);

            validationContext.AddValidations(pedido, new CadastraPedidoSpecification(idProdutos));

            if (validationContext.HasValidations)
                return default;

            pedido.AtualizarValorTotal();

            unitOfWork.BeginTransaction();
            var pedidoCadastrado = await pedidoGateway.CadastrarPedidoAsync(pedido, cancellationToken);
            var pagamento = await pagamentoGateway.CadastrarPedidoMercadoPagoAsync(pedidoCadastrado, cancellationToken);
            await pagamentoGateway.AdicionarPagamentoAsync(pagamento, cancellationToken);
            await unitOfWork.CommitAsync();

            pedidoCadastrado = await pedidoGateway.ObterPedidoComRelacionamentosAsync(pedidoCadastrado.Id.Value, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(CadastraPedidoUseCase), nameof(ExecutarAsync), pedidoCadastrado);

            return pedidoCadastrado;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(CadastraPedidoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }

    private async Task AtualizaItensPedido(Pedido pedido,
        IEnumerable<PedidoItem> itens,
        CancellationToken cancellationToken)
    {
        if (itens is null || !itens.Any())
            return;

        var idProdutos = itens.Select(i => i.IdProduto);

        var produtos = await produtoGateway.ObterProdutosPorIdsAsync(idProdutos, cancellationToken, true);
        var produtoDict = produtos.ToDictionary(p => p.Id);

        var pedidoItens = itens
            .Select(i =>
                produtoDict.TryGetValue(i.IdProduto, out var produto)
                    ? i.AtualizarValor(produto.Preco)
                    : null)
            .Where(p => p != null)
            .ToList();

        pedido.AtualizarItens(pedidoItens);
    }

    private async Task<Cliente> ObterOuCadastrarPorCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var cpfValueObject = new Cpf(cpf);

        if (string.IsNullOrWhiteSpace(cpf))
            return await clienteGateway.ObterClientePadraoAsync(cancellationToken);

        var cliente = await clienteGateway.ObterPorCpfAsync(cpfValueObject, cancellationToken);

        if (cliente is null)
        {
            cliente = new Cliente(cpf);

            unitOfWork.BeginTransaction();
            cliente = await clienteGateway.CadastrarClienteAsync(cliente, cancellationToken);
            await unitOfWork.CommitAsync();
        }

        return cliente;
    }
}
