namespace FoodChallenge.Order.Application.Pedidos.Models.Responses;

public sealed class PedidoItemResponse
{
    public Guid? Id { get; set; }
    public Guid? IdProduto { get; set; }
    public string Codigo { get; set; }
    public decimal Quantidade { get; set; }
    public decimal Valor { get; set; }
}
