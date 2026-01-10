using FoodChallenge.Order.Application.Clientes.Models.Responses;
using FoodChallenge.Order.Application.Pagamentos.Models.Responses;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Application.Pedidos.Models.Responses;

public class PedidoResponse
{
    public Guid? Id { get; set; }
    public string Codigo { get; set; }
    public decimal? ValorTotal { get; set; }
    public PedidoStatus? Status { get; set; }
    public string DescricaoStatus { get; set; }
    public ClienteResponse Cliente { get; set; }
    public PagamentoResponse Pagamento { get; set; }
    public IEnumerable<PedidoItemResponse> Itens { get; set; }
}
