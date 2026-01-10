using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Application.Pedidos;

public interface IPedidoGateway
{
    Task<Pedido> ObterPedidoComRelacionamentosAsync(Guid idPedido, CancellationToken cancellationToken, bool tracking = false);
    Task<Pedido> ObterPedidoAsync(Guid idPedido, CancellationToken cancellationToken, bool tracking = false);
    Task<Pagination<Pedido>> ObterPedidosPorFiltroAsync(Filter<PedidoFilter> filtro, CancellationToken cancellationToken);
    void AtualizarPedido(Pedido pedido);
    Task<Pedido> CadastrarPedidoAsync(Pedido pedido, CancellationToken cancellationToken);
    Task<IEnumerable<Pedido>> ObterPedidosPorProdutoAsync(Guid idProduto, IEnumerable<PedidoStatus> pedidosStatus, CancellationToken cancellationToken, bool tracking = false);
}
