using PizzaModel;
using DataAccessLayer;
using PizzaLogic;
using PizzaClass = PizzaModel.Pizza;

namespace Pizza
{
    /// <summary>
    /// Консольный интерфейс приложения "Пиццерия".
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Главный метод. Показывает меню и обрабатывает выбор пользователя.
        /// </summary>
        static void Main()
        {
            using var uow = new EntityUnitOfWork();
            Logic logic = new Logic(uow.Pizzas, uow.Discounts);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("       ПИЦЦЕРИЯ ОТ МАКСА и АРТЕМА");
                Console.WriteLine("");
                Console.WriteLine("1. Показать все пиццы");
                Console.WriteLine("2. Добавить пиццу");
                Console.WriteLine("3. Изменить пиццу");
                Console.WriteLine("4. Удалить пиццу");
                Console.WriteLine("5. Поиск по диапазону цены");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Управление скидками по размерам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowAll(logic);
                            break;

                        case "2":
                            AddPizza(logic);
                            break;

                        case "3":
                            UpdatePizza(logic);
                            break;

                        case "4":
                            DeletePizza(logic);
                            break;

                        case "5":
                            FilterByPrice(logic);
                            break;

                        case "6":
                            Console.WriteLine();
                            Console.WriteLine(logic.GetStatistics());
                            break;

                        case "7":
                            ManageDiscounts(logic);
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Ошибка: такого пункта нет.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ошибка: " + ex.Message);
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Показывает список всех пицц.
        /// </summary>
        static void ShowAll(Logic logic)
        {
            List<PizzaClass> pizzas = logic.ReadAll();

            if (pizzas.Count == 0)
            {
                Console.WriteLine("Список пицц пуст.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("СПИСОК ПИЦЦ:");

            foreach (PizzaClass pizza in pizzas)
            {
                Console.WriteLine(pizza);
            }
        }

        /// <summary>
        /// Запрашивает данные и добавляет новую пиццу.
        /// </summary>
        static void AddPizza(Logic logic)
        {
            Console.WriteLine("ДОБАВЛЕНИЕ ПИЦЦЫ");

            Console.Write("Название пиццы: ");
            string name = Console.ReadLine();

            Console.Write("Цена: ");
            decimal price = ReadDecimal();

            Console.Write("ПП пицца? (д/н): ");
            bool type = ReadBool();

            Console.Write("Размер (см): ");
            int size = ReadInt();

            PizzaClass pizza = logic.Create(name, price, type, size);

            Console.WriteLine();
            Console.WriteLine("Пицца добавлена:");
            Console.WriteLine(pizza);
        }

        /// <summary>
        /// Позволяет изменить данные существующей пиццы.
        /// </summary>
        static void UpdatePizza(Logic logic)
        {
            Console.WriteLine("ИЗМЕНЕНИЕ ПИЦЦЫ");

            List<PizzaClass> pizzas = logic.ReadAll();

            if (pizzas.Count == 0)
            {
                Console.WriteLine("Список пицц пуст. Изменять нечего.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("СПИСОК ПИЦЦ:");

            for (int i = 0; i < pizzas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pizzas[i]}");
            }

            Console.WriteLine();
            Console.Write("Введите номер пиццы, которую хотите изменить: ");
            int number = ReadInt();

            if (number < 1 || number > pizzas.Count)
            {
                Console.WriteLine("Пицца с таким номером не найдена.");
                return;
            }

            PizzaClass selected = pizzas[number - 1];

            Console.WriteLine();
            Console.WriteLine("Вы выбрали:");
            Console.WriteLine(selected);
            Console.WriteLine();

            Console.Write("Новое название: ");
            string name = Console.ReadLine();

            Console.Write("Новая цена: ");
            decimal price = ReadDecimal();

            Console.Write("ПП пицца? (д/н): ");
            bool type = ReadBool();

            Console.Write("Новый размер: ");
            int size = ReadInt();

            logic.Update(selected.Id, name, price, type, size);

            Console.WriteLine("Пицца изменена.");
        }

        /// <summary>
        /// Удаляет пиццу по Id.
        /// </summary>
        static void DeletePizza(Logic logic)
        {
            Console.WriteLine("УДАЛЕНИЕ ПИЦЦЫ");

            Console.Write("ID пиццы: ");
            int id = ReadInt();

            if (logic.Delete(id))
                Console.WriteLine("Пицца удалена.");
            else
                Console.WriteLine("Пицца с таким ID не найдена.");
        }

        /// <summary>
        /// Ищет пиццы в заданном диапазоне цен.
        /// </summary>
        static void FilterByPrice(Logic logic)
        {
            Console.WriteLine("ПОИСК ПО ЦЕНЕ");

            Console.Write("Минимальная цена: ");
            decimal minPrice = ReadDecimal();

            Console.Write("Максимальная цена: ");
            decimal maxPrice = ReadDecimal();

            List<PizzaClass> pizzas =
                logic.FilterByPrice(minPrice, maxPrice);

            Console.WriteLine();

            if (pizzas.Count == 0)
            {
                Console.WriteLine("Пицц в данном диапазоне нет.");
                return;
            }

            Console.WriteLine("НАЙДЕННЫЕ ПИЦЦЫ:");

            foreach (PizzaClass pizza in pizzas)
            {
                Console.WriteLine(pizza);
            }
        }

        /// <summary>
        /// Считывает целое положительное число с консоли.
        /// </summary>
        /// <returns>Введённое число.</returns>
        static int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                Console.Write("Введите целое число больше нуля: ");
            }
        }

        /// <summary>
        /// Считывает положительное decimal с консоли.
        /// </summary>
        /// <returns>Введённое число.</returns>
        static decimal ReadDecimal()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value) && value > 0)
                    return value;

