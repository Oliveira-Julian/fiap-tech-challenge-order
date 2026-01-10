using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Clientes.Models.Requests;

namespace FoodChallenge.Order.UnitTests.Models;

public class RegistrarClienteRequestTests : TestBase
{
    private readonly Faker _faker;

    public RegistrarClienteRequestTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var request = new RegistrarClienteRequest
        {
            Cpf = cpf,
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.NotNull(request);
        Assert.Equal(cpf, request.Cpf);
        Assert.NotNull(request.Nome);
        Assert.NotNull(request.Email);
    }

    [Fact]
    public void DevePermitirCpfValido()
    {
        // Arrange
        var cpf = _faker.Person.Cpf();
        var request = new RegistrarClienteRequest
        {
            Cpf = cpf,
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Equal(cpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfVazio()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = string.Empty,
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Empty(request.Cpf);
    }

    [Fact]
    public void DevePermitirCpfNulo()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = null,
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Null(request.Cpf);
    }

    [Fact]
    public void DevePermitirNomeValido()
    {
        // Arrange
        var nome = _faker.Person.FullName;
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = nome,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Equal(nome, request.Nome);
    }

    [Fact]
    public void DevePermitirNomeVazio()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = string.Empty,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Empty(request.Nome);
    }

    [Fact]
    public void DevePermitirNomeNulo()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = null,
            Email = _faker.Person.Email
        };

        // Act & Assert
        Assert.Null(request.Nome);
    }

    [Fact]
    public void DevePermitirEmailValido()
    {
        // Arrange
        var email = _faker.Person.Email;
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = _faker.Person.FullName,
            Email = email
        };

        // Act & Assert
        Assert.Equal(email, request.Email);
    }

    [Fact]
    public void DevePermitirEmailVazio()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = _faker.Person.FullName,
            Email = string.Empty
        };

        // Act & Assert
        Assert.Empty(request.Email);
    }

    [Fact]
    public void DevePermitirEmailNulo()
    {
        // Arrange
        var request = new RegistrarClienteRequest
        {
            Cpf = _faker.Person.Cpf(),
            Nome = _faker.Person.FullName,
            Email = null
        };

        // Act & Assert
        Assert.Null(request.Email);
    }

    [Fact]
    public void DevePermitirAlteracaoDeCpf()
    {
        // Arrange
        var request = new RegistrarClienteRequest();
        var novoCpf = _faker.Person.Cpf();

        // Act
        request.Cpf = novoCpf;

        // Assert
        Assert.Equal(novoCpf, request.Cpf);
    }

    [Fact]
    public void DevePermitirAlteracaoDeNome()
    {
        // Arrange
        var request = new RegistrarClienteRequest();
        var novoNome = _faker.Person.FullName;

        // Act
        request.Nome = novoNome;

        // Assert
        Assert.Equal(novoNome, request.Nome);
    }

    [Fact]
    public void DevePermitirAlteracaoDeEmail()
    {
        // Arrange
        var request = new RegistrarClienteRequest();
        var novoEmail = _faker.Person.Email;

        // Act
        request.Email = novoEmail;

        // Assert
        Assert.Equal(novoEmail, request.Email);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(RegistrarClienteRequest);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirEmailComFormatosVariados()
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
            var request = new RegistrarClienteRequest
            {
                Cpf = _faker.Person.Cpf(),
                Nome = _faker.Person.FullName,
                Email = email
            };

            // Act & Assert
            Assert.Equal(email, request.Email);
        }
    }
}
