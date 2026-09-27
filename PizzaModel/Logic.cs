using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;

namespace PizzaModel
{
    public class Logic
    {
        private readonly IRepository<Pizza> _repository;

        public Logic()
        {
            _repository = new EntityRepository<Pizza>();
        }

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

            _repository.Add(pizza);

            return pizza;
        }

        public Pizza Read(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            return _repository.ReadById(id);
        }

        public List<Pizza> ReadAll()
        {
            return _repository.ReadAll();
        }

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

            _repository.Update(pizza);

            return true;
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            Pizza pizza = Read(id);

            if (pizza == null)
                return false;

            _repository.Delete(id);

            return true;
        }

        public List<Pizza> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            if (minPrice <= 0)
                throw new ArgumentException(
                    "Минимальная цена должна быть больше нуля.");

            if (maxPrice <= 0)
                throw new ArgumentException(
                    "Максимальная цена должна быть больше нуля.");

            if (minPrice > maxPrice)
                throw new ArgumentException(
                    "Минимальная цена не может быть больше максимальной.");

            return _repository.ReadAll()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        public string GetStatistics()
        {
            List<Pizza> pizzas = _repository.ReadAll();

            if (pizzas.Count == 0)
                throw new InvalidOperationException(
                    "Нельзя получить статистику. Список пицц пуст.");

            Pizza cheapest = pizzas
                .OrderBy(p => p.Price)
                .First();

            Pizza mostExpensive = pizzas
                .OrderByDescending(p => p.Price)
                .First();

            decimal averagePrice = pizzas.Average(p => p.Price);

            int ppCount = pizzas.Count(p => p.Type);

            return
                $"Количество пицц: {pizzas.Count}\n" +
                $"Средняя цена: {averagePrice:F2} руб.\n" +
                $"Самая дешёвая: {cheapest.Name} — {cheapest.Price} руб.\n" +
                $"Самая дорогая: {mostExpensive.Name} — {mostExpensive.Price} руб.\n" +
                $"ПП-пицц: {ppCount}";
        }
    }
}