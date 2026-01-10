namespace FoodChallenge.Order.Domain.Entities;

public class DomainBase
{
    public DomainBase()
    {
    }

    public Guid? Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime? DataExclusao { get; set; }

    public void Atualizar() => DataAtualizacao = DateTime.UtcNow;

    public void Excluir()
    {
        Ativo = false;
        DataExclusao = DateTime.UtcNow;
        DataAtualizacao = DateTime.UtcNow;
    }
}
