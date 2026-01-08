using System.ComponentModel;

namespace FoodChallenge.Order.Domain.Enums;

public enum ProdutoCategoria
{
    [Description("Lanche")]
    Lanche = 1,
    [Description("Acompanhamento")]
    Acompanhamento = 2,
    [Description("Bebida")]
    Bebida = 3,
    [Description("Sobremsa")]
    Sobremsa = 4
}
