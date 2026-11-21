
namespace PizzaLogic
{
    /// <summary>
    /// Общий интерфейс бизнес-логики приложения.
    /// Объединяет операции работы с пиццами и скидками.
    /// </summary>
    public interface ILogic : IPizzaLogic, IDiscountLogic
    {
    }
}
