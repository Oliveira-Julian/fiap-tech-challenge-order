using Bogus;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Produtos.Specifications;

public class ProdutoAtualizacaoSpecificationTests : TestBase
{
    private readonly Faker _faker;
    private readonly ProdutoAtualizacaoSpecification _specification;

    public ProdutoAtualizacaoSpecificationTests()
    {
        _faker = GetFaker();
        _specification = new ProdutoAtualizacaoSpecification();
    }

    [Fact]
    public async Task DeveSerValida_QuandoTodosCamposEIdPreenchidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
        Assert.Empty(result.Responses);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoIdEhNulo()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Id = null;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Id)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoCategoriaEstaVazia()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Categoria = 0; // Categoria inválida

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Categoria)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoNomeEstaVazio()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = string.Empty;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Nome)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoNomeEhNulo()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = null;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Nome)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoDescricaoEstaVazia()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Descricao = string.Empty;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Descricao)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoDescricaoEhNula()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Descricao = null;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Descricao)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoPrecoEhZero()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Preco = 0;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Preco)), mensagens);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoPrecoENegativo()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Preco = -50.00m;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Preco)), mensagens);
    }

    [Fact]
    public async Task DeveSerValida_QuandoAtualizandoApenasCamposPermitidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = "Novo Nome do Produto";
        produto.Preco = 150.00m;
        produto.Descricao = "Nova descrição";

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerValida_QuandoIdEPrecoSaoValidos()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Id = Guid.NewGuid();
        produto.Preco = 199.99m;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoIdEOutrosCamposEstaoVazios()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Id = null;
        produto.Nome = string.Empty;
        produto.Descricao = string.Empty;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.True(result.Responses.Count() > 1);
    }

    [Fact]
    public async Task DeveSerValida_QuandoTodosCamposVaoemCadastroeAtualizacao()
    {
        // Arrange
        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Produto Válido",
            Descricao = "Uma descrição válida",
            Categoria = ProdutoCategoria.Acompanhamento,
            Preco = 25.50m
        };

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }
}
