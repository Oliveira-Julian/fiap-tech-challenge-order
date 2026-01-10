using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class CadastrarPedidoRequestTests : TestBase
{
    private readonly Faker _faker;

    public CadastrarPedidoRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var itens = new List<PedidoItemRequest>
        {
            new PedidoItemRequest { IdProduto = Guid.NewGuid(), Quantidade = 2 }
        };
        var request = new CadastrarPedidoRequest
        {
            Cpf = _faker.Person.Cpf(),
            Itens = itens
        };

        // Act & Assert
        Assert.NotNull(request);
        Assert.NotNull(request.Cpf);
        Assert.NotNull(request.Itens);
        Assert.Single(request.Itens);
    }

    [Fact]
    public void DevePermitirCpfValido()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var request = new CadastrarPedidoRequest
        {
            Cpf = cpf,
            Itens = new List<PedidoItemRequest>()
        };

        // Act & Assert
        Assert.Equal(cpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfVazio()
    {
        // Arrange
        var request = new CadastrarPedidoRequest
        {
            Cpf = string.Empty,
            Itens = new List<PedidoItemRequest>()
        };

        // Act & Assert
        Assert.Empty(request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfNulo()
    {
        // Arrange
        var request = new CadastrarPedidoRequest
        {
            Cpf = null,
            Itens = new List<PedidoItemRequest>()
        };

        // Act & Assert
        Assert.Null(request.Cpf);
    }

    [Fact]
    public void DevePermitirMultiplosItens()
    {
        // Arrange
        var itens = new List<PedidoItemRequest>
        {
            new PedidoItemRequest { IdProduto = Guid.NewGuid(), Quantidade = 2 },
            new PedidoItemRequest { IdProduto = Guid.NewGuid(), Quantidade = 1 },
            new PedidoItemRequest { IdProduto = Guid.NewGuid(), Quantidade = 3 }
        };
        var request = new CadastrarPedidoRequest
        {
            Cpf = _faker.Person.Cpf(),
            Itens = itens
        };

        // Act & Assert
        Assert.Equal(3, request.Itens.Count());
    }

    [Fact]
    public void DevePermitirItensVazio()
    {
        // Arrange
        var request = new CadastrarPedidoRequest
        {
            Cpf = _faker.Person.Cpf(),
            Itens = Enumerable.Empty<PedidoItemRequest>()
        };

        // Act & Assert
        Assert.Empty(request.Itens);
    }

    [Fact]
    public void DevePermitirItensNulo()
    {
        // Arrange
        var request = new CadastrarPedidoRequest
        {
            Cpf = _faker.Person.Cpf(),
            Itens = null
        };

        // Act & Assert
        Assert.Null(request.Itens);
    }

    [Fact]
    public void DevePermitirAlteracaoDeCpf()
    {
        // Arrange
        var request = new CadastrarPedidoRequest();
        var novoCpf = _faker.Person.Cpf();

        // Act
        request.Cpf = novoCpf;

        // Assert
        Assert.Equal(novoCpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirAlteracaoDeItens()
    {
        // Arrange
        var request = new CadastrarPedidoRequest();
        var itens = new List<PedidoItemRequest>
        {
            new PedidoItemRequest { IdProduto = Guid.NewGuid(), Quantidade = 5 }
        };

        // Act
        request.Itens = itens;

        // Assert
        Assert.NotNull(request.Itens);
        Assert.Single(request.Itens);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(CadastrarPedidoRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirMuitosItens()
    {
        // Arrange
        var itens = Enumerable.Range(1, 100)
            .Select(i => new PedidoItemRequest
            {
                IdProduto = Guid.NewGuid(),
                Quantidade = i
            })
            .ToList();
        var request = new CadastrarPedidoRequest
        {
            Cpf = _faker.Person.Cpf(),
            Itens = itens
        };

        // Act & Assert
        Assert.Equal(100, request.Itens.Count());
    }
}
