using FoodChallenge.Infrastructure.Clients.Kitchens.Models;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Order.Domain.Preparos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PreparoMapper
{
    public static OrdemPedido ToDomain(OrdemPedidoResponse response)
    {
        if (response is null) return default;

        return new OrdemPedido(response.Id, response.IdPedido, response.Status, response.DataInicioPreparacao, response.DataFimPreparacao);
    }

    public static CadastrarOrdemPedidoRequest ToRequest(Pedido pedido)
    {
        if (pedido is null) return default;

        return new CadastrarOrdemPedidoRequest
        {
            IdPedido = pedido.Id.Value,
            CodigoPedido = pedido.Codigo,
            ValorTotal = pedido.ValorTotal,
            Itens = pedido.Itens?.Select(PreparoItemMapper.ToRequest)
        };
    }
}
