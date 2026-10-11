using Dapper;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий на Dapper. Работает с БД через SQL-запросы.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>
        /// Добавляет запись в таблицу Pizza.
        /// </summary>
        public void Add(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "INSERT INTO Pizza (Name, Price, Type, Size) " +
                    "VALUES (@Name, @Price, @Type, @Size)",
                    entity);
            }
        }

        /// <summary>
        /// Удаляет запись по Id.
        /// </summary>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "DELETE FROM Pizza WHERE Id = @Id",
                    new { Id = id });
            }
        }

        /// <summary>
        /// Возвращает все записи из таблицы Pizza.
        /// </summary>
        public List<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>(
                    "SELECT * FROM Pizza").ToList();
            }
        }

        /// <summary>
        /// Возвращает одну запись по Id.
        /// </summary>
        public T ReadById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<T>(
                    "SELECT * FROM Pizza WHERE Id = @Id",
                    new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет запись в таблице Pizza.
        /// </summary>
        public void Update(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    "UPDATE Pizza SET Name = @Name, Price = @Price, " +
                    "Type = @Type, Size = @Size WHERE Id = @Id",
                    entity);
            }
        }
    }
}