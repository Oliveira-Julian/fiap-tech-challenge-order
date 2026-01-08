using FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Application.Produtos.Models.Reponses;

public sealed class ProdutoResponse
{
    public Guid? Id { get; set; }
    public ProdutoCategoria Categoria { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public IEnumerable<ProdutoImagemResponse> Imagens { get; set; }
}
