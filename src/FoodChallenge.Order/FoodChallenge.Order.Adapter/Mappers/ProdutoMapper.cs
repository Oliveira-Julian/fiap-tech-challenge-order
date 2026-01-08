using FoodChallenge.Order.Adapter.Mappers;
using FoodChallenge.CrossCutting.Paging;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;
using FoodChallenge.Order.Application.Produtos.Models.Requests;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Application.Produtos.Mappers;

public static class ProdutoMapper
{
    public static Produto ToDomain(ProdutoEntity produtoEntity)
    {
        if (produtoEntity is null) return default;

        return new Produto
        {
            Id = produtoEntity.Id,
            DataAtualizacao = produtoEntity.DataAtualizacao,
            DataCriacao = produtoEntity.DataCriacao,
            DataExclusao = produtoEntity.DataExclusao,
            Ativo = produtoEntity.Ativo,
            Categoria = (ProdutoCategoria)produtoEntity.Categoria,
            Nome = produtoEntity.Nome,
            Descricao = produtoEntity.Descricao,
            Preco = produtoEntity.Preco,
            Imagens = produtoEntity.Imagens?.Select(ProdutoImagemMapper.ToDomain)
        };
    }

    public static ProdutoEntity ToEntity(Produto produto)
    {
        if (produto is null) return default;

        return new ProdutoEntity
        {
            Id = produto.Id,
            DataAtualizacao = produto.DataAtualizacao,
            DataCriacao = produto.DataCriacao,
            DataExclusao = produto.DataExclusao,
            Ativo = produto.Ativo,
            Categoria = (int)produto.Categoria,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Imagens = produto.Imagens?.Select(ProdutoImagemMapper.ToEntity)?.ToList()
        };
    }

    public static Produto ToDomain(ProdutoRequest produtoRequest, Guid? id)
    {
        if (produtoRequest is null) return default;

        var produto = new Produto()
        {
            Categoria = produtoRequest.Categoria,
            Nome = produtoRequest.Nome,
            Descricao = produtoRequest.Descricao,
            Preco = produtoRequest.Preco
        };

        if (id.HasValue)
            produto.Id = id.Value;

        return produto;
    }

    public static Filter<ProdutoFilter> ToDomain(FiltrarProdutoRequest filterRequest)
    {
        var produtoFilter = new ProdutoFilter()
        {
            Categorias = filterRequest?.Categorias,
            Ativo = true
        };

        if (filterRequest is null)
            return new Filter<ProdutoFilter>(1, 30, produtoFilter);

        return new Filter<ProdutoFilter>(filterRequest.Page,
            filterRequest.SizePage,
            filterRequest.FieldOrdenation,
            filterRequest.OrdenationAsc,
            produtoFilter);
    }

    public static Pagination<Produto> ToPagination(Pagination<ProdutoEntity> pagination)
    {
        if (pagination is null) return default;

        return new Pagination<Produto>(
            pagination.Page,
            pagination.SizePage,
            pagination.TotalRecords,
            pagination.Records?.Select(ToDomain));
    }

    internal static Filter<ProdutoEntityFilter> ToEntityFilter(Filter<ProdutoFilter> domainFilter)
    {
        if (domainFilter == null) return null;

        var filter = new ProdutoEntityFilter
        {
            Categorias = domainFilter.Fields?.Categorias,
            Ativo = domainFilter.Fields?.Ativo ?? true
        };

        return new Filter<ProdutoEntityFilter>(
            domainFilter.Page,
            domainFilter.SizePage,
            domainFilter.FieldOrdenation,
            domainFilter.OrdenationAsc,
            filter
        );
    }
}
