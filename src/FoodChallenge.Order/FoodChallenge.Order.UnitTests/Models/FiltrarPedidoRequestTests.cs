using Bogus;
using FoodChallenge.Order.Application.Pedidos.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class FiltrarPedidoRequestTests : TestBase
{
    private readonly Faker _faker;

    public FiltrarPedidoRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var idCliente = Guid.NewGuid();
        var request = new FiltrarPedidoRequest
        {
            Id = id,
            IdCliente = idCliente
        };

        // Act & Assert
        Assert.NotNull(request);
        Assert.Equal(id, request.Id);
        Assert.Equal(idCliente, request.IdCliente);
    }

    [Fact]
    public void DeveHerdarDeFilter()
    {
        // Arrange & Act
        var type = typeof(FiltrarPedidoRequest);
        var baseType = type.BaseType;

        // Assert
        Assert.NotNull(baseType);
        Assert.Equal("Filter", baseType.Name);
    }

    [Fact]
    public void DevePermitirIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new FiltrarPedidoRequest { Id = id };

        // Act & Assert
        Assert.Equal(id, request.Id);
    }

    [Fact]
    public void DevePermitirIdNulo()
    {
        // Arrange
        var request = new FiltrarPedidoRequest { Id = null };

        // Act & Assert
        Assert.Null(request.Id);
    }

    [Fact]
    public void DevePermitirIdClienteValido()
    {
        // Arrange
        var idCliente = Guid.NewGuid();
        var request = new FiltrarPedidoRequest { IdCliente = idCliente };

        // Act & Assert
        Assert.Equal(idCliente, request.IdCliente);
    }

    [Fact]
    public void DevePermitirIdClienteNulo()
    {
        // Arrange
        var request = new FiltrarPedidoRequest { IdCliente = null };

        // Act & Assert
        Assert.Null(request.IdCliente);
    }

    [Fact]
    public void DevePermitirPaginacaoPropriedades()
    {
        // Arrange
        var request = new FiltrarPedidoRequest
        {
            Page = 2,
            SizePage = 15
        };

        // Act & Assert
        Assert.Equal(2, request.Page);
        Assert.Equal(15, request.SizePage);
    }

    [Fact]
    public void DevePermitirOrdenacaoPropriedade()
    {
        // Arrange
        var request = new FiltrarPedidoRequest
        {
            FieldOrdenation = "Codigo"
        };

        // Act & Assert
        Assert.Equal("Codigo", request.FieldOrdenation);
    }

    [Fact]
    public void DevePermitirOrdenacaoDescendentePropriedade()
    {
        // Arrange
        var request = new FiltrarPedidoRequest
        {
            OrdenationAsc = false
        };

        // Act & Assert
        Assert.False(request.OrdenationAsc);
    }

    [Fact]
    public void DevePermitirCombinacaoDeFiltrosAndPaginacao()
    {
        // Arrange
        var id = Guid.NewGuid();
        var idCliente = Guid.NewGuid();
        var request = new FiltrarPedidoRequest
        {
            Id = id,
            IdCliente = idCliente,
            Page = 1,
            SizePage = 10,
            FieldOrdenation = "Codigo"
        };

        // Act & Assert
        Assert.Equal(id, request.Id);
        Assert.Equal(idCliente, request.IdCliente);
        Assert.Equal(1, request.Page);
        Assert.Equal(10, request.SizePage);
        Assert.Equal("Codigo", request.FieldOrdenation);
    }

    [Fact]
    public void DevePermitirApenasIdCliente()
    {
        // Arrange
        var idCliente = Guid.NewGuid();
        var request = new FiltrarPedidoRequest
        {
            Id = null,
            IdCliente = idCliente
        };

        // Act & Assert
        Assert.Null(request.Id);
        Assert.Equal(idCliente, request.IdCliente);
    }

    [Fact]
    public void DevePermitirApenasId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new FiltrarPedidoRequest
        {
            Id = id,
            IdCliente = null
        };

        // Act & Assert
        Assert.Equal(id, request.Id);
        Assert.Null(request.IdCliente);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(FiltrarPedidoRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDeTodasAsPropriedades()
    {
        // Arrange
        var request = new FiltrarPedidoRequest();
        var novoId = Guid.NewGuid();
        var novoIdCliente = Guid.NewGuid();

        // Act
        request.Id = novoId;
        request.IdCliente = novoIdCliente;
        request.Page = 5;
        request.SizePage = 25;

        // Assert
        Assert.Equal(novoId, request.Id);
        Assert.Equal(novoIdCliente, request.IdCliente);
        Assert.Equal(5, request.Page);
        Assert.Equal(25, request.SizePage);
    }
}
