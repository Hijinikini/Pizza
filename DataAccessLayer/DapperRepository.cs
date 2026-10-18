using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Универсальный Dapper-репозиторий.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        private readonly string _tableName;

        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Создаёт репозиторий с внешним соединением и транзакцией.
        /// </summary>
        public DapperRepository(IDbConnection connection, IDbTransaction transaction, string tableName)
        {
            _connection = connection;
            _transaction = transaction;
            _tableName = tableName;
        }

        /// <summary>
        /// Создаёт репозиторий с собственным соединением (без транзакции).
        /// </summary>
        public DapperRepository(string tableName)
        {
            _connection = new SqlConnection(_connectionString);
            _transaction = null;
            _tableName = tableName;
        }

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        public void Add(T entity)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => p.Name)
                .ToList();

            var columns = string.Join(", ", props);
            var parameters = string.Join(", ", props.Select(p => "@" + p));

            EnsureOpen();
            _connection.Execute(
                $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})",
                entity, _transaction);
        }

        /// <summary>
        /// Удаляет сущность по Id.
        /// </summary>
        public void Delete(int id)
        {
            EnsureOpen();
            _connection.Execute(
                $"DELETE FROM {_tableName} WHERE Id = @Id",
                new { Id = id }, _transaction);
        }

        /// <summary>
        /// Возвращает все сущности.
        /// </summary>
        public List<T> ReadAll()
        {
            EnsureOpen();
            return _connection.Query<T>($"SELECT * FROM {_tableName}", null, _transaction).ToList();
        }

        /// <summary>
        /// Возвращает сущность по Id.
        /// </summary>
        public T ReadById(int id)
        {
            EnsureOpen();
            return _connection.QueryFirstOrDefault<T>(
                $"SELECT * FROM {_tableName} WHERE Id = @Id",
                new { Id = id }, _transaction);
        }

        /// <summary>
        /// Обновляет сущность.
        /// </summary>
        public void Update(T entity)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => p.Name)
                .ToList();

            var setClause = string.Join(", ", props.Select(p => $"{p} = @{p}"));

            EnsureOpen();
            _connection.Execute(
                $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id",
                entity, _transaction);
        }

        /// <summary>
        /// Открывает соединение, если оно закрыто.
        /// </summary>
        private void EnsureOpen()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }
    }
}