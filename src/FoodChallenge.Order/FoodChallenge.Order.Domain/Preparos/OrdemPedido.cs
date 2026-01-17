using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Domain.Preparos;

public class OrdemPedido : DomainBase
{
    public Guid IdPedido { get; set; }
    public PreparoStatus Status { get; set; }
    public DateTime? DataInicioPreparacao { get; set; }
    public DateTime? DataFimPreparacao { get; set; }

    public OrdemPedido(Guid id, Guid idPedido, int status, DateTime? dataInicioPreparacao, DateTime? datafimPreparacao)
    {
        Id = id;
        IdPedido = idPedido;
        Status = (PreparoStatus)(status);
        DataInicioPreparacao = dataInicioPreparacao;
        DataFimPreparacao = datafimPreparacao;
    }
}
