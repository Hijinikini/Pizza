using System;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Единица работы (Unit of Work).
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий пицц.
        /// </summary>
        IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Репозиторий скидок.
        /// </summary>
        IRepository<SizeDiscount> Discounts { get; }

        /// <summary>
        /// Начать транзакцию.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатить изменения.
        /// </summary>
        void Rollback();
    }
}