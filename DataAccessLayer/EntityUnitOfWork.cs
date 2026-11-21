
using System;
using Microsoft.EntityFrameworkCore.Storage;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация Unit of Work с использованием Entity Framework Core.
    /// Управляет репозиториями, сохранением изменений и транзакциями.
    /// </summary>
    public class EntityUnitOfWork : IUnitOfWork
    {
        private readonly PizzaDbContext _context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Репозиторий для работы с пиццами.
        /// </summary>
        public IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Репозиторий для работы со скидками.
        /// </summary>
        public IRepository<SizeDiscount> Discounts { get; }

        /// <summary>
        /// Создаёт Unit of Work с указанным контекстом базы данных.
        /// Инициализирует репозитории и проверяет наличие базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EntityUnitOfWork(PizzaDbContext context)
        {
            _context = context;

            _context.Database.EnsureCreated();

            Pizzas = new EntityRepository<Pizza>(_context);
            Discounts = new EntityRepository<SizeDiscount>(_context);
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException(
                    "Транзакция уже начата.");

            _transaction = _context.Database.BeginTransaction();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _context.Dispose();
        }
    }
}
