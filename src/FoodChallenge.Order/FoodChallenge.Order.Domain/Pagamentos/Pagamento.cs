using FoodChallenge.Order.Domain.Entities;
using FoodChallenge.Order.Domain.Enums;

namespace FoodChallenge.Payment.Domain.Pagamentos;

public class Pagamento : DomainBase
{
    public PagamentoStatus Status { get; set; }
    public PagamentoMetodo Metodo { get; set; }
    public string QrCode { get; set; }

    public Pagamento(Guid? id, int statusCodigo, string qrCode)
    {
        Id = id;
        QrCode = qrCode;
        Metodo = PagamentoMetodo.Pix;
        Status = (PagamentoStatus)statusCodigo;
    }
}
