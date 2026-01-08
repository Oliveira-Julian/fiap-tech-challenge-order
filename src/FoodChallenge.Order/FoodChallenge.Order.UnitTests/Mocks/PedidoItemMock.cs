using Bogus;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class PedidoItemMock
{
    public static Faker<PedidoItem> GetFaker()
    {
        return new Faker<PedidoItem>()
            .CustomInstantiator(faker =>
            {
                return new PedidoItem
                {
                    IdProduto = Guid.NewGuid(),
                    Valor = faker.Random.Decimal(10, 100),
                    Quantidade = faker.Random.Int(1, 5),
                    Codigo = faker.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                };
            });
    }

    public static PedidoItem CriarValido()
    {
        return GetFaker().Generate();
    }

    public static List<PedidoItem> CriarListaValida(int quantidade)
    {
        return GetFaker().Generate(quantidade);
    }
}
