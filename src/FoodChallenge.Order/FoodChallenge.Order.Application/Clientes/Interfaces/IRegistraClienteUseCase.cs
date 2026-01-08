using FoodChallenge.Order.Domain.Clientes;

namespace FoodChallenge.Order.Application.Clientes.Interfaces;

public interface IRegistraClienteUseCase
{
    Task<Cliente> ExecutarAsync(Cliente cliente, CancellationToken cancellationToken);
}
