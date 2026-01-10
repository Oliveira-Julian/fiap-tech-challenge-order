using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Clientes.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class FiltrarClienteRequestTests : TestBase
{
    private readonly Faker _faker;

    public FiltrarClienteRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var request = new FiltrarClienteRequest
        {
            Cpf = cpf
        };

        // Act & Assert
        Assert.NotNull(request);
        Assert.Equal(cpf, request.Cpf);
    }

    [Fact]
    public void DeveHerdarDeFilter()
    {
        // Arrange & Act
        var type = typeof(FiltrarClienteRequest);
        var baseType = type.BaseType;

        // Assert
        Assert.NotNull(baseType);
        Assert.Equal("Filter", baseType.Name);
    }

    [Fact]
    public void DevePermitirCpfValido()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var request = new FiltrarClienteRequest { Cpf = cpf };

        // Act & Assert
        Assert.Equal(cpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfNulo()
    {
        // Arrange
        var request = new FiltrarClienteRequest { Cpf = null };

        // Act & Assert
        Assert.Null(request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfVazio()
    {
        // Arrange
        var request = new FiltrarClienteRequest { Cpf = string.Empty };

        // Act & Assert
        Assert.Empty(request.Cpf);
    }

    [Fact]
    public void DevePermitirPaginacaoPropriedades()
    {
        // Arrange
        var request = new FiltrarClienteRequest
        {
            Page = 2,
            SizePage = 20
        };

        // Act & Assert
        Assert.Equal(2, request.Page);
        Assert.Equal(20, request.SizePage);
    }

    [Fact]
    public void DevePermitirOrdenacaoPropriedade()
    {
        // Arrange
        var request = new FiltrarClienteRequest
        {
            FieldOrdenation = "Nome"
        };

        // Act & Assert
        Assert.Equal("Nome", request.FieldOrdenation);
    }

    [Fact]
    public void DevePermitirOrdenacaoDescendentePropriedade()
    {
        // Arrange
        var request = new FiltrarClienteRequest
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
        var cpf = _faker.Person.Cpf();
        var request = new FiltrarClienteRequest
        {
            Cpf = cpf,
            Page = 1,
            SizePage = 10,
            FieldOrdenation = "Cpf"
        };

        // Act & Assert
        Assert.Equal(cpf, request.Cpf);
        Assert.Equal(1, request.Page);
        Assert.Equal(10, request.SizePage);
        Assert.Equal("Cpf", request.FieldOrdenation);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(FiltrarClienteRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDeCpf()
    {
        // Arrange
        var request = new FiltrarClienteRequest();
        var novoCpf = _faker.Person.Cpf();

        // Act
        request.Cpf = novoCpf;

        // Assert
        Assert.Equal(novoCpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirAlteracaoDePaginacao()
    {
        // Arrange
        var request = new FiltrarClienteRequest();

        // Act
        request.Page = 3;
        request.SizePage = 50;

        // Assert
        Assert.Equal(3, request.Page);
        Assert.Equal(50, request.SizePage);
    }

    [Fact]
    public void DevePermitirMultiplosCpfsSequenciados()
    {
        // Arrange
        var cpfs = new[] { _faker.Person.Cpf(), _faker.Person.Cpf(), _faker.Person.Cpf() };

        foreach (var cpf in cpfs)
        {
            var request = new FiltrarClienteRequest { Cpf = cpf };

            // Act & Assert
            Assert.Equal(cpf, request.Cpf);
        }
    }
}
