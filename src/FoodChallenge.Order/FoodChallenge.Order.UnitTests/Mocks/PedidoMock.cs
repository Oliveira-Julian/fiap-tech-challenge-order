using Bogus;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pedidos;

namespace FoodChallenge.Order.UnitTests.Mocks;

public static class PedidoMock
{
    public static Faker<Pedido> GetFaker(IEnumerable<PedidoItem> itens = null)
    {
        return new Faker<Pedido>()
            .CustomInstantiator(faker =>
            {
                var pedido = new Pedido
                {
                    Id = Guid.NewGuid(),
                    Status = PedidoStatus.Recebido,
                    Itens = itens ?? new List<PedidoItem>(),
                };

                pedido.Ativo = true;
                pedido.DataCriacao = DateTime.UtcNow;
                pedido.Codigo = faker.Random.String2(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
                pedido.ValorTotal = pedido.Itens.Sum(i => i.Valor * i.Quantidade);
                return pedido;
            });
    }

    public static Pedido CriarValido(IEnumerable<PedidoItem> itens = null)
    {
        return GetFaker(itens).Generate();
    }

    public static Pedido CriarComStatus(PedidoStatus status, IEnumerable<PedidoItem> itens = null)
    {
        var pedido = CriarValido(itens);
        pedido.Status = status;
        return pedido;
    }

    public static List<Pedido> CriarListaValida(int quantidade, IEnumerable<PedidoItem> itens = null)
    {
        return GetFaker(itens).Generate(quantidade);
    }

}
