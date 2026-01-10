namespace FoodChallenge.Order.Application.Produtos.Interfaces
{
    public interface IRemoveProdutoImagemUseCase
    {
        Task ExecutarAsync(Guid idProduto, Guid idImagem, CancellationToken cancellationToken);
    }
}
