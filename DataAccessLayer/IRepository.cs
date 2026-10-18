using System.Collections.Generic;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория для CRUD-операций.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <param name="entity">Что добавляем.</param>
        void Add(T entity);

        /// <summary>
        /// Удалить сущность по Id.
        /// </summary>
        /// <param name="id">Id для удаления.</param>
        void Delete(int id);

        /// <summary>
        /// Получить все сущности.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        List<T> ReadAll();

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id">Id для поиска.</param>
        /// <returns>Сущность или null.</returns>
        T ReadById(int id);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность с новыми данными.</param>
        void Update(T entity);
    }
}