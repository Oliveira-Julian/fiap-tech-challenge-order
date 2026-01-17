using FoodChallenge.Infrastructure.Clients.Kitchens.Models;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PreparoItemMapper
{
    public static CriarOrdemPedidoItemRequest ToRequest(PedidoItem pedidoItem)
    {
        if (pedidoItem is null) return default;

        return new CriarOrdemPedidoItemRequest
        {
            Codigo = pedidoItem.Codigo,
            Categoria = pedidoItem.Produto.Categoria.ToString(),
            Nome = pedidoItem.Produto?.Nome,
            Quantidade = pedidoItem.Quantidade,
            Valor = pedidoItem.Valor
        };
    }
}
