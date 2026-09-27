using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class, IDomainObject
    {
        void Add(T entity);
        void Delete(int id);
        List<T> ReadAll();
        T ReadById(int id);
        void Update(T entity);
    }
}