namespace FoodChallenge.Order.Application.Pedidos.Models.Requests;

public sealed class PedidoItemRequest
{
    public Guid IdProduto { get; set; }
    public int Quantidade { get; set; }
}
