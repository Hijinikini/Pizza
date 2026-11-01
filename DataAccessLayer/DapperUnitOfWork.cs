
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Unit of Work для Dapper.
    /// Управляет общим соединением и транзакцией.
    /// </summary>
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public IRepository<Pizza> Pizzas { get; }
        public IRepository<SizeDiscount> Discounts { get; }

        public DapperUnitOfWork()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            Pizzas = new DapperRepository<Pizza>(
                _connection, "Pizza", () => _transaction);

            Discounts = new DapperRepository<SizeDiscount>(
                _connection, "SizeDiscounts", () => _transaction);
        }

        /// <summary>
        /// Начинает транзакцию.
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new InvalidOperationException(
                    "Транзакция уже начата.");

            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Фиксирует изменения транзакции.
        /// </summary>
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

        /// <summary>
        /// Отменяет изменения транзакции.
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
        /// Освобождает транзакцию и соединение.
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
            _connection.Dispose();
        }
    }
}
