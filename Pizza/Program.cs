using PizzaModel;
using PizzaClass = PizzaModel.Pizza;
namespace Pizza
{
    internal class Program
    {
        /// <summary>
        /// Запускает консольное приложение.
        /// </summary>
        static void Main()
        {
            Logic logic = new Logic();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine("       ПИЦЦЕРИЯ ОТ МАКСА");
                Console.WriteLine("================================");
                Console.WriteLine("1. Показать все пиццы");
                Console.WriteLine("2. Добавить пиццу");
                Console.WriteLine("3. Изменить пиццу");
                Console.WriteLine("4. Удалить пиццу");
                Console.WriteLine("5. Поиск по диапазону цены");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("0. Выход");
                Console.WriteLine("================================");
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
        /// Показывает все пиццы.
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
        /// Добавляет новую пиццу.
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
        /// Изменяет пиццу.
        /// </summary>
        static void UpdatePizza(Logic logic)
        {
            Console.WriteLine("ИЗМЕНЕНИЕ ПИЦЦЫ");

            Console.Write("ID пиццы: ");
            int id = ReadInt();

            PizzaClass pizza = logic.Read(id);

            if (pizza == null)
            {
                Console.WriteLine("Пицца с таким ID не найдена.");
                return;
            }

            Console.Write("Новое название: ");
            string name = Console.ReadLine();

            Console.Write("Новая цена: ");
            decimal price = ReadDecimal();

            Console.Write("ПП пицца? (д/н): ");
            bool type = ReadBool();

            Console.Write("Новый размер: ");
            int size = ReadInt();

            logic.Update(id, name, price, type, size);

            Console.WriteLine("Пицца изменена.");
        }

        /// <summary>
        /// Удаляет пиццу.
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
        /// Ищет пиццы по диапазону цены.
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
        /// Считывает целое число.
        /// </summary>
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
        /// Считывает цену.
        /// </summary>
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
        /// Считывает ответ да или нет.
        /// </summary>
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
    }
}