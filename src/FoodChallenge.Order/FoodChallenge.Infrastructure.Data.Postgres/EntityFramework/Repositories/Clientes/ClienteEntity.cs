using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Bases;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;

public sealed class ClienteEntity : EntityBase
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public ICollection<PedidoEntity> Pedidos { get; set; }
}
