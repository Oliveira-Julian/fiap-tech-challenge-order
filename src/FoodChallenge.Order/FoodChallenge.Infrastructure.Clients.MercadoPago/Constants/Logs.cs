namespace FoodChallenge.Infrastructure.Clients.MercadoPago.Constants;

public static class Logs
{
    public const string InicioExecucao = "Executa o cliente do Mercado Pago. Endpoint: {Endpoint}";
    public const string FimExecucao = "Retorno da execução do cliente do Mercado Pago. Endpoint: {@Endpoint}. Response: {@Response}";
    public const string ErroResponse = "Ocorreu um erro no retorno do Mercado Pago. Endpoint: {Endpoint}. StatusCode: {StatusCode}. Mensagem: {@Mensagem}";
    public const string ErroGenerico = "Ocorreu um erro ao executar o cliente do Mercado Pago. Endpoint: {Endpoint}";
}
