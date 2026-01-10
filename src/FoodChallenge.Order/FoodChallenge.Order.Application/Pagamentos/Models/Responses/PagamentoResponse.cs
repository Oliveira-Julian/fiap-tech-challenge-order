namespace FoodChallenge.Order.Application.Pagamentos.Models.Responses;

public sealed class PagamentoResponse
{
    public string QrCode { get; set; }
    public int Status { get; set; }
    public string DescricaoStatus { get; set; }
    public string IdMercadoPagoPagamento { get; set; }
}
