using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pagamentos;

public class PagamentoEntity : EntityBase
{
    public Guid? IdPedido { get; set; }
    public int Status { get; set; }
    public string QrCode { get; set; }

    public PedidoEntity Pedido { get; set; }
}
