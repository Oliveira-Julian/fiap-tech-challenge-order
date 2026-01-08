using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Mapping
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItemEntity>
    {
        public void Configure(EntityTypeBuilder<PedidoItemEntity> builder)
        {
            builder.ToTable("PEDIDO_ITEM");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.IdPedido)
                .HasColumnName("ID_PEDIDO")
                .IsRequired();

            builder.Property(x => x.IdProduto)
                .HasColumnName("ID_PRODUTO")
                .IsRequired();

            builder.Property(x => x.Codigo)
                .HasColumnName("CODIGO")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasColumnName("QUANTIDADE")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("VALOR");

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

            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(x => x.IdPedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
