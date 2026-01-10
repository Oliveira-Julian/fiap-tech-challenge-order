using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<ProdutoEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
        {
            builder.ToTable("PRODUTO");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME");

            builder.Property(x => x.Descricao)
                .HasColumnName("DESCRICAO");

            builder.Property(x => x.Preco)
                .HasColumnName("PRECO");

            builder.Property(x => x.Categoria)
                .HasColumnName("CATEGORIA");

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

            builder.HasMany(p => p.Imagens)
                .WithOne(pi => pi.Produto)
                .HasForeignKey(pi => pi.IdProduto)
                .HasConstraintName("FK_PRODUTO_PRODUTO_IMAGEM")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
