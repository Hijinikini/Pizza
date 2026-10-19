using System;
using System.Data;
using Microsoft.Data.SqlClient;
using PizzaModel;

namespace DataAccessLayer
{
    public class DapperUnitOfWork : BaseUnitOfWork 
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public DapperUnitOfWork()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            Pizzas = new DapperRepository<Pizza>(_connection, "Pizza");
            Discounts = new DapperRepository<SizeDiscount>(_connection, "SizeDiscounts");
        }

        public override void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public override void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public override void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public override void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}