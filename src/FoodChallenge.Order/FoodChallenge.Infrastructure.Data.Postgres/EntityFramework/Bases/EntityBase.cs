namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;

public class EntityBase
{
    public EntityBase()
    {
    }

    public Guid? Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public bool Ativo { get; set; }
    public DateTime? DataExclusao { get; set; }
}
