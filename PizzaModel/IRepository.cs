using System.Collections.Generic;

namespace PizzaModel
{
    /// <summary>
    /// Обобщённый интерфейс репозитория с CRUD-операциями:
    /// Add (добавить), Delete (удалить), ReadAll (получить все),
    /// ReadById (получить по Id), Update (обновить).
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующий IDomainObject.</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        void Add(T entity);
        void Delete(int id);
        List<T> ReadAll();
        T ReadById(int id);
        void Update(T entity);
    }
}