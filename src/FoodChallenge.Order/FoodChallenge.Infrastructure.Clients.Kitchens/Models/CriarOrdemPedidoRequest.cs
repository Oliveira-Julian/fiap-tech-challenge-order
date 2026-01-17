namespace FoodChallenge.Infrastructure.Clients.Kitchens.Models;

public sealed class CriarOrdemPedidoRequest
{
    public Guid IdPedido { get; set; }
    public string CodigoPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public IEnumerable<CriarOrdemPedidoItemRequest> Itens { get; set; }
}

public sealed class CriarOrdemPedidoItemRequest
{
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public string Categoria { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
}
