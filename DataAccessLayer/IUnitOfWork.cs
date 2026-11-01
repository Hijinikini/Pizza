using System;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для Unit of Work.
    /// Нужен, чтобы управлять транзакциями и репозиториями.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Репозиторий для работы с пиццами.
        /// </summary>
        IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Репозиторий для работы со скидками.
        /// </summary>
        IRepository<SizeDiscount> Discounts { get; }

        /// <summary>
        /// Начать транзакцию.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Сохранить изменения (зафиксировать транзакцию).
        /// </summary>
        void Commit();

        /// <summary>
        /// Отменить изменения (откатить транзакцию).
        /// </summary>
        void Rollback();
    }
}