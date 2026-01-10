namespace FoodChallenge.Order.Application.Clientes.Models.Responses;

public sealed class ClienteResponse
{
    public Guid? Id { get; set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
}
