
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using PizzaModel;

namespace PizzaLogic
{
    /// <summary>
    /// Бизнес-логика для работы со скидками.
    /// </summary>
    public class DiscountService : IDiscountLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Создаёт сервис работы со скидками.
        /// </summary>
        /// <param name="unitOfWork">Объект управления операциями с данными.</param>
        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Выполняет операцию в транзакции.
        /// </summary>
        /// <param name="operation">Выполняемая операция.</param>
        private void ExecuteInTransaction(Action operation)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                operation();
                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Возвращает все правила скидок.
        /// </summary>
        public List<SizeDiscount> ReadDiscounts()
        {
            return _unitOfWork.Discounts.ReadAll()
                .OrderBy(d => d.Size)
                .ToList();
        }

        /// <summary>
        /// Создаёт или обновляет правило скидки.
        /// </summary>
        public SizeDiscount SaveSizeDiscount(int size, decimal discountPercent)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть больше нуля.");

            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Скидка должна быть от 0 до 100 процентов.");

            SizeDiscount discount = _unitOfWork.Discounts.ReadAll()
                .FirstOrDefault(d => d.Size == size);

            if (discount == null)
            {
                discount = new SizeDiscount
                {
                    Size = size,
                    DiscountPercent = discountPercent
                };

                ExecuteInTransaction(() => _unitOfWork.Discounts.Add(discount));
            }
            else
            {
                discount.DiscountPercent = discountPercent;

                ExecuteInTransaction(() => _unitOfWork.Discounts.Update(discount));
            }

            return discount;
        }

        /// <summary>
        /// Удаляет правило скидки по идентификатору.
        /// </summary>
        public bool DeleteSizeDiscount(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный идентификатор.");

            SizeDiscount discount = _unitOfWork.Discounts.ReadById(id);

            if (discount == null)
                return false;

            ExecuteInTransaction(() => _unitOfWork.Discounts.Delete(id));
            return true;
        }

        /// <summary>
        /// Возвращает процент скидки для указанного размера.
        /// </summary>
        public decimal GetDiscountPercent(int size)
        {
            if (size <= 0)
                return 0;

            SizeDiscount discount = _unitOfWork.Discounts.ReadAll()
                .Where(d => d.Size <= size)
                .OrderByDescending(d => d.Size)
                .FirstOrDefault();

            return discount?.DiscountPercent ?? 0;
        }

        /// <summary>
        /// Рассчитывает цену пиццы с учётом скидки.
        /// </summary>
        public decimal GetDiscountedPrice(Pizza pizza)
        {
            if (pizza == null)
                throw new ArgumentNullException(nameof(pizza));

            decimal discountPercent = GetDiscountPercent(pizza.Size);

            return pizza.Price * (1 - discountPercent / 100);
        }
    }
}
