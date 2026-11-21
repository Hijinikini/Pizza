
using System.Collections.Generic;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Универсальный интерфейс репозитория.
    /// Определяет основные CRUD-операции для работы с сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject.</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для добавления.</param>
        void Add(T entity);

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности.</param>
        void Delete(int id);

        /// <summary>
        /// Возвращает список всех сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        List<T> ReadAll();

        /// <summary>
        /// Возвращает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор искомой сущности.</param>
        /// <returns>Найденная сущность или null, если она отсутствует.</returns>
        T ReadById(int id);

        /// <summary>
        /// Обновляет существующую сущность.
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными.</param>
        void Update(T entity);
    }
}
