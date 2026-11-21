
using System.Collections.Generic;
using PizzaModel;

namespace PizzaLogic
{
    /// <summary>
    /// Интерфейс бизнес-логики для работы со скидками.
    /// Определяет операции получения, сохранения и удаления скидок,
    /// а также расчёта стоимости пиццы с учётом скидки.
    /// </summary>
    public interface IDiscountLogic
    {
        /// <summary>
        /// Возвращает список всех правил скидок.
        /// </summary>
        List<SizeDiscount> ReadDiscounts();

        /// <summary>
        /// Создаёт новое правило скидки или обновляет существующее.
        /// </summary>
        SizeDiscount SaveSizeDiscount(int size, decimal discountPercent);

        /// <summary>
        /// Удаляет правило скидки по идентификатору.
        /// </summary>
        bool DeleteSizeDiscount(int id);

        /// <summary>
        /// Возвращает процент скидки для указанного размера.
        /// </summary>
        decimal GetDiscountPercent(int size);

        /// <summary>
        /// Рассчитывает стоимость пиццы с учётом действующей скидки.
        /// </summary>
        decimal GetDiscountedPrice(Pizza pizza);
    }
}
