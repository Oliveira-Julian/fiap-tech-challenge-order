using FoodChallenge.Order.Domain.Entities;

namespace FoodChallenge.Order.Domain.Produtos;

public class ProdutoImagem : DomainBase
{
    public Guid? IdProduto { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public decimal Tamanho { get; set; }
    public byte[] Conteudo { get; set; }

    public virtual Produto Produto { get; set; }

    public ProdutoImagem()
    {
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }
}
