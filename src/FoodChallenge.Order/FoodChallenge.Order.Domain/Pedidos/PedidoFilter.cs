namespace FoodChallenge.Order.Domain.Pedidos;

public class PedidoFilter
{
    public Guid? Id { get; set; }
    public Guid? IdCliente { get; set; }
    public bool Ativo { get; set; } = true;
}
