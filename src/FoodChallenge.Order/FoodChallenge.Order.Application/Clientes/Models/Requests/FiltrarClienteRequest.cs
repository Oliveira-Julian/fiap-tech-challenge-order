using FoodChallenge.CrossCutting.Paging;

namespace FoodChallenge.Order.Application.Clientes.Models.Requests;

public sealed class FiltrarClienteRequest : Filter
{
    public string Cpf { get; set; }
}
