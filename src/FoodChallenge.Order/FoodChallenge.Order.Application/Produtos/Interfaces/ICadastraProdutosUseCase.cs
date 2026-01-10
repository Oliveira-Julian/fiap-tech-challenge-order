using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface ICadastraProdutosUseCase
{
    Task<IEnumerable<Produto>> ExecutarAsync(IEnumerable<Produto> produtos, CancellationToken cancellationToken);
}
