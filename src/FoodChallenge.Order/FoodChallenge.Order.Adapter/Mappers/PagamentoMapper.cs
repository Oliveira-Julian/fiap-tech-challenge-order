using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;
using FoodChallenge.Order.Domain.Enums;
using FoodChallenge.Payment.Domain.Pagamentos;

namespace FoodChallenge.Order.Adapter.Mappers;

public static class PagamentoMapper
{
    public static Pagamento ToDomain(PagamentoEntity pagamentoEntity)
    {
        if (pagamentoEntity is null) return default;

        return new Pagamento
        {
            Id = pagamentoEntity.Id,
            IdPedido = pagamentoEntity.IdPedido,
            IdMercadoPagoOrdem = pagamentoEntity.IdMercadoPagoOrdem,
            IdMercadoPagoPagamento = pagamentoEntity.IdMercadoPagoPagamento,
            DataAtualizacao = pagamentoEntity.DataAtualizacao,
            DataCriacao = pagamentoEntity.DataCriacao,
            DataExclusao = pagamentoEntity.DataExclusao,
            Ativo = pagamentoEntity.Ativo,
            ChaveMercadoPagoOrdem = pagamentoEntity.ChaveMercadoPagoOrdem,
            Metodo = (PagamentoMetodo)pagamentoEntity.Metodo,
            QrCode = pagamentoEntity.QrCode,
            Status = (PagamentoStatus)pagamentoEntity.Status,
            Valor = pagamentoEntity.Valor
        };
    }

    public static PagamentoEntity ToEntity(Pagamento pagamento)
    {
        if (pagamento is null) return default;

        return new PagamentoEntity
        {
            Id = pagamento.Id,
            IdPedido = pagamento.IdPedido,
            IdMercadoPagoOrdem = pagamento.IdMercadoPagoOrdem,
            IdMercadoPagoPagamento = pagamento.IdMercadoPagoPagamento,
            DataAtualizacao = pagamento.DataAtualizacao,
            DataCriacao = pagamento.DataCriacao,
            DataExclusao = pagamento.DataExclusao,
            Ativo = pagamento.Ativo,
            ChaveMercadoPagoOrdem = pagamento.ChaveMercadoPagoOrdem,
            Metodo = (int)pagamento.Metodo,
            QrCode = pagamento.QrCode,
            Status = (int)pagamento.Status,
            Valor = pagamento.Valor
        };
    }
}
