using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PreparoMapper
{
    public static OrdemPedido ToDomain(OrdemPedidoEntity ordemPedidoEntity)
    {
        if (ordemPedidoEntity is null) return default;

        return new OrdemPedido()
        {
            Id = ordemPedidoEntity.Id,
            IdPedido = ordemPedidoEntity.IdPedido,
            DataAtualizacao = ordemPedidoEntity.DataAtualizacao,
            DataCriacao = ordemPedidoEntity.DataCriacao,
            DataExclusao = ordemPedidoEntity.DataExclusao,
            Ativo = ordemPedidoEntity.Ativo,
            Status = (PreparoStatus)ordemPedidoEntity.Status,
            DataInicioPreparacao = ordemPedidoEntity.DataInicioPreparacao,
            DataFimPreparacao = ordemPedidoEntity.DataFimPreparacao,
            Pedido = PedidoMapper.ToDomain(ordemPedidoEntity.Pedido)
        };
    }

    public static OrdemPedidoRequest ToRequest(OrdemPedido ordemPedido)
    {
        if (ordemPedido is null) return default;

        return new OrdemPedidoEntity
        {
            Id = ordemPedido.Id,
            IdPedido = ordemPedido.IdPedido,
            DataAtualizacao = ordemPedido.DataAtualizacao,
            DataCriacao = ordemPedido.DataCriacao,
            DataExclusao = ordemPedido.DataExclusao,
            Ativo = ordemPedido.Ativo,
            Status = (int)ordemPedido.Status,
            DataInicioPreparacao = ordemPedido.DataInicioPreparacao,
            DataFimPreparacao = ordemPedido.DataFimPreparacao,
            Pedido = PedidoMapper.ToEntity(ordemPedido.Pedido)
        };
    }
}
