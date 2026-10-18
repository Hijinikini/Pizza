using System;
using System.Data;
using Microsoft.Data.SqlClient;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Unit of Work для Dapper. Держит одно соединение для всех репозиториев.
    /// </summary>
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;

        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Репозиторий пицц.
        /// </summary>
        public IRepository<Pizza> Pizzas { get; private set; }

        /// <summary>
        /// Репозиторий скидок.
        /// </summary>
        public IRepository<SizeDiscount> Discounts { get; private set; }

        /// <summary>
        /// Открывает соединение и создаёт репозитории.
        /// </summary>
        public DapperUnitOfWork()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            Pizzas = new DapperRepository<Pizza>(_connection, null, "Pizza");
            Discounts = new DapperRepository<SizeDiscount>(_connection, null, "SizeDiscounts");
        }

        /// <summary>
        /// Начинает транзакцию (для Dapper не используется).
        /// </summary>
        public void BeginTransaction()
        {
            // Dapper работает без явных транзакций
        }

        /// <summary>
        /// Для Dapper не нужен — изменения сохраняются сразу.
        /// </summary>
        public void Commit()
        {
            // Dapper сохраняет изменения сразу при Execute
        }

        /// <summary>
        /// Откатывает изменения (для Dapper не используется).
        /// </summary>
        public void Rollback()
        {
            // Dapper не поддерживает откат
        }

        /// <summary>
        /// Закрывает соединение.
        /// </summary>
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}