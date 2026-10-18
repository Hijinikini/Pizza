using System;
using Microsoft.EntityFrameworkCore.Storage;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Unit of Work на Entity Framework Core.
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

        /// <summary>
        /// Создаёт Unit of Work на EF Core.
        /// </summary>
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
            _transaction = _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Сохраняет все изменения.
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// Откатывает все изменения.
        /// </summary>
        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// Освобождает ресурсы.
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}