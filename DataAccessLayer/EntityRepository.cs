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
        /// Создаёт контекст БД и гарантирует, что БД существует.
        /// </summary>
        public EntityRepository()
        {
            _context = new PizzaDbContext();

            _context.Set<T>();
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// Добавляет запись в БД.
        /// </summary>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
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
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает все записи из БД.
        /// </summary>
        public List<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Возвращает одну запись по Id.
        /// </summary>
        public T ReadById(int id)
        {
            return _context.Set<T>()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Обновляет запись в БД.
        /// </summary>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}