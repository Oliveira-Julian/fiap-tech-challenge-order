namespace FoodChallenge.Order.Domain.Extensions;

public static class StringExtensions
{
    public static string GetRandomCode(int tamanho = 6)
    {
        const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable
            .Range(0, tamanho)
            .Select(_ => caracteresPermitidos[random.Next(caracteresPermitidos.Length)])
            .ToArray());
    }
}
