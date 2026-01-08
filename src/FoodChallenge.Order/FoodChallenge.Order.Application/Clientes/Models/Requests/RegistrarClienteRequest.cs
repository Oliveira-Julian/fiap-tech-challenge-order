namespace FoodChallenge.Order.Application.Clientes.Models.Requests
{
    public sealed class RegistrarClienteRequest
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
