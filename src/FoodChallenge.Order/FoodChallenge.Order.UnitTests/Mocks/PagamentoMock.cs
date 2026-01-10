using Bogus;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class PagamentoMock
{
    public static Faker<Pagamento> GetFaker()
    {
        return new Faker<Pagamento>()
            .CustomInstantiator(faker => new Pagamento
            {
                Id = Guid.NewGuid(),
                QrCode = faker.Random.String2(32),
                Valor = faker.Random.Decimal(10, 200),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            });
    }

    public static Pagamento CriarValido() => GetFaker().Generate();

    public static List<Pagamento> CriarListaValida(int quantidade) => GetFaker().Generate(quantidade);
}
