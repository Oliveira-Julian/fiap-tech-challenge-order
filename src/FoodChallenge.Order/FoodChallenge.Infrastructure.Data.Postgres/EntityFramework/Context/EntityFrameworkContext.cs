using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Clientes.Mapping;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Pedidos.Mapping;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Preparos.Mapping;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Mapping;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context
{
    public sealed class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext() { }

        public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options) : base(options) { }

        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<PedidoEntity> Pedido { get; set; }
        public DbSet<ProdutoEntity> Produto { get; set; }
        public DbSet<ProdutoImagemEntity> ProdutoImagem { get; set; }
        public DbSet<PagamentoEntity> PedidoPagamento { get; set; }
        public DbSet<OrdemPedidoEntity> OrdemPedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.LogTo(message => Debug.WriteLine(message));
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.WithExpressionExpanding();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMapping());
            modelBuilder.ApplyConfiguration(new PedidoMapping());
            modelBuilder.ApplyConfiguration(new PedidoItemMapping());
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
            modelBuilder.ApplyConfiguration(new ProdutoImagemMapping());
            modelBuilder.ApplyConfiguration(new PedidoPagamentoMapping());
            modelBuilder.ApplyConfiguration(new OrdemPedidoMapping());
        }
    }
}
