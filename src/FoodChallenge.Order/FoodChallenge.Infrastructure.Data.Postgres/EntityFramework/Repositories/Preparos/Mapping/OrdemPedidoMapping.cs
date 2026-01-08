using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos.Mapping;

public class OrdemPedidoMapping : IEntityTypeConfiguration<OrdemPedidoEntity>
{
    public void Configure(EntityTypeBuilder<OrdemPedidoEntity> builder)
    {
        builder.ToTable("ORDEM_PEDIDO");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("ID");

        builder.Property(x => x.IdPedido)
            .HasColumnName("ID_PEDIDO")
            .IsRequired();

        builder.HasOne(o => o.Pedido)
            .WithOne(p => p.OrdemPedido)
            .HasForeignKey<OrdemPedidoEntity>(x => x.IdPedido)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Status)
            .HasColumnName("STATUS");

        builder.Property(x => x.DataInicioPreparacao)
            .HasColumnName("DATA_INICIO_PREPARACAO")
            .IsRequired(false);

        builder.Property(x => x.DataFimPreparacao)
            .HasColumnName("DATA_FIM_PREPARACAO")
            .IsRequired(false);

        builder.Property(x => x.DataCriacao)
            .HasColumnName("DATA_CRIACAO")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(x => x.DataAtualizacao)
            .HasColumnName("DATA_ATUALIZACAO")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Ignore(o => o.Ativo);
        builder.Ignore(o => o.DataExclusao);
    }
}
