
using System.Collections.Generic;
using PizzaModel;

namespace PizzaLogic
{
    /// <summary>
    /// Интерфейс бизнес-логики для работы с пиццами.
    /// Определяет операции создания, чтения, изменения,
    /// удаления и обработки данных пицц.
    /// </summary>
    public interface IPizzaLogic
    {
        /// <summary>
        /// Создаёт новую пиццу.
        /// </summary>
        Pizza Create(string name, decimal price, bool type, int size);

        /// <summary>
        /// Возвращает пиццу по идентификатору.
        /// </summary>
        Pizza Read(int id);

        /// <summary>
        /// Возвращает список всех пицц.
        /// </summary>
        List<Pizza> ReadAll();

        /// <summary>
        /// Обновляет данные пиццы.
        /// </summary>
        bool Update(int id, string name, decimal price, bool type, int size);

        /// <summary>
        /// Удаляет пиццу по идентификатору.
        /// </summary>
        bool Delete(int id);

        /// <summary>
        /// Возвращает пиццы в заданном диапазоне цен.
        /// </summary>
        List<Pizza> FilterByPrice(decimal minPrice, decimal maxPrice);

        /// <summary>
        /// Возвращает статистику по всем пиццам.
        /// </summary>
        string GetStatistics();
    }
}
