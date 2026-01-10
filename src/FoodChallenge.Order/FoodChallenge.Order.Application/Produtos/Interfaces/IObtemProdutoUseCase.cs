using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface IObtemProdutoUseCase
{
    Task<Produto> ExecutarAsync(Guid id, CancellationToken cancellationToken);
}
