using FoodChallenge.Common.Extensions;
using FoodChallenge.Order.Application.Pagamentos.Models.Responses;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Adapter.Presenters;

public static class PagamentoPresenter
{
    public static PagamentoResponse ToResponse(Pagamento pagamento)
    {
        if (pagamento is null) return default;

        return new PagamentoResponse
        {
            QrCode = pagamento.QrCode,
            Status = (int)pagamento.Status,
            DescricaoStatus = pagamento.Status.GetDescription(),
            Id = pagamento.Id
        };
    }
}
