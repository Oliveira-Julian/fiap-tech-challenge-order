using Bogus;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class ProdutoMock
{
    public static Faker<Produto> GetFaker()
    {
        return new Faker<Produto>()
            .CustomInstantiator(faker => new Produto
            {
                Id = Guid.NewGuid(),
                Categoria = faker.Random.Enum<ProdutoCategoria>(),
                Nome = faker.Commerce.ProductName(),
                Descricao = faker.Commerce.ProductAdjective(),
                Preco = faker.Random.Decimal(5, 100),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            });
    }

    public static Produto CriarValido() => GetFaker().Generate();

    public static List<Produto> CriarListaValida(int quantidade) => GetFaker().Generate(quantidade);
}
