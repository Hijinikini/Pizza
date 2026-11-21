
using System;
using DataAccessLayer;
using PizzaLogic;

namespace Pizza
{
    /// <summary>
    /// Точка входа в консольное приложение пиццерии.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Запускает консольное приложение.
        /// </summary>
        static void Main(string[] args)
        {
            using var context = new PizzaDbContext();
            using var uow = new EntityUnitOfWork(context);

            Logic logic = new Logic(
                new PizzaService(uow),
                new DiscountService(uow)
            );

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== ПИЦЦЕРИЯ =====");
                Console.WriteLine("1. Показать все пиццы");
                Console.WriteLine("2. Добавить пиццу");
                Console.WriteLine("3. Изменить пиццу");
                Console.WriteLine("4. Удалить пиццу");
                Console.WriteLine("5. Фильтр по цене");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Работа со скидками");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            foreach (var pizza in logic.ReadAll())
                            {
                                Console.WriteLine(
                                    $"{pizza.Id}. {pizza.Name} | Цена: {pizza.Price} | Размер: {pizza.Size} | ПП: {pizza.Type}"
                                );
                            }
                            break;

                        case "2":
                            Console.Write("Название: ");
                            string name = Console.ReadLine();

                            Console.Write("Цена: ");
                            decimal price = decimal.Parse(Console.ReadLine());

                            Console.Write("ПП (true/false): ");
                            bool type = bool.Parse(Console.ReadLine());

                            Console.Write("Размер: ");
                            int size = int.Parse(Console.ReadLine());

                            logic.Create(name, price, type, size);
                            Console.WriteLine("Пицца добавлена.");
                            break;

                        case "3":
                            Console.Write("ID пиццы: ");
                            int updateId = int.Parse(Console.ReadLine());

                            Console.Write("Новое название: ");
                            string newName = Console.ReadLine();

                            Console.Write("Новая цена: ");
                            decimal newPrice = decimal.Parse(Console.ReadLine());

                            Console.Write("ПП (true/false): ");
                            bool newType = bool.Parse(Console.ReadLine());

                            Console.Write("Новый размер: ");
                            int newSize = int.Parse(Console.ReadLine());

                            Console.WriteLine(
                                logic.Update(updateId, newName, newPrice, newType, newSize)
                                    ? "Пицца изменена."
                                    : "Пицца не найдена."
                            );
                            break;

                        case "4":
                            Console.Write("ID пиццы: ");
                            int deleteId = int.Parse(Console.ReadLine());

                            Console.WriteLine(
                                logic.Delete(deleteId)
                                    ? "Пицца удалена."
                                    : "Пицца не найдена."
                            );
                            break;

                        case "5":
                            Console.Write("Минимальная цена: ");
                            decimal minPrice = decimal.Parse(Console.ReadLine());

                            Console.Write("Максимальная цена: ");
                            decimal maxPrice = decimal.Parse(Console.ReadLine());

                            foreach (var pizza in logic.FilterByPrice(minPrice, maxPrice))
                            {
                                Console.WriteLine($"{pizza.Name} — {pizza.Price}");
                            }
                            break;

                        case "6":
                            Console.WriteLine(logic.GetStatistics());
                            break;

                        case "7":
                            Console.WriteLine("1. Показать скидки");
                            Console.WriteLine("2. Добавить или изменить скидку");
                            Console.WriteLine("3. Удалить скидку");
                            Console.Write("Выберите действие: ");

                            string discountChoice = Console.ReadLine();

                            if (discountChoice == "1")
                            {
                                foreach (var discount in logic.ReadDiscounts())
                                {
                                    Console.WriteLine(
                                        $"Размер: {discount.Size}, скидка: {discount.DiscountPercent}%"
                                    );
                                }
                            }
                            else if (discountChoice == "2")
                            {
                                Console.Write("Размер: ");
                                int discountSize = int.Parse(Console.ReadLine());

                                Console.Write("Процент скидки: ");
                                decimal discountPercent = decimal.Parse(Console.ReadLine());

                                logic.SaveSizeDiscount(discountSize, discountPercent);
                                Console.WriteLine("Скидка сохранена.");
                            }
                            else if (discountChoice == "3")
                            {
                                Console.Write("ID скидки: ");
                                int discountId = int.Parse(Console.ReadLine());

                                Console.WriteLine(
                                    logic.DeleteSizeDiscount(discountId)
                                        ? "Скидка удалена."
                                        : "Скидка не найдена."
                                );
                            }
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Неизвестная команда.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
