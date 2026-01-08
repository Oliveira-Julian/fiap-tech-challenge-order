using System.ComponentModel;

namespace FoodChallenge.Order.Domain.Enums;

public enum PagamentoStatus
{
    [Description("Aprovado")]
    Aprovado = 1,
    [Description("Recusado")]
    Recusado = 2,
    [Description("Pendente")]
    Pendente = 3,
    [Description("Não Identificado")]
    NaoIdentificado = 4
}
