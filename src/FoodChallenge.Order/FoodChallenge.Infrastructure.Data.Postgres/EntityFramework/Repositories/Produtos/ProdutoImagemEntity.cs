using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;

public class ProdutoImagemEntity : EntityBase
{
    public Guid? IdProduto { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public decimal Tamanho { get; set; }
    public byte[] Conteudo { get; set; }

    public virtual ProdutoEntity Produto { get; set; }
}
