namespace FoodChallenge.Infrastructure.Clients.Kitchens.Models;

public sealed class CadastrarOrdemPedidoRequest
{
    public Guid IdPedido { get; set; }
    public string CodigoPedido { get; set; }
    public IEnumerable<CadastrarOrdemPedidoItemRequest> Itens { get; set; }
}

public sealed class CadastrarOrdemPedidoItemRequest
{
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public string Categoria { get; set; }
    public int Quantidade { get; set; }
}
