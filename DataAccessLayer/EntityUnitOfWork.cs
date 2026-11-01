
using System;
using Microsoft.EntityFrameworkCore.Storage;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Unit of Work для Entity Framework Core.
    /// </summary>
    public class EntityUnitOfWork : IUnitOfWork
    {
        private readonly PizzaDbContext _context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Репозиторий пицц.
        /// </summary>
        public IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Репозиторий скидок.
        /// </summary>
        public IRepository<SizeDiscount> Discounts { get; }

        public EntityUnitOfWork()
        {
            _context = new PizzaDbContext();

            _context.Database.EnsureCreated();

            Pizzas = new EntityRepository<Pizza>(_context);
            Discounts = new EntityRepository<SizeDiscount>(_context);
        }

        /// <summary>
        /// Начинает транзакцию.
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException(
                    "Транзакция уже начата.");

            _transaction = _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Сохраняет изменения и фиксирует транзакцию,
        /// если она была начата.
        /// </summary>
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        /// <summary>
        /// Отменяет транзакцию.
        /// </summary>
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

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _context.Dispose();
        }
    }
}
