using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PedidoMapper
{
    public static Pedido ToDomain(PedidoEntity pedidoEntity)
    {
        if (pedidoEntity is null) return default;

        return new Pedido()
        {
            Id = pedidoEntity.Id,
            IdPagamento = pedidoEntity.IdPagamento,
            IdCliente = pedidoEntity.IdCliente,
            Codigo = pedidoEntity.Codigo,
            DataAtualizacao = pedidoEntity.DataAtualizacao,
            DataCriacao = pedidoEntity.DataCriacao,
            DataExclusao = pedidoEntity.DataExclusao,
            Ativo = pedidoEntity.Ativo,
            Status = (PedidoStatus)pedidoEntity.Status,
            ValorTotal = pedidoEntity.ValorTotal,
            Cliente = ClienteMapper.ToDomain(pedidoEntity.Cliente),
            Itens = pedidoEntity.Itens?.Select(PedidoItemMapper.ToDomain)
        };
    }

    public static PedidoEntity ToEntity(Pedido pedido)
    {
        if (pedido is null) return default;

        return new PedidoEntity()
        {
            Id = pedido.Id,
            IdPagamento = pedido.IdPagamento,
            IdCliente = pedido.IdCliente,
            Codigo = pedido.Codigo,
            DataAtualizacao = pedido.DataAtualizacao,
            DataCriacao = pedido.DataCriacao,
            DataExclusao = pedido.DataExclusao,
            Ativo = pedido.Ativo,
            Status = (int)pedido.Status,
            ValorTotal = pedido.ValorTotal,
            Itens = pedido.Itens?.Select(PedidoItemMapper.ToEntity)?.ToList()
        };
    }

    public static Pedido ToDomain(Guid? idCliente)
    {
        if (!idCliente.HasValue) return default;

        return new Pedido()
        {
            IdCliente = idCliente.Value
        };
    }

    public static Filter<PedidoFilter> ToDomain(FiltrarPedidoRequest filterRequest)
    {
        var filter = new PedidoFilter()
        {
            Id = filterRequest?.Id,
            IdCliente = filterRequest?.IdCliente,
            Ativo = true
        };

        if (filterRequest is null)
            return new Filter<PedidoFilter>(1, 30, filter);

        return new Filter<PedidoFilter>(filterRequest.Page,
            filterRequest.SizePage,
            filterRequest.FieldOrdenation,
            filterRequest.OrdenationAsc,
            filter);
    }

    public static Pagination<Pedido> ToPagination(Pagination<PedidoEntity> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<Pedido>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToDomain));
    }

    public static Filter<PedidoEntityFilter> ToEntityFilter(Filter<PedidoFilter> domainFilter)
    {
        if (domainFilter == null) return null;

        var filter = new PedidoEntityFilter
        {
            Id = domainFilter.Fields?.Id,
            IdCliente = domainFilter.Fields?.IdCliente,
            Ativo = domainFilter.Fields?.Ativo ?? true
        };

        return new Filter<PedidoEntityFilter>(
            domainFilter.Page,
            domainFilter.SizePage,
            domainFilter.FieldOrdenation,
            domainFilter.OrdenationAsc,
            filter
        );
    }
}
