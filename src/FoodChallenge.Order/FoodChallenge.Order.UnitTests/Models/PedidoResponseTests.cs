using Bogus;
using Bogus.Extensions.Brazil;
using FoodChallenge.Order.Application.Pedidos.Models.Responses;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.UnitTests.Models;

public class PedidoResponseTests : TestBase
{
    private readonly Faker _faker;

    public PedidoResponseTests()
    {
        _faker = GetFaker();
    }

    [Fact]
    public void DevePossuirPropriedadesValidas()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new PedidoResponse
        {
            Id = id,
            Codigo = _faker.Random.AlphaNumeric(10),
            ValorTotal = (decimal)_faker.Random.Double(0, 1000),
            Status = PedidoStatus.Recebido,
            DescricaoStatus = "Recebido",
            Cliente = new FoodChallenge.Order.Application.Clientes.Models.Responses.ClienteResponse(),
            Pagamento = new FoodChallenge.Order.Application.Pagamentos.Models.Responses.PagamentoResponse(),
            Itens = new List<PedidoItemResponse>()
        };

        // Act & Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
        Assert.NotNull(response.Codigo);
        Assert.True(response.ValorTotal >= 0);
        Assert.Equal(PedidoStatus.Recebido, response.Status);
        Assert.NotNull(response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var response = new PedidoResponse { Id = id };

        // Act & Assert
        Assert.Equal(id, response.Id);
    }

    [Fact]
    public void DevePermitirIdNulo()
    {
        // Arrange
        var response = new PedidoResponse { Id = null };

        // Act & Assert
        Assert.Null(response.Id);
    }

    [Fact]
    public void DevePermitirCodigoValido()
    {
        // Arrange
        var codigo = _faker.Random.AlphaNumeric(10);
        var response = new PedidoResponse { Codigo = codigo };

        // Act & Assert
        Assert.Equal(codigo, response.Codigo);
    }

    [Fact]
    public void DevePermitirCodigoNulo()
    {
        // Arrange
        var response = new PedidoResponse { Codigo = null };

        // Act & Assert
        Assert.Null(response.Codigo);
    }

    [Fact]
    public void DevePermitirValorTotalPositivo()
    {
        // Arrange
        var valorTotal = (decimal)_faker.Random.Double(0.01, 1000);
        var response = new PedidoResponse { ValorTotal = valorTotal };

        // Act & Assert
        Assert.Equal(valorTotal, response.ValorTotal);
        Assert.True(response.ValorTotal > 0);
    }

    [Fact]
    public void DevePermitirValorTotalZero()
    {
        // Arrange
        var response = new PedidoResponse { ValorTotal = 0 };

        // Act & Assert
        Assert.Equal(0, response.ValorTotal);
    }

    [Fact]
    public void DevePermitirValorTotalNulo()
    {
        // Arrange
        var response = new PedidoResponse { ValorTotal = null };

        // Act & Assert
        Assert.Null(response.ValorTotal);
    }

    [Fact]
    public void DevePermitirTodosOsStatus()
    {
        // Arrange
        var statuses = new[]
        {
            PedidoStatus.Recebido,
            PedidoStatus.NaFila,
            PedidoStatus.EmPreparacao,
            PedidoStatus.AguardandoRetirada,
            PedidoStatus.Finalizado
        };

        foreach (var status in statuses)
        {
            var response = new PedidoResponse { Status = status };

            // Act & Assert
            Assert.Equal(status, response.Status);
        }
    }

    [Fact]
    public void DevePermitirStatusNulo()
    {
        // Arrange
        var response = new PedidoResponse { Status = null };

        // Act & Assert
        Assert.Null(response.Status);
    }

    [Fact]
    public void DevePermitirDescricaoStatusValida()
    {
        // Arrange
        var descricao = "Pedido recebido com sucesso";
        var response = new PedidoResponse { DescricaoStatus = descricao };

        // Act & Assert
        Assert.Equal(descricao, response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirDescricaoStatusNula()
    {
        // Arrange
        var response = new PedidoResponse { DescricaoStatus = null };

        // Act & Assert
        Assert.Null(response.DescricaoStatus);
    }

    [Fact]
    public void DevePermitirClienteValido()
    {
        // Arrange
        var cliente = new FoodChallenge.Order.Application.Clientes.Models.Responses.ClienteResponse
        {
            Id = Guid.NewGuid(),
            Cpf = _faker.Person.Cpf(),
            Nome = _faker.Person.FullName,
            Email = _faker.Person.Email
        };
        var response = new PedidoResponse { Cliente = cliente };

        // Act & Assert
        Assert.NotNull(response.Cliente);
        Assert.Equal(cliente.Id, response.Cliente.Id);
    }

    [Fact]
    public void DevePermitirClienteNulo()
    {
        // Arrange
        var response = new PedidoResponse { Cliente = null };

        // Act & Assert
        Assert.Null(response.Cliente);
    }

    [Fact]
    public void DevePermitirPagamentoValido()
    {
        // Arrange
        var pagamento = new FoodChallenge.Order.Application.Pagamentos.Models.Responses.PagamentoResponse();
        var response = new PedidoResponse { Pagamento = pagamento };

        // Act & Assert
        Assert.NotNull(response.Pagamento);
    }

    [Fact]
    public void DevePermitirPagamentoNulo()
    {
        // Arrange
        var response = new PedidoResponse { Pagamento = null };

        // Act & Assert
        Assert.Null(response.Pagamento);
    }

    [Fact]
    public void DevePermitirItensVazios()
    {
        // Arrange
        var response = new PedidoResponse { Itens = new List<PedidoItemResponse>() };

        // Act & Assert
        Assert.NotNull(response.Itens);
        Assert.Empty(response.Itens);
    }

    [Fact]
    public void DevePermitirItensNulos()
    {
        // Arrange
        var response = new PedidoResponse { Itens = null };

        // Act & Assert
        Assert.Null(response.Itens);
    }

    [Fact]
    public void DevePermitirAlteracaoDePropriedades()
    {
        // Arrange
        var response = new PedidoResponse();
        var id = Guid.NewGuid();
        var codigo = "PED001";
        var valorTotal = 100.50m;

        // Act
        response.Id = id;
        response.Codigo = codigo;
        response.ValorTotal = valorTotal;
        response.Status = PedidoStatus.EmPreparacao;

        // Assert
        Assert.Equal(id, response.Id);
        Assert.Equal(codigo, response.Codigo);
        Assert.Equal(valorTotal, response.ValorTotal);
        Assert.Equal(PedidoStatus.EmPreparacao, response.Status);
    }

    [Fact]
    public void DevePermitirMultiplosItens()
    {
        // Arrange
        var itens = new List<PedidoItemResponse>
        {
            new PedidoItemResponse { Id = Guid.NewGuid(), Quantidade = 2, Valor = 25m },
            new PedidoItemResponse { Id = Guid.NewGuid(), Quantidade = 1, Valor = 50m },
            new PedidoItemResponse { Id = Guid.NewGuid(), Quantidade = 3, Valor = 10m }
        };
        var response = new PedidoResponse { Itens = itens };

        // Act & Assert
        Assert.NotNull(response.Itens);
        Assert.Equal(3, response.Itens.Count());
    }
}
