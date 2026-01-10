namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface IRemoveProdutoUseCase
{
    Task ExecutarAsync(Guid id, CancellationToken cancellationToken);
}
