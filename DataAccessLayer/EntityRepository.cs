
using System.Collections.Generic;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий на Entity Framework Core.
    /// Работает с БД через DbContext.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class EntityRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly PizzaDbContext _context;

        /// <summary>
        /// Создаёт репозиторий с указанным контекстом.
        /// </summary>
        public EntityRepository(PizzaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет запись.
        /// Сохранение выполняется через Unit of Work.
        /// </summary>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Удаляет запись по Id.
        /// </summary>
        public void Delete(int id)
        {
            T entity = ReadById(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        public List<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Возвращает запись по Id.
        /// </summary>
        public T ReadById(int id)
        {
            return _context.Set<T>()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Обновляет запись.
        /// Сохранение выполняется через Unit of Work.
        /// </summary>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
