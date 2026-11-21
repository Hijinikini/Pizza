
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация Unit of Work с использованием Dapper.
    /// Управляет общим подключением к базе данных,
    /// репозиториями и транзакциями.
    /// </summary>
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Репозиторий для работы с пиццами.
        /// </summary>
        public IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Репозиторий для работы со скидками.
        /// </summary>
        public IRepository<SizeDiscount> Discounts { get; }

        /// <summary>
        /// Создаёт Unit of Work, открывает подключение к базе данных
        /// и инициализирует репозитории.
        /// </summary>
        public DapperUnitOfWork()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            Pizzas = new DapperRepository<Pizza>(
                _connection, "Pizza", () => _transaction);

            Discounts = new DapperRepository<SizeDiscount>(
                _connection, "SizeDiscounts", () => _transaction);
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException(
                    "Транзакция уже начата.");

            _transaction = _connection.BeginTransaction();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            if (_transaction == null)
                return;

            try
            {
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

            _connection.Dispose();
        }
    }
}
