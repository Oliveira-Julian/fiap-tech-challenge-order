using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Interfaces;

public interface ICadastraProdutoImagemUseCase
{
    Task ExecutarAsync(Guid idProduto, IEnumerable<ProdutoImagem> produtoImagens, CancellationToken cancellationToken);
}
