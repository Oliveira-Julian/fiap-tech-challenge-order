using Bogus;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class PagamentoMock
{
    public static Faker<Pagamento> GetFaker()
    {
        return new Faker<Pagamento>()
            .CustomInstantiator(faker => new Pagamento(
                id: Guid.NewGuid(),
                statusCodigo: (int)PagamentoStatus.Aprovado,
                qrCode: faker.Random.String2(32))
            );
    }

    public static Pagamento CriarValido() => GetFaker().Generate();

    public static List<Pagamento> CriarListaValida(int quantidade) => GetFaker().Generate(quantidade);
}
