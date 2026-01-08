using System.ComponentModel;

namespace FoodChallenge.Order.Domain.Enums;

public enum PagamentoMetodo
{
    [Description("Pix")]
    Pix = 1,
    [Description("Cartão Crédito")]
    CartaoCredito = 2
}
