using System;
using System.Collections.Generic;
using System.Linq;
using PizzaModel;
using DataAccessLayer;

namespace PizzaLogic
{

    /// <summary>
    /// Бизнес-логика приложения. Работа с пиццами и скидками.
    /// </summary>
    public class Logic
    {
        private readonly IRepository<Pizza> _repository;
        private readonly IRepository<SizeDiscount> _discountRepository;

        /// <summary>
        /// Внедряет репозитории для пицц и скидок.
        /// </summary>
        public Logic(IRepository<Pizza> repository, IRepository<SizeDiscount> discountRepository)
        {
            _repository = repository;
            _discountRepository = discountRepository;
        }

        /// <summary>
        /// Создаёт пиццу и сохраняет её в БД.
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

            _repository.Add(pizza);

            return pizza;
        }

        /// <summary>
        /// Возвращает пиццу по Id.
        /// </summary>
        public Pizza Read(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            return _repository.ReadById(id);
        }

        /// <summary>
        /// Возвращает список всех пицц.
        /// </summary>
        public List<Pizza> ReadAll()
        {
            return _repository.ReadAll();
        }

        /// <summary>
        /// Обновляет данные пиццы.
        /// </summary>
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

        /// <summary>
        /// Удаляет пиццу по Id.
        /// </summary>
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

        /// <summary>
        /// Ищет пиццы в заданном диапазоне цен.
        /// </summary>
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

        /// <summary>
        /// Собирает статистику по всем пиццам.
        /// </summary>
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

        /// <summary>
        /// Возвращает все правила скидок, отсортированные по размеру.
        /// </summary>
        public List<SizeDiscount> ReadDiscounts()
        {
            return _discountRepository.ReadAll()
                .OrderBy(x => x.Size)
                .ToList();
        }

        /// <summary>
        /// Сохраняет скидку для размера: создаёт или обновляет.
        /// </summary>
        public SizeDiscount SaveSizeDiscount(int size, decimal discountPercent)
        {
            if (size <= 0)
                throw new ArgumentException("Размер должен быть больше нуля.");

            if (discountPercent < 0 || discountPercent > 100)
                throw new ArgumentException("Скидка должна быть от 0 до 100%.");

            SizeDiscount existing = _discountRepository.ReadAll()
                .FirstOrDefault(x => x.Size == size);

            if (existing == null)
            {
                existing = new SizeDiscount
                {
                    Size = size,
                    DiscountPercent = discountPercent
                };

                _discountRepository.Add(existing);
            }
            else
            {
                existing.DiscountPercent = discountPercent;
                _discountRepository.Update(existing);
            }

            return existing;
        }

        /// <summary>
        /// Удаляет правило скидки по Id.
        /// </summary>
        public bool DeleteSizeDiscount(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID должен быть больше нуля.");

            SizeDiscount discount = _discountRepository.ReadById(id);

            if (discount == null)
                return false;

            _discountRepository.Delete(id);
            return true;
        }

        /// <summary>
        /// Считает процент скидки для размера. Скидка действует
        /// начиная с указанного размера и больше.
        /// </summary>
        public decimal GetDiscountPercent(int size)
        {
            if (size <= 0)
                return 0;

            var applicable = _discountRepository.ReadAll()
                .Where(x => x.Size <= size)
                .OrderByDescending(x => x.Size)
                .FirstOrDefault();

            return applicable?.DiscountPercent ?? 0;
        }

        /// <summary>
        /// Считает цену пиццы с учётом скидки.
        /// </summary>
        public decimal GetDiscountedPrice(Pizza pizza)
        {
            if (pizza == null)
                throw new ArgumentNullException(nameof(pizza));

            decimal discount = GetDiscountPercent(pizza.Size);
            return pizza.Price * (1 - discount / 100m);
        }
    }
}