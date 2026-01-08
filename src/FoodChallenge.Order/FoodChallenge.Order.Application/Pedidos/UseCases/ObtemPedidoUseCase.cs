using FoodChallenge.Order.Application.Pedidos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Pedidos;
using Serilog;

namespace FoodChallenge.Order.Application.Pedidos.UseCases;

public class ObtemPedidoUseCase(
    IPedidoGateway pedidoGateway) : IObtemPedidoUseCase
{
    private readonly ILogger logger = Log.ForContext<ObtemPedidoUseCase>();

    public async Task<Pedido> ExecutarAsync(Guid idOrdemPedido, CancellationToken cancellationToken)
    {
        try
        {
            logger.Information(Logs.InicioExecucaoServico, nameof(ObtemPedidoUseCase), nameof(ExecutarAsync));

            var pedido = await pedidoGateway.ObterPedidoComRelacionamentosAsync(idOrdemPedido, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(ObtemPedidoUseCase), nameof(ExecutarAsync), pedido);

            return pedido;
        }
        catch (Exception ex)
        {
            logger.Error(ex, Logs.ErroGenerico, nameof(ObtemPedidoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
