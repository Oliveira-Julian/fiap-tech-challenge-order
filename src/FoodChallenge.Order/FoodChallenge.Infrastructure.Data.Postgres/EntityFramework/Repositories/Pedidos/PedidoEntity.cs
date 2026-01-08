using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PedidoEntity : EntityBase
{
    public Guid? IdCliente { get; set; }
    public ClienteEntity Cliente { get; set; }
    public Guid? IdPagamento { get; set; }
    public PagamentoEntity Pagamento { get; set; }
    public ICollection<PedidoItemEntity> Itens { get; set; }
    public string Codigo { get; set; }
    public decimal ValorTotal { get; set; }
    public int Status { get; set; }
    public OrdemPedidoEntity OrdemPedido { get; set; }
}
