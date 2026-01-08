using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context;
using FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos.Interfaces;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Repositories.Produtos;

public class ProdutoRepository(EntityFrameworkContext context)
    : RepositoryBase<ProdutoEntity>(context), IProdutoRepository
{
}