using System.Text.RegularExpressions;

namespace FoodChallenge.Order.Domain.Clientes.ValueObjects;

public sealed class Email
{
    public string Valor { get; }

    public Email() { }

    public Email(string valor)
    {
        Valor = string.IsNullOrWhiteSpace(valor) ? string.Empty : valor.Trim().ToLowerInvariant();
    }

    public bool EhValido()
    {
        if (string.IsNullOrWhiteSpace(Valor))
            return false;

        var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return regex.IsMatch(Valor);
    }
}
