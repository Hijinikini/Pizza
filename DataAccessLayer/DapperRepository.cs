
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Универсальный Dapper-репозиторий.
    /// Выполняет операции с данными в рамках транзакции Unit of Work.
    /// </summary>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;
        private readonly System.Func<IDbTransaction> _getTransaction;

        public DapperRepository(
            IDbConnection connection,
            string tableName,
            System.Func<IDbTransaction> getTransaction)
        {
            _connection = connection;
            _tableName = tableName;
            _getTransaction = getTransaction;
        }

        public void Add(T entity)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .ToList();

            var columns = string.Join(", ", props.Select(p => p.Name));
            var parameters = string.Join(", ", props.Select(p => "@" + p.Name));

            _connection.Execute(
                $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})",
                entity,
                transaction: _getTransaction());
        }

        public void Delete(int id)
        {
            _connection.Execute(
                $"DELETE FROM {_tableName} WHERE Id = @Id",
                new { Id = id },
                transaction: _getTransaction());
        }

        public List<T> ReadAll()
        {
            return _connection.Query<T>(
                $"SELECT * FROM {_tableName}",
                transaction: _getTransaction()).ToList();
        }

        public T ReadById(int id)
        {
            return _connection.QueryFirstOrDefault<T>(
                $"SELECT * FROM {_tableName} WHERE Id = @Id",
                new { Id = id },
                transaction: _getTransaction());
        }

        public void Update(T entity)
        {
            var props = typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .ToList();

            var setClause = string.Join(
                ", ",
                props.Select(p => $"{p.Name} = @{p.Name}"));

            _connection.Execute(
                $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id",
                entity,
                transaction: _getTransaction());
        }
    }
}
