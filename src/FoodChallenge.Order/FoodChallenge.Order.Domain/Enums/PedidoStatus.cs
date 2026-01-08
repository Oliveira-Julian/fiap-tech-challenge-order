using System.ComponentModel;

namespace FoodChallenge.Order.Domain.Enums;

public enum PedidoStatus
{
    [Description("Recebido")]
    Recebido = 1,
    [Description("Na fila para preparação")]
    NaFila = 2,
    [Description("Em preparação")]
    EmPreparacao = 3,
    [Description("Aguardando retirada")]
    AguardandoRetirada = 4,
    [Description("Finalizado")]
    Finalizado = 5
}
