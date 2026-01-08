using FoodChallenge.CrossCutting.Paging;

namespace FoodChallenge.Order.Application.Pedidos.Models.Requests;

public sealed class FiltrarPedidoRequest : Filter
{
    public Guid? Id { get; set; }
    public Guid? IdCliente { get; set; }
}
