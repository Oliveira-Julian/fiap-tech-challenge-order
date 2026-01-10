using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("CLIENTE");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder
                .Property(x => x.Cpf)
                .HasColumnName("CPF");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME");

            builder
                .Property(x => x.Email)
                .HasColumnName("EMAIL");

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
