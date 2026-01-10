using Bogus;
using FoodChallenge.Order.Domain.Produtos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class ProdutoImagemMock
{
    private static readonly string[] TiposPermitidos = {
        "image/jpeg",
        "image/jpg",
        "image/png",
        "image/webp"
    };

    public static Faker<ProdutoImagem> GetFaker()
    {
        return new Faker<ProdutoImagem>()
            .CustomInstantiator(faker => new ProdutoImagem
            {
                Id = Guid.NewGuid(),
                IdProduto = Guid.NewGuid(),
                Nome = faker.System.FileName(),
                Tipo = faker.Random.ListItem(TiposPermitidos),
                Tamanho = faker.Random.Decimal(1, 1024 * 1024),
                Conteudo = faker.Random.Bytes(faker.Random.Int(100, 50000)),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            });
    }

    public static ProdutoImagem CriarValido() => GetFaker().Generate();

    public static List<ProdutoImagem> CriarListaValida(int quantidade) => GetFaker().Generate(quantidade);
}
