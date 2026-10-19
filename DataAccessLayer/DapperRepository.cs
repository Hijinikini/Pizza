using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Универсальный Dapper-репозиторий.
    /// </summary>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;

        public DapperRepository(IDbConnection connection, string tableName)
        {
            _connection = connection;
            _tableName = tableName;
        }

        public void Add(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            var columns = string.Join(", ", props);
            var parameters = string.Join(", ", props.Select(p => "@" + p));

            _connection.Execute(
                $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})",
                entity);
        }

        public void Delete(int id)
        {
            _connection.Execute($"DELETE FROM {_tableName} WHERE Id = @Id", new { Id = id });
        }

        public List<T> ReadAll()
        {
            return _connection.Query<T>($"SELECT * FROM {_tableName}").ToList();
        }

        public T ReadById(int id)
        {
            return _connection.QueryFirstOrDefault<T>($"SELECT * FROM {_tableName} WHERE Id = @Id", new { Id = id });
        }

        public void Update(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name).ToList();
            var setClause = string.Join(", ", props.Select(p => $"{p} = @{p}"));

            _connection.Execute(
                $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id",
                entity);
        }
    }
}