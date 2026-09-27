using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private readonly PizzaDbContext<T> _context;

        public EntityRepository()
        {
            _context = new PizzaDbContext<T>();

            _context.Set<T>();
            _context.Database.EnsureCreated();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = ReadById(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        public T ReadById(int id)
        {
            return _context.Set<T>()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}