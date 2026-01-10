using System.Text.RegularExpressions;

namespace FoodChallenge.Order.Domain.Clientes.ValueObjects;

public class Cpf
{
    public string Valor { get; }

    protected Cpf() { }

    public Cpf(string valor)
    {
        Valor = string.IsNullOrEmpty(valor) ? string.Empty : RemoverMascara(valor);
    }

    public static string RemoverMascara(string cpfComMascara)
    {
        return Regex.Replace(cpfComMascara ?? string.Empty, "[^0-9]", "");
    }

    public bool EhValido()
    {
        var cpf = RemoverMascara(Valor);

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        var multiplicador1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf[..9];
        var soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith($"{digito1}{digito2}");
    }
    public override string ToString() =>
        !string.IsNullOrWhiteSpace(Valor) ? Convert.ToUInt64(Valor).ToString(@"000\.000\.000\-00") : string.Empty;

    public override bool Equals(object obj) =>
        obj is Cpf other && Valor == other.Valor;

    public override int GetHashCode() =>
        Valor?.GetHashCode() ?? 0;
}
