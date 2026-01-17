using FoodChallenge.Common.Entities;
using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Infrastructure.Clients.Payments.Clients;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;
using FoodChallenge.Order.Adapter.Gateways;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Adapter.Presenters;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;
using FoodChallenge.Order.Application.Pedidos.Models.Responses;
using FoodChallenge.Order.Application.Pedidos.UseCases;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Adapter.Controllers;

public class PedidoAppController(ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IClienteRepository clienteDataSource,
    IPedidoRepository pedidoDataSource,
    IProdutoRepository produtoDataSource,
    IProdutoImagemRepository produtoImagemDataSource,
    IPaymentsClient paymentsClient)
{
    public async Task<Resposta> ObterPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new ObtemPedidoUseCase(pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }

    public async Task<Resposta> CadastrarAsync(CadastrarPedidoRequest request, CancellationToken cancellationToken)
    {
        var clienteGateway = new ClienteGateway(clienteDataSource);
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var pagamentoGateway = new PagamentoGateway(paymentsClient);
        var produtoGateway = new ProdutoGateway(produtoDataSource, produtoImagemDataSource);
        var useCase = new CadastraPedidoUseCase(validationContext, unitOfWork, clienteGateway, pedidoGateway, produtoGateway, pagamentoGateway);

        var pedidoItens = request?.Itens?.Select(PedidoItemMapper.ToDomain);
        var pedidoRetorno = await useCase.ExecutarAsync(request?.Cpf, pedidoItens, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedidoRetorno));
    }

    public async Task<Resposta> FinalizarPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new AtualizaStatusPedidoUseCase(validationContext, unitOfWork, pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, PedidoStatus.Finalizado, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }

    public async Task<Resposta> ConfirmarPagamentoPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new AtualizaStatusPedidoUseCase(validationContext, unitOfWork, pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, PedidoStatus.NaFila, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }

    public async Task<Resposta> IniciarPreparacaoPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new AtualizaStatusPedidoUseCase(validationContext, unitOfWork, pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, PedidoStatus.EmPreparacao, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }

    public async Task<Resposta> PermitirRetiradaPedidoAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var useCase = new AtualizaStatusPedidoUseCase(validationContext, unitOfWork, pedidoGateway);

        var pedido = await useCase.ExecutarAsync(idPedido, PedidoStatus.AguardandoRetirada, cancellationToken);
        return Resposta<PedidoResponse>.ComSucesso(PedidoPresenter.ToResponse(pedido));
    }
}
