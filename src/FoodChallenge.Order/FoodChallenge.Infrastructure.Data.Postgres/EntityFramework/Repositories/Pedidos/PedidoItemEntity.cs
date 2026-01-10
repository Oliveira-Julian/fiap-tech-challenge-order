using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

public class PedidoItemEntity : EntityBase
{
    public Guid? IdProduto { get; set; }
    public Guid? IdPedido { get; set; }
    public string Codigo { get; set; }
    public decimal Valor { get; set; }
    public int Quantidade { get; set; }

    public virtual PedidoEntity Pedido { get; set; }
    public virtual ProdutoEntity Produto { get; set; }
}
