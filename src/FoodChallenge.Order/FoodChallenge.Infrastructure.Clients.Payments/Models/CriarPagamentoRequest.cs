namespace FoodChallenge.Infrastructure.Clients.Payments.Models;

public sealed class CriarPagamentoRequest
{
    public Guid? IdPedido { get; set; }
    public string CodigoPedido { get; set; }
    public decimal ValorTotal { get; set; }
    public IEnumerable<CriarPagamentoItemRequest> Itens { get; set; }
}

public sealed class CriarPagamentoItemRequest
{
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public string Categoria { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }
}
