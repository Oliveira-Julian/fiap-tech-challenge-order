namespace FoodChallenge.Infrastructure.Clients.Kitchens.Constants;

public static class Logs
{
    public const string InicioExecucao = "Executa o cliente do FoodChallenge Kitchens. Endpoint: {Endpoint}";
    public const string FimExecucao = "Retorno da execução do cliente do FoodChallenge Kitchens. Endpoint: {@Endpoint}. Response: {@Response}";
    public const string ErroResponse = "Ocorreu um erro no retorno do FoodChallenge Kitchens. Endpoint: {Endpoint}. StatusCode: {StatusCode}. Mensagem: {@Mensagem}";
    public const string ErroGenerico = "Ocorreu um erro ao executar o cliente do FoodChallenge Kitchens. Endpoint: {Endpoint}";
}
