using Bogus;
using FoodChallenge.Order.Application.Produtos.Specifications;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Globalization;
using FoodChallenge.Order.Domain.Produtos;
using FoodChallenge.Order.UnitTests.Mocks;

namespace FoodChallenge.Order.UnitTests.Produtos.Specifications;

public class ProdutoCadastroSpecificationTests : TestBase
{
    private readonly Faker _faker;
    private readonly ProdutoCadastroSpecification _specification;

    public ProdutoCadastroSpecificationTests()
    {
        _faker = GetFaker();
        _specification = new ProdutoCadastroSpecification();
    }

    [Fact]
    public async Task DeveSerValida_QuandoTodosCamposObrigatoriosPreenchidos()
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
    public async Task DeveSerInvalida_QuandoCategoriaEstaVazia()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Categoria = 0; // Categoria inválida (padrão de enum)

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
        produto.Preco = -10;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        var mensagens = result.Responses.SelectMany(m => m.Mensagens).ToList();
        Assert.Contains(string.Format(Textos.CampoObrigatorio, nameof(Produto.Preco)), mensagens);
    }

    [Fact]
    public async Task DeveSerValida_QuandoPrecoEhPositivo()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Preco = 99.99m;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerValida_QuandoCategoriaEhLanche()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Categoria = ProdutoCategoria.Lanche;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerValida_QuandoCategoriaEhBebida()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Categoria = ProdutoCategoria.Bebida;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.True(result.Valid);
    }

    [Fact]
    public async Task DeveSerInvalida_QuandoMultiplosCamposEstaVazios()
    {
        // Arrange
        var produto = ProdutoMock.CriarValido();
        produto.Nome = string.Empty;
        produto.Descricao = string.Empty;
        produto.Preco = 0;

        // Act
        var result = await _specification.ValidateModelAsync(produto, CancellationToken.None);

        // Assert
        Assert.False(result.Valid);
        Assert.True(result.Responses.Count() > 1);
    }
}
