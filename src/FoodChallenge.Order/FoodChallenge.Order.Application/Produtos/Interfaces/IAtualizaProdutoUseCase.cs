using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface IAtualizaProdutoUseCase
{
    Task<Produto> ExecutarAsync(Produto produto, CancellationToken cancellationToken);
}
