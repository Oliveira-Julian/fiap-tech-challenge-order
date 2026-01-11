using Bogus;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;
using FoodChallenge.Order.Application.Produtos.Mappers;
using FoodChallenge.Order.Application.Produtos.Models.Requests;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Adapter.Mappers;

public class ProdutoMapperTests : TestBase
{
    private readonly Faker _faker;

    public ProdutoMapperTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DeveConverterProdutoEntityParaDomain()
    {
        // Arrange
        var produtoEntity = new ProdutoEntity
        {
            Id = Guid.NewGuid(),
            Nome = "Hambúrguer",
            Descricao = "Hambúrguer artesanal",
            Preco = 25.50m,
            Categoria = (int)ProdutoCategoria.Lanche,
            Ativo = true,
            DataCriacao = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow,
            DataExclusao = null,
            Imagens = null
        };

        // Act
        var resultado = ProdutoMapper.ToDomain(produtoEntity);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoEntity.Id, resultado.Id);
        Assert.Equal(produtoEntity.Nome, resultado.Nome);
        Assert.Equal(produtoEntity.Descricao, resultado.Descricao);
        Assert.Equal(produtoEntity.Preco, resultado.Preco);
        Assert.Equal((ProdutoCategoria)produtoEntity.Categoria, resultado.Categoria);
        Assert.Equal(produtoEntity.Ativo, resultado.Ativo);
    }

    [Fact]
    public void DeveConverterProdutoParaEntity()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        // Act
        var resultado = ProdutoMapper.ToEntity(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produto.Id, resultado.Id);
        Assert.Equal(produto.Nome, resultado.Nome);
        Assert.Equal(produto.Descricao, resultado.Descricao);
        Assert.Equal(produto.Preco, resultado.Preco);
        Assert.Equal((int)produto.Categoria, resultado.Categoria);
        Assert.Equal(produto.Ativo, resultado.Ativo);
    }

    [Fact]
    public void DeveConverterProdutoRequestParaDomain_ComId()
    {
        // Arrange
        var produtoId = Guid.NewGuid();
        var produtoRequest = new ProdutoRequest
        {
            Nome = "Pizza",
            Descricao = "Pizza deliciosa",
            Preco = 45.00m,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act
        var resultado = ProdutoMapper.ToDomain(produtoRequest, produtoId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(produtoId, resultado.Id);
        Assert.Equal(produtoRequest.Nome, resultado.Nome);
        Assert.Equal(produtoRequest.Descricao, resultado.Descricao);
        Assert.Equal(produtoRequest.Preco, resultado.Preco);
        Assert.Equal(produtoRequest.Categoria, resultado.Categoria);
    }

    [Fact]
    public void DeveConverterProdutoRequestParaDomain_SemId()
    {
        // Arrange
        var produtoRequest = new ProdutoRequest
        {
            Nome = "Refrigerante",
            Descricao = "Refrigerante gelado",
            Preco = 8.50m,
            Categoria = ProdutoCategoria.Bebida
        };

        // Act
        var resultado = ProdutoMapper.ToDomain(produtoRequest, null);

        // Assert
        Assert.NotNull(resultado);
        // O Produto cria um ID automaticamente no construtor, então não será null
        Assert.NotNull(resultado.Id);
        Assert.Equal(produtoRequest.Nome, resultado.Nome);
        Assert.Equal(produtoRequest.Descricao, resultado.Descricao);
        Assert.Equal(produtoRequest.Preco, resultado.Preco);
        Assert.Equal(produtoRequest.Categoria, resultado.Categoria);
    }

    [Fact]
    public void DeveRetornarNulo_QuandoProdutoEntityEhNulo()
    {
        // Act
        var resultado = ProdutoMapper.ToDomain((ProdutoEntity)null);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void DeveRetornarNulo_QuandoProdutoEhNulo()
    {
        // Act
        var resultado = ProdutoMapper.ToEntity((Produto)null);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void DeveRetornarNulo_QuandoProdutoRequestEhNulo()
    {
        // Act
        var resultado = ProdutoMapper.ToDomain((ProdutoRequest)null, Guid.NewGuid());

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public void DevePreservarTodasAsPropriedadesAoConverterDomainParaEntity()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        var dataAtualizacao = DateTime.UtcNow.AddHours(-1);
        var dataExclusao = DateTime.UtcNow.AddMinutes(-30);
        produto.DataAtualizacao = dataAtualizacao;
        produto.DataExclusao = dataExclusao;

        // Act
        var resultado = ProdutoMapper.ToEntity(produto);

        // Assert
        Assert.Equal(produto.DataAtualizacao, resultado.DataAtualizacao);
        Assert.Equal(produto.DataExclusao, resultado.DataExclusao);
        Assert.Equal(produto.DataCriacao, resultado.DataCriacao);
    }

    [Fact]
    public void DeveConverterCategoryCorretamente()
    {
        // Arrange
        var categorias = new[] {
            ProdutoCategoria.Lanche,
            ProdutoCategoria.Acompanhamento,
            ProdutoCategoria.Bebida,
            ProdutoCategoria.Sobremsa
        };

        foreach (var categoria in categorias)
        {
            var produtoEntity = new ProdutoEntity
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Descricao = "Teste",
                Preco = 10.00m,
                Categoria = (int)categoria,
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            // Act
            var resultado = ProdutoMapper.ToDomain(produtoEntity);

            // Assert
            Assert.Equal(categoria, resultado.Categoria);
        }
    }

    [Fact]
    public void DeveConverterAtivoProperly()
    {
        // Arrange
        var produtoAtivoEntity = new ProdutoEntity
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = (int)ProdutoCategoria.Lanche,
            Ativo = true,
            DataCriacao = DateTime.UtcNow
        };

        var produtoInativoEntity = new ProdutoEntity
        {
            Id = Guid.NewGuid(),
            Nome = "Teste",
            Descricao = "Teste",
            Preco = 10.00m,
            Categoria = (int)ProdutoCategoria.Lanche,
            Ativo = false,
            DataCriacao = DateTime.UtcNow
        };

        // Act
        var resultadoAtivo = ProdutoMapper.ToDomain(produtoAtivoEntity);
        var resultadoInativo = ProdutoMapper.ToDomain(produtoInativoEntity);

        // Assert
        Assert.True(resultadoAtivo.Ativo);
        Assert.False(resultadoInativo.Ativo);
    }

    [Fact]
    public void DeveManterPrecacaoAoConverter()
    {
        // Arrange
        var preco = 123.45m;
        var produtoRequest = new ProdutoRequest
        {
            Nome = "Teste",
            Descricao = "Teste",
            Preco = preco,
            Categoria = ProdutoCategoria.Lanche
        };

        // Act
        var resultado = ProdutoMapper.ToDomain(produtoRequest, Guid.NewGuid());

        // Assert
        Assert.Equal(preco, resultado.Preco);
    }
}
