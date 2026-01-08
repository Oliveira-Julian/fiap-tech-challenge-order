using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Interfaces;
using FoodChallenge.Order.Application.Pedidos;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Adapter.Gateways;

public class PedidoGateway(IPedidoRepository pedidoDataSource) : IPedidoGateway
{
    public async Task<Pedido> ObterPedidoComRelacionamentosAsync(Guid idPedido, CancellationToken cancellationToken, bool tracking = false)
    {
        var pedidoEntity = await pedidoDataSource.GetByIdWithRelationsAsync(idPedido, cancellationToken, tracking);
        return PedidoMapper.ToDomain(pedidoEntity);
    }

    public async Task<Pedido> ObterPedidoAsync(Guid idPedido, CancellationToken cancellationToken, bool tracking = false)
    {
        var pedidoEntity = await pedidoDataSource.GetByIdAsync(idPedido, cancellationToken, tracking);
        return PedidoMapper.ToDomain(pedidoEntity);
    }

    public async Task<Pagination<Pedido>> ObterPedidosPorFiltroAsync(Filter<PedidoFilter> filtro, CancellationToken cancellationToken)
    {
        var filterEntity = PedidoMapper.ToEntityFilter(filtro);
        var pagedEntity = await pedidoDataSource.QueryPagedAsync(filterEntity, cancellationToken);
        return PedidoMapper.ToPagination(pagedEntity);
    }

    public void AtualizarPedido(Pedido pedido)
    {
        var pedidoEntity = PedidoMapper.ToEntity(pedido);
        pedidoDataSource.Update(pedidoEntity);
    }

    public async Task<Pedido> CadastrarPedidoAsync(Pedido pedido, CancellationToken cancellationToken)
    {
        var pedidoEntity = PedidoMapper.ToEntity(pedido);
        pedidoEntity = await pedidoDataSource.AddAsync(pedidoEntity, cancellationToken);
        return PedidoMapper.ToDomain(pedidoEntity);
    }

    public async Task<IEnumerable<Pedido>> ObterPedidosPorProdutoAsync(Guid idProduto, IEnumerable<PedidoStatus> pedidosStatus, CancellationToken cancellationToken, bool tracking = false)
    {
        var pedidosEntity = await pedidoDataSource.ObterPedidosPorProdutoAsync(idProduto, pedidosStatus?.Select(status => (int)status), cancellationToken, tracking);
        return pedidosEntity?.Select(PedidoMapper.ToDomain);
    }
}
