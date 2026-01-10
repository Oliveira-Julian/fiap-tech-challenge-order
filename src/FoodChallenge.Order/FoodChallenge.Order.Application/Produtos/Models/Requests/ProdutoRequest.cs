using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Application.Produtos.Models.Requests;

public sealed class ProdutoRequest
{
    public ProdutoCategoria Categoria { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
}
