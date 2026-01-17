using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pagamentos.Mapping;

public class PedidoPagamentoMapping : IEntityTypeConfiguration<PagamentoEntity>
{
    public void Configure(EntityTypeBuilder<PagamentoEntity> builder)
    {
        builder.ToTable("PEDIDO_PAGAMENTO");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("ID");

        builder.Property(x => x.IdPedido)
            .HasColumnName("ID_PEDIDO");

        builder.Property(x => x.Status)
            .HasColumnName("STATUS");

        builder.Property(x => x.DataCriacao)
            .HasColumnName("DATA_CRIACAO")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(x => x.DataAtualizacao)
            .HasColumnName("DATA_ATUALIZACAO")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.HasOne(p => p.Pedido)
            .WithOne(p => p.Pagamento)
            .HasForeignKey<PagamentoEntity>(p => p.IdPedido)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(x => x.Ativo);
        builder.Ignore(x => x.DataExclusao);
    }
}
