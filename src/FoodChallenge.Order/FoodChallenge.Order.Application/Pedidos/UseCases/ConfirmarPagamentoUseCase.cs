using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos.Interfaces;
using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Application.Preparos;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.Domain.Preparos;
using Serilog;

namespace FoodChallenge.Order.Application.Pedidos.UseCases;

public class ConfirmarPagamentoUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IPedidoGateway pedidoGateway,
    IPreparoGateway ordemPedidoGateway) : IConfirmarPagamentoUseCase
{
    private readonly ILogger logger = Log.ForContext<FinalizaPedidoUseCase>();

    public async Task<Pedido> ExecutarAsync(Guid idPedido, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(FinalizaPedidoUseCase), nameof(ExecutarAsync));

        try
        {
            var pedido = await pedidoGateway.ObterPedidoComRelacionamentosAsync(idPedido, cancellationToken);
            if (pedido is null)
            {
                validationContext.AddValidation(string.Format(Textos.NaoEncontrado, nameof(Pedido)));
                return default;
            }

            validationContext.AddValidations(pedido, new AtualizaStatusPedidoSpecification(PedidoStatus.Finalizado));

            if (validationContext.HasValidations)
                return default;

            var preparoCadastradoSucesso = await CadastrarPreparoAsync(pedido, cancellationToken);

            if (!preparoCadastradoSucesso)
            {
                validationContext.AddValidation(Textos.PreparoNaoIniciado);
                return default;
            }


            pedido.AtualizarStatusPago();

            unitOfWork.BeginTransaction();
            pedidoGateway.AtualizarPedido(pedido);
            await unitOfWork.CommitAsync();

            var pedidoAtualizado = await pedidoGateway.ObterPedidoAsync(pedido.Id.Value, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(FinalizaPedidoUseCase), nameof(ExecutarAsync), pedidoAtualizado);

            return pedidoAtualizado;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(FinalizaPedidoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }

    private async Task<bool> CadastrarPreparoAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        var ordemPedido = await ordemPedidoGateway.CadastrarAsync(pedido, cancellationToken);

        if (!ordemPedido.Id.HasValue)
        {
            validationContext.AddValidation(Textos.PreparoNaoIniciado);
            return false;
        }

        return true;
    }
}
