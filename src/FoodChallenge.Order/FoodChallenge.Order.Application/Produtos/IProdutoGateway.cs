using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos;

public interface IProdutoGateway
{
    Task<IEnumerable<Produto>> ObterProdutosPorIdsAsync(IEnumerable<Guid?> ids, CancellationToken cancellationToken, bool tracking = false);
    Task CadastrarProdutosAsync(IEnumerable<Produto> produtos, CancellationToken cancellationToken);
    void AtualizarProduto(Produto produto);
    Task<Produto> ObterProdutoPorIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = false);
    Task<Produto> ObterProdutoPorIdComImagensAsync(Guid id, CancellationToken cancellationToken, bool tracking = false);
    Task<Pagination<Produto>> ObterProdutosPorFiltroAsync(Filter<ProdutoFilter> filtro, CancellationToken cancellationToken);
    Task<ProdutoImagem> ObterProdutoImagemPorIdAsync(Guid idImagem, CancellationToken cancellationToken);
    Task CadastrarImagensAsync(IEnumerable<ProdutoImagem> produtoImagens, CancellationToken cancellationToken);
    void RemoverProdutoImagem(ProdutoImagem produtoImagem);
}
