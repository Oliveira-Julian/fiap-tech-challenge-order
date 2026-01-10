namespace FoodChallenge.Order.Domain.Constants;

/// <summary>
/// Define as mensagens de log geradas pelo cliente.
/// </summary>
public static class Logs
{
    /// <summary>
    /// Log quando inicia a execução de um serviço.
    /// </summary>
    public const string InicioExecucaoServico = "Executa o método {Service}.{Method}";

    /// <summary>
    /// Log quando finaliza a execução de um serviço.
    /// </summary>
    public const string FimExecucaoServico = "Retorno da execução do método {Service}.{Method}. Response: {@Response}";

    /// <summary>
    /// Log quando ocorre um erro genérico na execução de um serviço.
    /// </summary>
    public const string ErroGenerico = "Ocorreu um erro ao executar o método {Service}.{Method}";
}
