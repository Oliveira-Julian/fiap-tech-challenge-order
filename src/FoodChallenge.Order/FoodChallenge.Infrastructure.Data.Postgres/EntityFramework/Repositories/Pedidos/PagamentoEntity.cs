using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PagamentoEntity : EntityBase
{
    public Guid? IdPedido { get; set; }
    public Guid? ChaveMercadoPagoOrdem { get; set; }
    public string IdMercadoPagoOrdem { get; set; }
    public string IdMercadoPagoPagamento { get; set; }
    public int Status { get; set; }
    public decimal Valor { get; set; }
    public int Metodo { get; set; }
    public string QrCode { get; set; }

    public PedidoEntity Pedido { get; set; }
}
