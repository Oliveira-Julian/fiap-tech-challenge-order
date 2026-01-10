using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Domain.Produtos;

public sealed class Produto : DomainBase
{
    public ProdutoCategoria Categoria { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public IEnumerable<ProdutoImagem> Imagens { get; set; }

    public Produto()
    {
        Id = Guid.NewGuid();
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }
}
