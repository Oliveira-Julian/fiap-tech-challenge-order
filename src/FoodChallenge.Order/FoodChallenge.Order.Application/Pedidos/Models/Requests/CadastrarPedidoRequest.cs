namespace FoodChallenge.Order.Application.Pedidos.Models.Requests;

public sealed class CadastrarPedidoRequest
{
    public string Cpf { get; set; }
    public IEnumerable<PedidoItemRequest> Itens { get; set; }
}
