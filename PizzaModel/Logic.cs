using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Logic
    {
        private readonly List<Pizza> _pizzas = new List<Pizza>();
        private int _nextId = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
                Id = _nextId,
                Name = name,
                Price = price,
                Type = type,
                Size = size
            };

            _nextId++;
            _pizzas.Add(pizza);

            return pizza;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Pizza Read(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            return _pizzas.FirstOrDefault(p => p.Id == id);
        }

    
        public List<Pizza> ReadAll()
        {
            return new List<Pizza>(_pizzas);
        }
        /// <summary>
        /// Изменяет данные пиццы.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// 
     
        public bool Update(int id, string name, decimal price, bool type, int size)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

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

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>


        
        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            Pizza pizza = Read(id);

            if (pizza == null)
                return false;

            _pizzas.Remove(pizza);

            return true;
        }

        /// <summary>
        /// 
        ///
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public List<Pizza> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            if (minPrice <= 0)
                throw new ArgumentException("Минимальная цена должна быть больше нуля.");

            if (maxPrice <= 0)
                throw new ArgumentException("Максимальная цена должна быть больше нуля.");

            if (minPrice > maxPrice)
                throw new ArgumentException(
                    "Минимальная цена не может быть больше максимальной.");

            return _pizzas
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetStatistics()
        {
            if (_pizzas.Count == 0)
                throw new InvalidOperationException(
                    "Нельзя получить статистику. Список пицц пуст.");

            Pizza cheapest = _pizzas
                .OrderBy(p => p.Price)
                .First();

            Pizza mostExpensive = _pizzas
                .OrderByDescending(p => p.Price)
                .First();

            decimal averagePrice = _pizzas.Average(p => p.Price);

            int ppCount = _pizzas.Count(p => p.Type);

            return
                $"Количество пицц: {_pizzas.Count}\n" +
                $"Средняя цена: {averagePrice:F2} руб.\n" +
                $"Самая дешёвая: {cheapest.Name} — {cheapest.Price} руб.\n" +
                $"Самая дорогая: {mostExpensive.Name} — {mostExpensive.Price} руб.\n" +
                $"ПП-пицц: {ppCount}";
        }
    }
}