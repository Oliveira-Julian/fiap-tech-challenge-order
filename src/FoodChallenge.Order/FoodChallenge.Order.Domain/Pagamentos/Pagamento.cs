using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Payment.Domain.Pagamentos;

public class Pagamento : DomainBase
{
    public Guid? IdPedido { get; set; }
    public Guid? ChaveMercadoPagoOrdem { get; set; }
    public string IdMercadoPagoOrdem { get; set; }
    public string IdMercadoPagoPagamento { get; set; }
    public PagamentoStatus Status { get; set; }
    public decimal Valor { get; set; }
    public PagamentoMetodo Metodo { get; set; }
    public string QrCode { get; set; }

    public void Cadastrar()
    {
        Id = Guid.NewGuid();
        Status = PagamentoStatus.Pendente;
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarStatus(PagamentoStatus status)
    {
        Status = status;
        Atualizar();
    }
}
