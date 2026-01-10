using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface IPesquisaProdutoUseCase
{
    Task<Pagination<Produto>> ExecutarAsync(Filter<ProdutoFilter> filtro, CancellationToken cancellationToken);
}
