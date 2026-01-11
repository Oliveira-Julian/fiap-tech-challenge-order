namespace FoodChallenge.Infrastructure.Clients.Payments.Constants;

public static class Logs
{
    public const string InicioExecucao = "Executa o cliente do FoodChallenge Payments. Endpoint: {Endpoint}";
    public const string FimExecucao = "Retorno da execução do cliente do FoodChallenge Payments. Endpoint: {@Endpoint}. Response: {@Response}";
    public const string ErroResponse = "Ocorreu um erro no retorno do FoodChallenge Payments. Endpoint: {Endpoint}. StatusCode: {StatusCode}. Mensagem: {@Mensagem}";
    public const string ErroGenerico = "Ocorreu um erro ao executar o cliente do FoodChallenge Payments. Endpoint: {Endpoint}";
}
