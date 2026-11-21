
namespace PizzaLogic
{
    /// <summary>
    /// Общий интерфейс бизнес-логики приложения.
    /// Объединяет операции работы с пиццами и скидками.
    /// </summary>
    public interface ILogic : IPizzaLogic, IDiscountLogic
    {
        /// <summary>
        /// Событие уведомления об изменении списка пицц.
        /// </summary>
        event System.EventHandler? PizzasChanged;
        /// <summary>
        /// Событие уведомления об изменении правил скидок.
        /// </summary>
        event System.EventHandler? DiscountsChanged;
    }
}
