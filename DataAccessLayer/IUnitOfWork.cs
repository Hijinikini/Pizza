using System;
using PizzaModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс для Unit of Work.
    /// Нужен, чтобы управлять транзакциями
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {

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