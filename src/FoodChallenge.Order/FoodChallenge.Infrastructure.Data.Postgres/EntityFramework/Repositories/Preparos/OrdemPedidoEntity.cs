using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos;

public class OrdemPedidoEntity : EntityBase
{
    public Guid? IdPedido { get; set; }
    public PedidoEntity Pedido { get; set; }
    public int Status { get; set; }
    public DateTime? DataInicioPreparacao { get; set; }
    public DateTime? DataFimPreparacao { get; set; }
}
