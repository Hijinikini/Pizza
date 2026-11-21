
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
        /// <summary>
        /// Событие уведомления об изменении списка пицц.
        /// </summary>
        public event System.EventHandler? PizzasChanged;
        /// <summary>
        /// Событие уведомления об изменении правил скидок.
        /// </summary>
        public event System.EventHandler? DiscountsChanged;
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
            var pizza = _pizzaLogic.Create(name, price, type, size);
            PizzasChanged?.Invoke(this, System.EventArgs.Empty);
            return pizza;
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
            bool updated = _pizzaLogic.Update(id, name, price, type, size);
            if (updated) PizzasChanged?.Invoke(this, System.EventArgs.Empty);
            return updated;
        }

        /// <summary>
        /// Удаляет пиццу по идентификатору.
        /// </summary>
        public bool Delete(int id)
        {
            bool deleted = _pizzaLogic.Delete(id);
            if (deleted) PizzasChanged?.Invoke(this, System.EventArgs.Empty);
            return deleted;
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
            var discount = _discountLogic.SaveSizeDiscount(size, discountPercent);
            DiscountsChanged?.Invoke(this, System.EventArgs.Empty);
            PizzasChanged?.Invoke(this, System.EventArgs.Empty);
            return discount;
        }

        /// <summary>
        /// Удаляет правило скидки.
        /// </summary>
        public bool DeleteSizeDiscount(int id)
        {
            bool deleted = _discountLogic.DeleteSizeDiscount(id);
            if (deleted)
            {
                DiscountsChanged?.Invoke(this, System.EventArgs.Empty);
                PizzasChanged?.Invoke(this, System.EventArgs.Empty);
            }
            return deleted;
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
