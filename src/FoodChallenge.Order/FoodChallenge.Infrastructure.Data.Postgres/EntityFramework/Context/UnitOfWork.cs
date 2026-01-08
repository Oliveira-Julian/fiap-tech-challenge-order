using FoodChallenge.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Context
{
    public class UnitOfWork(DbContext context) : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private bool _disposed = false;

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                    await _transaction.RollbackAsync();
            }
            catch { /* ignora caso ocorra alguma exceção */ }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            try
            {
                if (_transaction != null)
                    _transaction.Dispose();
            }
            catch { /* ignora caso ocorra alguma exceção */ }

            // Não dispose _context se ele for gerenciado por injeção de dependência
            _disposed = true;
        }
    }

}

