using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Produtos.Models.Reponses;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Adapter.Presenters;

public static class ProdutoPresenter
{
    public static ProdutoResponse ToResponse(Produto produto)
    {
        if (produto is null) return default;

        return new ProdutoResponse()
        {
            Id = produto.Id,
            Categoria = produto.Categoria,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagens = produto.Imagens?.Select(ProdutoImagemPresenter.ToResponse)
        };
    }

    public static Pagination<ProdutoResponse> ToPaginationResponse(Pagination<Produto> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<ProdutoResponse>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToResponse));
    }
}
