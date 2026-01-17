using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Mapping
{
    public class PedidoMapping : IEntityTypeConfiguration<PedidoEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoEntity> builder)
        {
            builder.ToTable("PEDIDO");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.IdCliente)
                .HasColumnName("ID_CLIENTE")
                .IsRequired();

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IdPagamento)
                .HasColumnName("ID_PAGAMENTO")
                .IsRequired(false);

            builder.HasOne(p => p.Pagamento)
                .WithOne(pag => pag.Pedido)
                .HasForeignKey<PedidoEntity>(x => x.IdPagamento)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Codigo)
                .HasColumnName("CODIGO")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("STATUS");

            builder.Property(x => x.ValorTotal)
                .HasColumnName("VALOR_TOTAL");

            builder.Property(x => x.DataCriacao)
                .HasColumnName("DATA_CRIACAO")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.DataAtualizacao)
                .HasColumnName("DATA_ATUALIZACAO")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasColumnName("ATIVO")
                .HasDefaultValue(false);

            builder.Property(x => x.DataExclusao)
                .HasColumnName("DATA_EXCLUSAO");
        }
    }
}
