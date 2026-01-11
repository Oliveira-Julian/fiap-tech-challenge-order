using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Clientes.Models.Responses;

namespace FoodChallenge.Order.UnitTests.Models;

public class ClienteResponseTests : TestBase
{
    private readonly Faker _faker;

    public ClienteResponseTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ClienteResponse
        {
            Id = id,
            Cpf = _faker.Person.Cpf(),
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
        Assert.NotNull(response.Cpf);
        Assert.NotNull(response.Nome);
        Assert.NotNull(response.Email);
    }

    [Fact]
    public void DevePermitirIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new ClienteResponse { Id = id };

        // Act & Assert
        Assert.Equal(id, response.Id);
    }

    [Fact]
    public void DevePermitirIdNulo()
    {
        // Arrange
        var response = new ClienteResponse { Id = null };

        // Act & Assert
        Assert.Null(response.Id);
    }

    [Fact]
    public void DevePermitirCpfValido()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var response = new ClienteResponse { Cpf = cpf };

        // Act & Assert
        Assert.Equal(cpf, response.Cpf);
    }

    [Fact]
    public void DevePermitirCpfNulo()
    {
        // Arrange
        var response = new ClienteResponse { Cpf = null };

        // Act & Assert
        Assert.Null(response.Cpf);
    }

    [Fact]
    public void DevePermitirCpfVazio()
    {
        // Arrange
        var response = new ClienteResponse { Cpf = string.Empty };

        // Act & Assert
        Assert.Empty(response.Cpf);
    }

    [Fact]
    public void DevePermitirNomeValido()
    {
        // Arrange
        var nome = _faker.Person.FullName;
        var response = new ClienteResponse { Nome = nome };

        // Act & Assert
        Assert.Equal(nome, response.Nome);
    }

    [Fact]
    public void DevePermitirNomeNulo()
    {
        // Arrange
        var response = new ClienteResponse { Nome = null };

        // Act & Assert
        Assert.Null(response.Nome);
    }

    [Fact]
    public void DevePermitirNomeVazio()
    {
        // Arrange
        var response = new ClienteResponse { Nome = string.Empty };

        // Act & Assert
        Assert.Empty(response.Nome);
    }

    [Fact]
    public void DevePermitirEmailValido()
    {
        // Arrange
        var email = _faker.Person.Email;
        var response = new ClienteResponse { Email = email };

        // Act & Assert
        Assert.Equal(email, response.Email);
    }

    [Fact]
    public void DevePermitirEmailNulo()
    {
        // Arrange
        var response = new ClienteResponse { Email = null };

        // Act & Assert
        Assert.Null(response.Email);
    }

    [Fact]
    public void DevePermitirEmailVazio()
    {
        // Arrange
        var response = new ClienteResponse { Email = string.Empty };

        // Act & Assert
        Assert.Empty(response.Email);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(ClienteResponse);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDeTodasAsPropriedades()
    {
        // Arrange
        var response = new ClienteResponse();
        var id = Guid.NewGuid();
        var cpf = _faker.Person.Cpf();
        var nome = _faker.Person.FullName;
        var email = _faker.Person.Email;

        // Act
        response.Id = id;
        response.Cpf = cpf;
        response.Nome = nome;
        response.Email = email;

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(cpf, response.Cpf);
        Assert.Equal(nome, response.Nome);
        Assert.Equal(email, response.Email);
    }

    [Fact]
    public void DevePermitirEmailsVariados()
    {
        // Arrange
        var emails = new[]
        {
            "teste@example.com",
            "usuario+tag@domain.co.uk",
            "nome.sobrenome@company.com.br",
            "test.email.with+symbol@example4u.net"
        };

        foreach (var email in emails)
        {
            var response = new ClienteResponse { Email = email };

            // Act & Assert
            Assert.Equal(email, response.Email);
        }
    }
}
