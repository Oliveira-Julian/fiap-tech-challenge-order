using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Mapping
{
    public class ProdutoImagemMapping : IEntityTypeConfiguration<ProdutoImagemEntity>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagemEntity> builder)
        {
            builder.ToTable("PRODUTO_IMAGEM");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(o => o.IdProduto)
                .HasColumnName("ID_PRODUTO");

            builder.Property(o => o.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(o => o.Conteudo)
                .HasColumnName("CONTEUDO")
                .IsRequired();

            builder.Property(o => o.Tipo)
                .HasColumnName("TIPO")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.Tamanho)
                .HasColumnName("TAMANHO")
                .IsRequired();

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

            builder.HasOne(o => o.Produto)
                .WithMany(p => p.Imagens)
                .HasForeignKey(o => o.IdProduto)
                .HasConstraintName("FK_PRODUTO_IMAGEM_PRODUTO")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
