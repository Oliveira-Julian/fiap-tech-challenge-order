using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Responses;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Adapter.Presenters;

public static class ProdutoImagemPresenter
{
    public static ProdutoImagemResponse ToResponse(ProdutoImagem produtoImagem)
    {
        if (produtoImagem is null) return default;

        return new ProdutoImagemResponse()
        {
            Id = produtoImagem.Id,
            Nome = produtoImagem.Nome,
            Tipo = produtoImagem.Tipo,
            Tamanho = produtoImagem.Tamanho,
            Conteudo = produtoImagem.Conteudo
        };
    }

    public static Pagination<ProdutoImagemResponse> ToPaginationResponse(Pagination<ProdutoImagem> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<ProdutoImagemResponse>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToResponse));
    }
}
