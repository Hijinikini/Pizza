
using System;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс Unit of Work.
    /// Обеспечивает централизованное управление репозиториями
    /// и транзакциями при работе с базой данных.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Возвращает репозиторий для работы с пиццами.
        /// </summary>
        IRepository<Pizza> Pizzas { get; }

        /// <summary>
        /// Возвращает репозиторий для работы со скидками.
        /// </summary>
        IRepository<SizeDiscount> Discounts { get; }

        /// <summary>
        /// Начинает новую транзакцию.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Подтверждает транзакцию и сохраняет изменения.
        /// </summary>
        void Commit();

        /// <summary>
        /// Отменяет транзакцию и выполняет откат изменений.
        /// </summary>
        void Rollback();
    }
}
