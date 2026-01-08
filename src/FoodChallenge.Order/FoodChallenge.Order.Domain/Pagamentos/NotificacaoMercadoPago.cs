using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Order.Domain.Pagamentos;

public class NotificacaoMercadoPago
{
    public string Id { get; set; }
    public string Tipo { get; set; }
    public string Acao { get; set; }
    public PagamentoStatus Status => ObterStatusPagamento(Acao?.Replace("payment.", ""));
    public DateTime DataCriacao { get; set; }

    public bool VerificarAcaoEhValida()
    {
        if (string.IsNullOrWhiteSpace(Acao))
            return false;

        var status = ObterStatusPagamento(Acao.Replace("payment.", ""));

        if (status == PagamentoStatus.NaoIdentificado)
            return false;

        return true;
    }

    public static PagamentoStatus ObterStatusPagamento(string status)
    {
        return status switch
        {
            "processed" => PagamentoStatus.Aprovado,
            "canceled" => PagamentoStatus.Recusado,
            "expired" => PagamentoStatus.Recusado,
            "refunded" => PagamentoStatus.Recusado,
            "created" => PagamentoStatus.Pendente,
            _ => PagamentoStatus.NaoIdentificado
        };
    }
}
