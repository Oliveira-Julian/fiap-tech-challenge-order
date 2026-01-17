using FoodChallenge.Common.Interfaces;
using FoodChallenge.Common.Validators;
using FoodChallenge.Order.Application.Pedidos.Interfaces;
using FoodChallenge.Order.Application.Pedidos.Specifications;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Pedidos;
using Serilog;

namespace FoodChallenge.Order.Application.Pedidos.UseCases;

public class AtualizaStatusPedidoUseCase(
    ValidationContext validationContext,
    IUnitOfWork unitOfWork,
    IPedidoGateway pedidoGateway) : IAtualizaStatusPedidoUseCase
{
    private readonly ILogger logger = Log.ForContext<AtualizaStatusPedidoUseCase>();

    public async Task<Pedido> ExecutarAsync(Guid idPedido, PedidoStatus pedidoStatus, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(AtualizaStatusPedidoUseCase), nameof(ExecutarAsync));

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

            pedido.AtualizarStatusPedido(pedidoStatus);

            unitOfWork.BeginTransaction();
            pedidoGateway.AtualizarPedido(pedido);
            await unitOfWork.CommitAsync();

            var pedidoAtualizado = await pedidoGateway.ObterPedidoAsync(pedido.Id.Value, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(AtualizaStatusPedidoUseCase), nameof(ExecutarAsync), pedidoAtualizado);

            return pedidoAtualizado;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            logger.Error(ex, Logs.ErroGenerico, nameof(AtualizaStatusPedidoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
