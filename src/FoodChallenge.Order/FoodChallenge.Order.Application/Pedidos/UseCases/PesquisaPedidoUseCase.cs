using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Pedidos.Interfaces;
using FoodChallenge.Order.Domain.Constants;
using FoodChallenge.Order.Domain.Pedidos;
using Serilog;

namespace FoodChallenge.Order.Application.Pedidos.UseCases;

public class PesquisaPedidoUseCase(IPedidoGateway pedidoGateway) : IPesquisaPedidoUseCase
{
    private readonly ILogger logger = Log.ForContext<PesquisaPedidoUseCase>();

    public async Task<Pagination<Pedido>> ExecutarAsync(Filter<PedidoFilter> filtro, CancellationToken cancellationToken)
    {
        logger.Information(Logs.InicioExecucaoServico, nameof(PesquisaPedidoUseCase), nameof(ExecutarAsync));

        try
        {
            var pedidosPaginacao = await pedidoGateway.ObterPedidosPorFiltroAsync(filtro, cancellationToken);

            logger.Information(Logs.FimExecucaoServico, nameof(PesquisaPedidoUseCase), nameof(ExecutarAsync), pedidosPaginacao);

            return pedidosPaginacao;
        }
        catch (Exception ex)
        {
            logger.Error(ex, Logs.ErroGenerico, nameof(PesquisaPedidoUseCase), nameof(ExecutarAsync));
            throw;
        }
    }
}
