
using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer
{
    /// <summary>
    /// Unit of Work для Entity Framework.
    /// Управляет транзакциями.
    /// </summary>
    public class EntityUnitOfWork : IUnitOfWork
    {
        private readonly PizzaDbContext _context;
        private IDbContextTransaction _transaction;

        public EntityUnitOfWork(PizzaDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException(
                    "Транзакция уже начата.");

            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                return;

            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
                return;

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            
        }
    }
}
