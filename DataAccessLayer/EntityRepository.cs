
using System.Collections.Generic;
using System.Linq;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Реализация универсального репозитория с использованием Entity Framework Core.
    /// Обеспечивает выполнение CRUD-операций через DbContext.
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующей IDomainObject.</typeparam>
    public class EntityRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly PizzaDbContext _context;

        /// <summary>
        /// Создаёт репозиторий с указанным контекстом базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EntityRepository(PizzaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новую запись в контекст базы данных.
        /// Сохранение выполняется через Unit of Work.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Удаляет запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой записи.</param>
        public void Delete(int id)
        {
            T entity = ReadById(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        /// <summary>
        /// Возвращает список всех записей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        public List<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Возвращает запись по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор искомой записи.</param>
        /// <returns>Найденная сущность или null, если запись отсутствует.</returns>
        public T ReadById(int id)
        {
            return _context.Set<T>()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Обновляет запись в контексте базы данных.
        /// Сохранение выполняется через Unit of Work.
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными.</param>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
