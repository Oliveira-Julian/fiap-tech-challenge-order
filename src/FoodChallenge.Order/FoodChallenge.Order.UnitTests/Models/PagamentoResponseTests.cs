using Bogus;
using FoodChallenge.Order.Application.Pagamentos.Models.Responses;

namespace FoodChallenge.Order.UnitTests.Models;

public class PagamentoResponseTests : TestBase
{
    private readonly Faker _faker;

    public PagamentoResponseTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var qrCode = _faker.Random.AlphaNumeric(100);
        var status = _faker.Random.Int(0, 10);
        var response = new PagamentoResponse
        {
            QrCode = qrCode,
            Status = status,
            DescricaoStatus = "Pagamento pendente",
            IdMercadoPagoPagamento = _faker.Random.AlphaNumeric(50)
        };

        // Act & Assert
        Assert.NotNull(response);
        Assert.Equal(qrCode, response.QrCode);
        Assert.Equal(status, response.Status);
        Assert.NotNull(response.DescricaoStatus);
        Assert.NotNull(response.IdMercadoPagoPagamento);
    }

    [Fact]
    public void DevePermitirQrCodeValido()
    {
        // Arrange
        var qrCode = _faker.Random.AlphaNumeric(100);
        var response = new PagamentoResponse { QrCode = qrCode };

        // Act & Assert
        Assert.Equal(qrCode, response.QrCode);
    }

    [Fact]
    public void DevePermitirQrCodeNulo()
    {
        // Arrange
        var response = new PagamentoResponse { QrCode = null };

        // Act & Assert
        Assert.Null(response.QrCode);
    }

    [Fact]
    public void DevePermitirQrCodeVazio()
    {
        // Arrange
        var response = new PagamentoResponse { QrCode = string.Empty };

        // Act & Assert
        Assert.Empty(response.QrCode);
    }

    [Fact]
    public void DevePermitirStatusValido()
    {
        // Arrange
        var status = _faker.Random.Int(0, 100);
        var response = new PagamentoResponse { Status = status };

        // Act & Assert
        Assert.Equal(status, response.Status);
    }

    [Fact]
    public void DevePermitirStatusZero()
    {
        // Arrange
        var response = new PagamentoResponse { Status = 0 };

        // Act & Assert
        Assert.Equal(0, response.Status);
    }

    [Fact]
    public void DevePermitirStatusNegativo()
    {
        // Arrange
        var response = new PagamentoResponse { Status = -1 };

        // Act & Assert
        Assert.Equal(-1, response.Status);
    }

    [Fact]
    public void DevePermitirDescricaoStatusValida()
    {
        // Arrange
        var descricao = "Pagamento aprovado";
        var response = new PagamentoResponse { DescricaoStatus = descricao };

        // Act & Assert
        Assert.Equal(descricao, response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirDescricaoStatusNula()
    {
        // Arrange
        var response = new PagamentoResponse { DescricaoStatus = null };

        // Act & Assert
        Assert.Null(response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirDescricaoStatusVazia()
    {
        // Arrange
        var response = new PagamentoResponse { DescricaoStatus = string.Empty };

        // Act & Assert
        Assert.Empty(response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirIdMercadoPagoValido()
    {
        // Arrange
        var idMercadoPago = _faker.Random.AlphaNumeric(50);
        var response = new PagamentoResponse { IdMercadoPagoPagamento = idMercadoPago };

        // Act & Assert
        Assert.Equal(idMercadoPago, response.IdMercadoPagoPagamento);
    }

    [Fact]
    public void DevePermitirIdMercadoPagoNulo()
    {
        // Arrange
        var response = new PagamentoResponse { IdMercadoPagoPagamento = null };

        // Act & Assert
        Assert.Null(response.IdMercadoPagoPagamento);
    }

    [Fact]
    public void DevePermitirIdMercadoPagoVazio()
    {
        // Arrange
        var response = new PagamentoResponse { IdMercadoPagoPagamento = string.Empty };

        // Act & Assert
        Assert.Empty(response.IdMercadoPagoPagamento);
    }

    [Fact]
    public void DeveSerSealedClass()
    {
        // Arrange & Act
        var type = typeof(PagamentoResponse);

        // Assert
        Assert.True(type.IsSealed);
    }

    [Fact]
    public void DevePermitirAlteracaoDeTodasAsPropriedades()
    {
        // Arrange
        var response = new PagamentoResponse();
        var qrCode = _faker.Random.AlphaNumeric(100);
        var status = 1;
        var descricao = "Pagamento processado";
        var idMercadoPago = _faker.Random.AlphaNumeric(50);

        // Act
        response.QrCode = qrCode;
        response.Status = status;
        response.DescricaoStatus = descricao;
        response.IdMercadoPagoPagamento = idMercadoPago;

        // Assert
        Assert.Equal(qrCode, response.QrCode);
        Assert.Equal(status, response.Status);
        Assert.Equal(descricao, response.DescricaoStatus);
        Assert.Equal(idMercadoPago, response.IdMercadoPagoPagamento);
    }

    [Fact]
    public void DevePermitirMultiplosStatus()
    {
        // Arrange
        var statuses = new[] { 0, 1, 2, 5, 10, 100 };

        foreach (var status in statuses)
        {
            var response = new PagamentoResponse { Status = status };

            // Act & Assert
            Assert.Equal(status, response.Status);
        }
    }
}
