using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;

public sealed class ProdutoEntity : EntityBase
{
    public int Categoria { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public ICollection<ProdutoImagemEntity> Imagens { get; set; }
}
