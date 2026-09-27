using Dapper;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=PizzaDB;Trusted_Connection=True;TrustServerCertificate=True;";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
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
        /// 
        /// </summary>
        /// <param name="id"></param>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<T> ReadAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<T>(
                    "SELECT * FROM Pizza").ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
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