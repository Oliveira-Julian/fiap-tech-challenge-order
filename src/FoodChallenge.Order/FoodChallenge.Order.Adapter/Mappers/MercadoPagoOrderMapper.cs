using FoodChallenge.Common.Extensions;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Models;
using FoodChallenge.Infrastructure.Clients.MercadoPago.Settings;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Order.Domain.Pagamentos;
using FoodChallenge.Order.Domain.Pedidos;
using FoodChallenge.Payment.Domain.Pagamentos;
using System.Globalization;
using MercadoPago = FoodChallenge.Infrastructure.Clients.MercadoPago.Models;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class MercadoPagoOrderMapper
{
    private const string Type = "qr";
    private const string Mode = "dynamic";
    private const string ExpirationTime = "PT20M";
    private const string UnitMeasure = "kg";

    public static CreateOrderRequest ToRequest(Pedido pedido, MercadoPagoSettings settings)
    {
        if (pedido is null) return default;

        var order = new CreateOrderRequest
        {
            Type = Type,
            TotalAmount = pedido.ValorTotal.ToString("F2", CultureInfo.InvariantCulture),
            Description = $"Pedido {pedido.Id} realizado no FoodChallenge",
            ExternalReference = pedido.Codigo,
            ExpirationTime = ExpirationTime,
            Config = new Config
            {
                Qr = new Qr
                {
                    ExternalPosId = settings.CaixaCodigo,
                    Mode = Mode
                }
            },
            Transactions = new Transactions
            {
                Payments =
                [
                    new MercadoPago.Payment { Amount = pedido.ValorTotal.ToString("F2", CultureInfo.InvariantCulture) }
                ]
            },
            Items = pedido.Itens?.Select(pedidoItem =>
                {
                    var item = new Item
                    {
                        Title = pedidoItem.Produto.Nome,
                        UnitPrice = pedidoItem.Valor.ToString("F2", CultureInfo.InvariantCulture),
                        Quantity = pedidoItem.Quantidade,
                        UnitMeasure = UnitMeasure,
                        ExternalCode = pedidoItem.Codigo,
                        ExternalCategories =
                        [
                            new() { Id = pedidoItem.Produto.Categoria.GetDescription() }
                        ]
                    };

                    return item;
                })
        };

        return order;
    }

    public static Pagamento ToDomain(OrderResponse orderResponse, Guid orderId, Guid idPedido)
    {
        if (orderResponse is null) return default;

        var totalAmount = decimal.Parse(orderResponse.TotalAmount, CultureInfo.InvariantCulture);

        var pagamento = new Pagamento()
        {
            IdPedido = idPedido,
            ChaveMercadoPagoOrdem = orderId,
            IdMercadoPagoOrdem = orderResponse.Id,
            IdMercadoPagoPagamento = orderResponse.Transactions?.Payments?.Select(payment => payment.Id)?.FirstOrDefault(),
            Status = NotificacaoMercadoPago.ObterStatusPagamento(orderResponse.Status),
            Valor = totalAmount,
            Metodo = PagamentoMetodo.CartaoCredito,
            QrCode = orderResponse.TypeResponse?.QrData
        };
        pagamento.Cadastrar();

        return pagamento;
    }

    public static Pagamento ToDomain(OrderResponse orderResponse)
    {
        if (orderResponse is null) return default;

        var totalAmount = decimal.Parse(orderResponse.TotalAmount, CultureInfo.InvariantCulture);

        var pagamento = new Pagamento()
        {
            IdMercadoPagoOrdem = orderResponse.Id,
            IdMercadoPagoPagamento = orderResponse.Transactions?.Payments?.Select(payment => payment.Id)?.FirstOrDefault(),
            Status = NotificacaoMercadoPago.ObterStatusPagamento(orderResponse.Status),
            Valor = totalAmount,
            Metodo = PagamentoMetodo.CartaoCredito,
            QrCode = orderResponse.TypeResponse?.QrData
        };

        return pagamento;
    }
}
