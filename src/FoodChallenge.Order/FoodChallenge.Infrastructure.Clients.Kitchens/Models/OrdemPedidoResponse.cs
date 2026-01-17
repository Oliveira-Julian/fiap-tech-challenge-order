namespace FoodChallenge.Infrastructure.Clients.Kitchens.Models;

public sealed class OrdemPedidoResponse
{
    public Guid Id { get; set; }
    public Guid IdPedido { get; set; }
    public int Status { get; set; }
    public string DescricaoStatus { get; set; }
    public DateTime? DataInicioPreparacao { get; set; }
    public DateTime? DataFimPreparacao { get; set; }
}
