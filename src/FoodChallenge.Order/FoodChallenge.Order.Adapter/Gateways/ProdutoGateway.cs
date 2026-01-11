using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;
using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.Order.Application.Produtos;
using FoodChallenge.Order.Application.Produtos.Mappers;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Adapter.Gateways;

public class ProdutoGateway(
    IProdutoRepository produtoDataSource,
    IProdutoImagemRepository produtoImagemDataSource) : IProdutoGateway
{
    public async Task<IEnumerable<Produto>> ObterProdutosPorIdsAsync(IEnumerable<Guid?> ids, CancellationToken cancellationToken, bool tracking = false)
    {
        var produtosEntity = await produtoDataSource.GetByIdsAsync(ids, cancellationToken, tracking);

        var produtosAtivos = produtosEntity?.Where(p => p.Ativo) ?? Enumerable.Empty<ProdutoEntity>();

        return produtosAtivos?.Select(ProdutoMapper.ToDomain);
    }

    public async Task CadastrarProdutosAsync(IEnumerable<Produto> produtos, CancellationToken cancellationToken)
    {
        var produtosEntity = produtos?.Select(ProdutoMapper.ToEntity);
        await produtoDataSource.AddItemsAsync(produtosEntity, cancellationToken);
    }

    public void AtualizarProduto(Produto produto)
    {
        var produtoEntity = ProdutoMapper.ToEntity(produto);
        produtoDataSource.Update(produtoEntity);
    }

    public async Task<Produto> ObterProdutoPorIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
    {
        var produtoEntity = await produtoDataSource.GetByIdAsync(id, cancellationToken, tracking);
        if (!(produtoEntity?.Ativo ?? false))
            return default;

        return ProdutoMapper.ToDomain(produtoEntity);
    }

    public async Task<Produto> ObterProdutoPorIdComImagensAsync(Guid id, CancellationToken cancellationToken, bool tracking = false)
    {
        var produtoEntity = await produtoDataSource.GetByIdAsync(id, cancellationToken, tracking);

        if (!(produtoEntity?.Ativo ?? false))
            return default;

        if (produtoEntity is not null)
        {
            var imagens = await produtoImagemDataSource.GetByProductIdAsync(id, cancellationToken);
            produtoEntity.Imagens = imagens;
        }

        return ProdutoMapper.ToDomain(produtoEntity);
    }

    public async Task<Pagination<Produto>> ObterProdutosPorFiltroAsync(Filter<ProdutoFilter> filtro, CancellationToken cancellationToken)
    {
        var filterEntity = ProdutoMapper.ToEntityFilter(filtro);
        var pagedEntity = await produtoDataSource.QueryPagedAsync(filterEntity, cancellationToken);
        return ProdutoMapper.ToPagination(pagedEntity);
    }

    public async Task<ProdutoImagem> ObterProdutoImagemPorIdAsync(Guid idImagem, CancellationToken cancellationToken)
    {
        var produtoImagemEntity = await produtoImagemDataSource.GetByIdAsync(idImagem, cancellationToken);

        return ProdutoImagemMapper.ToDomain(produtoImagemEntity);
    }

    public async Task CadastrarImagensAsync(IEnumerable<ProdutoImagem> produtoImagens, CancellationToken cancellationToken)
    {
        var produtoImagensEntity = produtoImagens?.Select(ProdutoImagemMapper.ToEntity);
        await produtoImagemDataSource.AddItemsAsync(produtoImagensEntity, cancellationToken);
    }

    public void RemoverProdutoImagem(ProdutoImagem produtoImagem)
    {
        var produtoImagemEntity = ProdutoImagemMapper.ToEntity(produtoImagem);
        produtoImagemDataSource.Remove(produtoImagemEntity);
    }
}
