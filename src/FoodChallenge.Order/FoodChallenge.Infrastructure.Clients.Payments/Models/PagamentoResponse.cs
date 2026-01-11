namespace FoodChallenge.Infrastructure.Clients.Payments.Models;

public sealed class PagamentoResponse
{
    public Guid? Id { get; set; }
    public string QrCode { get; set; }
    public int Status { get; set; }
    public string DescricaoStatus { get; set; }
}
