
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Универсальная реализация репозитория с использованием Dapper.
    /// Выполняет CRUD-операции с базой данных в рамках транзакций Unit of Work.
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject.</typeparam>
    public class DapperRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;
        private readonly Func<IDbTransaction> _getTransaction;

        /// <summary>
        /// Создаёт Dapper-репозиторий.
        /// </summary>
        /// <param name="connection">Подключение к базе данных.</param>
        /// <param name="tableName">Название таблицы базы данных.</param>
        /// <param name="getTransaction">Функция получения текущей транзакции.</param>
        public DapperRepository(
            IDbConnection connection,
            string tableName,
            Func<IDbTransaction> getTransaction)
        {
            _connection = connection;
            _tableName = tableName;
            _getTransaction = getTransaction;
        }

        /// <summary>
        /// Добавляет новую запись в базу данных.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
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

        /// <summary>
        /// Удаляет запись из базы данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи.</param>
        public void Delete(int id)
        {
            _connection.Execute(
                $"DELETE FROM {_tableName} WHERE Id = @Id",
                new { Id = id },
                transaction: _getTransaction());
        }

        /// <summary>
        /// Возвращает все записи из таблицы.
        /// </summary>
        /// <returns>Список найденных сущностей.</returns>
        public List<T> ReadAll()
        {
            return _connection.Query<T>(
                $"SELECT * FROM {_tableName}",
                transaction: _getTransaction()).ToList();
        }

        /// <summary>
        /// Возвращает запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор искомой записи.</param>
        /// <returns>Найденная сущность или null, если запись отсутствует.</returns>
        public T ReadById(int id)
        {
            return _connection.QueryFirstOrDefault<T>(
                $"SELECT * FROM {_tableName} WHERE Id = @Id",
                new { Id = id },
                transaction: _getTransaction());
        }

        /// <summary>
        /// Обновляет существующую запись в базе данных.
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными.</param>
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
