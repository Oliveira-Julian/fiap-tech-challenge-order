using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;
using FoodChallenge.Order.Application.Produtos.Mappers;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PedidoItemMapper
{
    public static PedidoItem ToDomain(PedidoItemEntity pedidoItemEntity)
    {
        if (pedidoItemEntity is null)
            return null;

        return new PedidoItem()
        {
            Id = pedidoItemEntity.Id,
            IdPedido = pedidoItemEntity.IdPedido,
            IdProduto = pedidoItemEntity.IdProduto,
            Codigo = pedidoItemEntity.Codigo,
            DataAtualizacao = pedidoItemEntity.DataAtualizacao,
            DataCriacao = pedidoItemEntity.DataCriacao,
            DataExclusao = pedidoItemEntity.DataExclusao,
            Ativo = pedidoItemEntity.Ativo,
            Quantidade = pedidoItemEntity.Quantidade,
            Valor = pedidoItemEntity.Valor,
            Produto = ProdutoMapper.ToDomain(pedidoItemEntity.Produto)
        };
    }

    public static PedidoItemEntity ToEntity(PedidoItem pedidoItem)
    {
        if (pedidoItem is null)
            return null;

        return new PedidoItemEntity()
        {
            Id = pedidoItem.Id,
            IdPedido = pedidoItem.IdPedido,
            IdProduto = pedidoItem.IdProduto,
            Codigo = pedidoItem.Codigo,
            DataAtualizacao = pedidoItem.DataAtualizacao,
            DataCriacao = pedidoItem.DataCriacao,
            DataExclusao = pedidoItem.DataExclusao,
            Ativo = pedidoItem.Ativo,
            Quantidade = pedidoItem.Quantidade,
            Valor = pedidoItem.Valor,
            Produto = ProdutoMapper.ToEntity(pedidoItem.Produto)
        };
    }

    public static PedidoItem ToDomain(PedidoItemRequest request)
    {
        if (request is null)
            return null;

        return new PedidoItem()
        {
            IdProduto = request.IdProduto,
            Quantidade = request.Quantidade
        };
    }
}
