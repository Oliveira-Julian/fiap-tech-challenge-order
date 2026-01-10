namespace FoodChallenge.Order.Domain.Produtos;

public class ProdutoFilter
{
    public IEnumerable<int> Categorias { get; set; }
    public bool Ativo { get; set; } = true;
}
