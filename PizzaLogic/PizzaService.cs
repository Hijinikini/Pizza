
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using PizzaModel;

namespace PizzaLogic
{
    /// <summary>
    /// Бизнес-логика для работы с пиццами.
    /// </summary>
    public class PizzaService : IPizzaLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Создаёт сервис работы с пиццами.
        /// </summary>
        /// <param name="unitOfWork">Объект управления операциями с данными.</param>
        public PizzaService(IUnitOfWork unitOfWork)
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
        /// Создаёт новую пиццу.
        /// </summary>
        public Pizza Create(string name, decimal price, bool type, int size)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название пиццы не может быть пустым.");

            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше нуля.");

            if (size <= 0)
                throw new ArgumentException("Размер должен быть больше нуля.");

            Pizza pizza = new Pizza
            {
                Name = name,
                Price = price,
                Type = type,
                Size = size
            };

            ExecuteInTransaction(() => _unitOfWork.Pizzas.Add(pizza));
            return pizza;
        }

        /// <summary>
        /// Возвращает пиццу по идентификатору.
        /// </summary>
        public Pizza Read(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный идентификатор.");

            return _unitOfWork.Pizzas.ReadById(id);
        }

        /// <summary>
        /// Возвращает все пиццы.
        /// </summary>
        public List<Pizza> ReadAll()
        {
            return _unitOfWork.Pizzas.ReadAll();
        }

        /// <summary>
        /// Обновляет данные пиццы.
        /// </summary>
        public bool Update(int id, string name, decimal price, bool type, int size)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный идентификатор.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название пиццы не может быть пустым.");

            if (price <= 0)
                throw new ArgumentException("Цена должна быть больше нуля.");

            if (size <= 0)
                throw new ArgumentException("Размер должен быть больше нуля.");

            Pizza pizza = Read(id);

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Price = price;
            pizza.Type = type;
            pizza.Size = size;

            ExecuteInTransaction(() => _unitOfWork.Pizzas.Update(pizza));
            return true;
        }

        /// <summary>
        /// Удаляет пиццу.
        /// </summary>
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный идентификатор.");

            if (Read(id) == null)
                return false;

            ExecuteInTransaction(() => _unitOfWork.Pizzas.Delete(id));
            return true;
        }

        /// <summary>
        /// Фильтрует пиццы по диапазону цен.
        /// </summary>
        public List<Pizza> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < minPrice)
                throw new ArgumentException("Некорректный диапазон цен.");

            return _unitOfWork.Pizzas.ReadAll()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        /// <summary>
        /// Формирует статистику по пиццам.
        /// </summary>
        public string GetStatistics()
        {
            List<Pizza> pizzas = ReadAll();

            if (pizzas.Count == 0)
                throw new InvalidOperationException("Список пицц пуст.");

            decimal minPrice = pizzas.Min(p => p.Price);
            decimal maxPrice = pizzas.Max(p => p.Price);
            decimal averagePrice = pizzas.Average(p => p.Price);
            int ppCount = pizzas.Count(p => p.Type);

            return $"Всего пицц: {pizzas.Count}\n" +
                   $"Самая дешёвая: {minPrice}\n" +
                   $"Самая дорогая: {maxPrice}\n" +
                   $"Средняя цена: {averagePrice:F2}\n" +
                   $"ПП-пицц: {ppCount}";
        }
    }
}
