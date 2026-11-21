
using System.Collections.Generic;
using PizzaModel;

namespace PizzaLogic
{
    /// <summary>
    /// Общая точка доступа к бизнес-логике пицц и скидок.
    /// Передаёт вызовы соответствующим специализированным сервисам.
    /// </summary>
    public class Logic : ILogic
    {
        private readonly IPizzaLogic _pizzaLogic;
        private readonly IDiscountLogic _discountLogic;

        /// <summary>
        /// Создаёт общую бизнес-логику приложения.
        /// </summary>
        /// <param name="pizzaLogic">Сервис работы с пиццами.</param>
        /// <param name="discountLogic">Сервис работы со скидками.</param>
        public Logic(IPizzaLogic pizzaLogic, IDiscountLogic discountLogic)
        {
            _pizzaLogic = pizzaLogic;
            _discountLogic = discountLogic;
        }

        /// <summary>
        /// Создаёт новую пиццу.
        /// </summary>
        public Pizza Create(string name, decimal price, bool type, int size)
        {
            return _pizzaLogic.Create(name, price, type, size);
        }

        /// <summary>
        /// Возвращает пиццу по идентификатору.
        /// </summary>
        public Pizza Read(int id)
        {
            return _pizzaLogic.Read(id);
        }

        /// <summary>
        /// Возвращает список всех пицц.
        /// </summary>
        public List<Pizza> ReadAll()
        {
            return _pizzaLogic.ReadAll();
        }

        /// <summary>
        /// Обновляет данные пиццы.
        /// </summary>
        public bool Update(int id, string name, decimal price, bool type, int size)
        {
            return _pizzaLogic.Update(id, name, price, type, size);
        }

        /// <summary>
        /// Удаляет пиццу по идентификатору.
        /// </summary>
        public bool Delete(int id)
        {
            return _pizzaLogic.Delete(id);
        }

        /// <summary>
        /// Фильтрует пиццы по диапазону цен.
        /// </summary>
        public List<Pizza> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            return _pizzaLogic.FilterByPrice(minPrice, maxPrice);
        }

        /// <summary>
        /// Возвращает статистику по пиццам.
        /// </summary>
        public string GetStatistics()
        {
            return _pizzaLogic.GetStatistics();
        }

        /// <summary>
        /// Возвращает список правил скидок.
        /// </summary>
        public List<SizeDiscount> ReadDiscounts()
        {
            return _discountLogic.ReadDiscounts();
        }

        /// <summary>
        /// Создаёт или обновляет правило скидки.
        /// </summary>
        public SizeDiscount SaveSizeDiscount(int size, decimal discountPercent)
        {
            return _discountLogic.SaveSizeDiscount(size, discountPercent);
        }

        /// <summary>
        /// Удаляет правило скидки.
        /// </summary>
        public bool DeleteSizeDiscount(int id)
        {
            return _discountLogic.DeleteSizeDiscount(id);
        }

        /// <summary>
        /// Возвращает процент скидки для размера.
        /// </summary>
        public decimal GetDiscountPercent(int size)
        {
            return _discountLogic.GetDiscountPercent(size);
        }

        /// <summary>
        /// Рассчитывает стоимость пиццы с учётом скидки.
        /// </summary>
        public decimal GetDiscountedPrice(Pizza pizza)
        {
            return _discountLogic.GetDiscountedPrice(pizza);
        }
    }
}
