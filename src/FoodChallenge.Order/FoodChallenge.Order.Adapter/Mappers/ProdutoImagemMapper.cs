using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;
using FoodChallenge.Order.Application.Produtos.Imagem.Models.Requests;
using FoodChallenge.Order.Application.Produtos.Mappers;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class ProdutoImagemMapper
{
    public static ProdutoImagem ToDomain(ProdutoImagemEntity produtoImagemEntity)
    {
        if (produtoImagemEntity is null) return default;

        return new ProdutoImagem()
        {
            Id = produtoImagemEntity.Id,
            IdProduto = produtoImagemEntity.IdProduto,
            Nome = produtoImagemEntity.Nome,
            Tipo = produtoImagemEntity.Tipo,
            Tamanho = produtoImagemEntity.Tamanho,
            Conteudo = produtoImagemEntity.Conteudo,
            DataAtualizacao = produtoImagemEntity.DataAtualizacao,
            DataCriacao = produtoImagemEntity.DataCriacao,
            DataExclusao = produtoImagemEntity.DataExclusao,
            Ativo = produtoImagemEntity.Ativo,
            Produto = ProdutoMapper.ToDomain(produtoImagemEntity.Produto)
        };
    }

    public static ProdutoImagemEntity ToEntity(ProdutoImagem produtoImagem)
    {
        if (produtoImagem is null) return default;

        return new ProdutoImagemEntity()
        {
            Id = produtoImagem.Id,
            IdProduto = produtoImagem.IdProduto,
            Nome = produtoImagem.Nome,
            Tipo = produtoImagem.Tipo,
            Tamanho = produtoImagem.Tamanho,
            Conteudo = produtoImagem.Conteudo,
            DataAtualizacao = produtoImagem.DataAtualizacao,
            DataCriacao = produtoImagem.DataCriacao,
            DataExclusao = produtoImagem.DataExclusao,
            Ativo = produtoImagem.Ativo,
            Produto = ProdutoMapper.ToEntity(produtoImagem.Produto)
        };
    }

    public static IEnumerable<ProdutoImagem> ToDomain(Guid idProduto, ProdutoImagemRequest produtoImagemRequest)
    {
        if (!(produtoImagemRequest?.Imagens?.Any()) ?? true) return default;

        var produtoImagens = new List<ProdutoImagem>();

        foreach (var file in produtoImagemRequest?.Imagens)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);

            produtoImagens.Add(new ProdutoImagem()
            {
                IdProduto = idProduto,
                Nome = file.FileName,
                Tipo = file.ContentType,
                Tamanho = file.Length,
                Conteudo = ms.ToArray()
            });
        }

        return produtoImagens;
    }
}