                Console.Write("Введите число больше нуля: ");
            }
        }

        /// <summary>
        /// Считывает булево значение (д/н) с консоли.
        /// </summary>
        /// <returns>true для "д", false для "н".</returns>
        static bool ReadBool()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (input == "д" || input == "Д")
                    return true;

                if (input == "н" || input == "Н")
                    return false;

                Console.Write("Введите «д» или «н»: ");
            }
        }

        /// <summary>
        /// Меню управления скидками по размерам.
        /// </summary>
        static void ManageDiscounts(Logic logic)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("УПРАВЛЕНИЕ СКИДКАМИ ПО РАЗМЕРАМ");
                Console.WriteLine("1. Показать скидки");
                Console.WriteLine("2. Добавить/изменить скидку для размера");
                Console.WriteLine("3. Удалить скидку");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowDiscounts(logic);
                        break;

                    case "2":
                        SetDiscount(logic);
                        break;

                    case "3":
                        DeleteDiscount(logic);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Ошибка: такого пункта нет.");
                        break;
                }
            }
        }

        /// <summary>
        /// Показывает все правила скидок.
        /// </summary>
        static void ShowDiscounts(Logic logic)
        {
            List<SizeDiscount> discounts = logic.ReadDiscounts();

            Console.WriteLine();
            Console.WriteLine("СКИДКИ ПО РАЗМЕРАМ:");

            if (discounts.Count == 0)
            {
                Console.WriteLine("Скидки не заданы. Для всех размеров действует 0%.");
                return;
            }

            foreach (SizeDiscount discount in discounts)
            {
                Console.WriteLine($"ID: {discount.Id} | {discount.Size} см | Скидка: {discount.DiscountPercent:F2}%");
            }
        }

        /// <summary>
        /// Добавляет или изменяет скидку для указанного размера.
        /// </summary>
        static void SetDiscount(Logic logic)
        {
            Console.WriteLine();
            Console.WriteLine("ДОБАВЛЕНИЕ / ИЗМЕНЕНИЕ СКИДКИ");

            Console.Write("Размер пиццы (см): ");
            int size = ReadInt();

            Console.Write("Скидка (%): ");
            decimal discount = ReadPercent();

            SizeDiscount result = logic.SaveSizeDiscount(size, discount);

            Console.WriteLine();
            Console.WriteLine("Правило скидки сохранено:");
            Console.WriteLine(result);

            List<PizzaClass> pizzas = logic.ReadAll()
                .Where(p => p.Size == size)
                .ToList();

            if (pizzas.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Пиццы размера {size} см теперь имеют скидку {discount:F2}%:");

                foreach (PizzaClass pizza in pizzas)
                {
                    ShowPizzaWithDiscount(logic, pizza);
                }
            }
            else
            {
                Console.WriteLine($"Пицц размера {size} см пока нет.");
            }
        }

        /// <summary>
        /// Удаляет правило скидки по Id.
        /// </summary>
        static void DeleteDiscount(Logic logic)
        {
            List<SizeDiscount> discounts = logic.ReadDiscounts();

            if (discounts.Count == 0)
            {
                Console.WriteLine("Скидок для удаления нет.");
                return;
            }

            ShowDiscounts(logic);

            Console.WriteLine();
            Console.Write("Введите ID правила скидки: ");
            int id = ReadInt();

            if (logic.DeleteSizeDiscount(id))
                Console.WriteLine("Правило скидки удалено. Для этого размера теперь действует скидка 0%.");
            else
                Console.WriteLine("Правило скидки с таким ID не найдено.");
        }

        /// <summary>
        /// Считывает процент (0–100) с консоли.
        /// </summary>
        /// <returns>Введённый процент.</returns>
        static decimal ReadPercent()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal value) && value >= 0 && value <= 100)
                    return value;

                Console.Write("Введите процент от 0 до 100: ");
            }
        }

        /// <summary>
        /// Показывает пиццу с информацией о скидке.
        /// </summary>
        static void ShowPizzaWithDiscount(Logic logic, PizzaClass pizza)
        {
            decimal discount = logic.GetDiscountPercent(pizza.Size);
            decimal discountedPrice = logic.GetDiscountedPrice(pizza);

            Console.WriteLine(
                $"[{pizza.Id}] {pizza.Name} | Размер: {pizza.Size} см | " +
                $"Цена: {pizza.Price:F2} руб. | Скидка: {discount:F2}% | " +
                $"Цена со скидкой: {discountedPrice:F2} руб. | " +
                $"Тип: {(pizza.Type ? "ПП" : "Обычная")}");
        }
    }
}