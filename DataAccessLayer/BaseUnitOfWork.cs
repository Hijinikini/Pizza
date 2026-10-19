using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый класс для Unit of Work.
    /// Содержит общие свойства репозиториев.
    /// </summary>
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        public IRepository<Pizza> Pizzas { get; protected set; }
        public IRepository<SizeDiscount> Discounts { get; protected set; }

        public abstract void BeginTransaction();
        public abstract void Commit();
        public abstract void Rollback();
        public abstract void Dispose();
    }
}