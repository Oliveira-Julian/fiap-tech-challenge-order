using FoodChallenge.CrossCutting.Paging;

namespace FoodChallenge.Order.Application.Produtos.Models.Requests
{
    public sealed class FiltrarProdutoRequest : Filter
    {
        public IEnumerable<int> Categorias { get; set; }
    }
}
