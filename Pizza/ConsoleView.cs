using Shared;

namespace Pizza;

/// <summary>Консольное пассивное представление: вводит данные, поднимает события и выводит DTO.</summary>
public sealed class ConsoleView : IView
{
    private IReadOnlyList<PizzaDto> _pizzas = Array.Empty<PizzaDto>();
    private IReadOnlyList<DiscountDto> _discounts = Array.Empty<DiscountDto>();

    /// <summary>
    /// Событие, возникающее после загрузки представления.
    /// </summary>
    public event EventHandler? ViewLoaded;
    /// <summary>
    /// Событие, возникающее при закрытии представления.
    /// </summary>
    public event EventHandler? ViewClosed;
    /// <summary>
    /// Событие запроса на добавление пиццы со стороны представления.
    /// </summary>
    public event EventHandler<PizzaInputEventArgs>? AddPizzaRequested;
    /// <summary>
    /// Событие запроса на изменение пиццы со стороны представления.
    /// </summary>
    public event EventHandler<PizzaInputEventArgs>? UpdatePizzaRequested;
    /// <summary>
    /// Событие запроса на удаление пиццы со стороны представления.
    /// </summary>
    public event EventHandler<IdEventArgs>? DeletePizzaRequested;
    /// <summary>
    /// Событие запроса на обновление отображаемых данных.
    /// </summary>
    public event EventHandler? RefreshRequested;
    /// <summary>
    /// Событие запроса фильтрации пицц по диапазону цен.
    /// </summary>
    public event EventHandler<PriceRangeEventArgs>? FilterRequested;
    /// <summary>
    /// Событие запроса отображения статистики.
    /// </summary>
    public event EventHandler? StatisticsRequested;
    /// <summary>
    /// Событие запроса сохранения правила скидки.
    /// </summary>
    public event EventHandler<DiscountInputEventArgs>? SaveDiscountRequested;
    /// <summary>
    /// Событие запроса удаления правила скидки.
    /// </summary>
    public event EventHandler<IdEventArgs>? DeleteDiscountRequested;

    /// <summary>
    /// Запускает выбранное представление и обрабатывает взаимодействие с пользователем.
    /// </summary>
    public void Run()
    {
        ViewLoaded?.Invoke(this, EventArgs.Empty);
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n===== ПИЦЦЕРИЯ =====");
            Console.WriteLine("1. Показать все пиццы\n2. Добавить пиццу\n3. Изменить пиццу\n4. Удалить пиццу");
            Console.WriteLine("5. Фильтр по цене\n6. Статистика\n7. Показать скидки\n8. Добавить/изменить скидку\n9. Удалить скидку\n0. Выход");
            Console.Write("Выберите действие: ");
            switch (Console.ReadLine())
            {
                case "1": RefreshRequested?.Invoke(this, EventArgs.Empty); break;
                case "2": AddPizza(); break;
                case "3": UpdatePizza(); break;
                case "4": DeletePizza(); break;
                case "5": Filter(); break;
                case "6": StatisticsRequested?.Invoke(this, EventArgs.Empty); break;
                case "7": ShowDiscounts(_discounts); break;
                case "8": SaveDiscount(); break;
                case "9": DeleteDiscount(); break;
                case "0": exit = true; break;
                default: ShowError("Неизвестная команда."); break;
            }
        }
        ViewClosed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Отображает переданный список пицц.
    /// </summary>
    public void ShowPizzas(IReadOnlyList<PizzaDto> pizzas)
    {
        _pizzas = pizzas;
        Console.WriteLine("\n--- Пиццы ---");
        if (pizzas.Count == 0) Console.WriteLine("Список пуст.");
        foreach (var p in pizzas)
            Console.WriteLine($"{p.Id}. {p.Name} | {p.Price} руб. | со скидкой {p.DiscountedPrice:0.00} руб. | {p.Size} см | ПП: {(p.IsHealthy ? "Да" : "Нет")}");
    }

    /// <summary>
    /// Отображает список правил скидок.
    /// </summary>
    public void ShowDiscounts(IReadOnlyList<DiscountDto> discounts)
    {
        _discounts = discounts;
        Console.WriteLine("\n--- Скидки ---");
        if (discounts.Count == 0) Console.WriteLine("Правил скидок нет.");
        foreach (var d in discounts) Console.WriteLine($"ID {d.Id}: от {d.Size} см — {d.DiscountPercent:0.##}%");
    }

    /// <summary>
    /// Показывает статистику по пиццам.
    /// </summary>
    public void ShowStatistics(string statistics) => Console.WriteLine("\n" + statistics);
    /// <summary>
    /// Показывает пользователю информационное сообщение.
    /// </summary>
    public void ShowMessage(string message) => Console.WriteLine(message);
    /// <summary>
    /// Показывает пользователю сообщение об ошибке.
    /// </summary>
    public void ShowError(string message) => Console.WriteLine("Ошибка: " + message);

    /// <summary>
    /// Собирает введённые пользователем данные и отправляет событие добавления пиццы.
    /// </summary>
    private void AddPizza()
    {
        if (ReadPizza(out string name, out decimal price, out bool healthy, out int size))
            AddPizzaRequested?.Invoke(this, new PizzaInputEventArgs(0, name, price, healthy, size));
    }

    /// <summary>
    /// Собирает данные выбранной пиццы и отправляет событие её изменения.
    /// </summary>
    private void UpdatePizza()
    {
        int id = ReadInt("ID пиццы: ");
        if (ReadPizza(out string name, out decimal price, out bool healthy, out int size))
            UpdatePizzaRequested?.Invoke(this, new PizzaInputEventArgs(id, name, price, healthy, size));
    }

    /// <summary>
    /// Отправляет событие удаления выбранной пиццы.
    /// </summary>
    private void DeletePizza() => DeletePizzaRequested?.Invoke(this, new IdEventArgs(ReadInt("ID пиццы: ")));

    /// <summary>
    /// Получает диапазон цен и отправляет событие фильтрации пицц.
    /// </summary>
    private void Filter()
    {
        decimal min = ReadDecimal("Минимальная цена: ");
        decimal max = ReadDecimal("Максимальная цена: ");
        FilterRequested?.Invoke(this, new PriceRangeEventArgs(min, max));
    }

    /// <summary>
    /// Собирает параметры скидки и отправляет событие её сохранения.
    /// </summary>
    private void SaveDiscount()
    {
        int size = ReadInt("Размер пиццы (см): ");
        decimal percent = ReadDecimal("Скидка (%): ");
        SaveDiscountRequested?.Invoke(this, new DiscountInputEventArgs(size, percent));
    }

    /// <summary>
    /// Отправляет событие удаления выбранного правила скидки.
    /// </summary>
    private void DeleteDiscount() => DeleteDiscountRequested?.Invoke(this, new IdEventArgs(ReadInt("ID правила скидки: ")));

    /// <summary>
    /// Выполняет операцию ReadPizza в рамках данного компонента.
    /// </summary>
    private static bool ReadPizza(out string name, out decimal price, out bool healthy, out int size)
    {
        Console.Write("Название: "); name = Console.ReadLine() ?? "";
        price = ReadDecimal("Цена: ");
        Console.Write("ПП (да/нет): "); healthy = (Console.ReadLine() ?? "").Trim().ToLowerInvariant() is "да" or "д" or "yes" or "y";
        size = ReadInt("Размер (см): ");
        return true;
    }

    /// <summary>
    /// Выполняет операцию ReadInt в рамках данного компонента.
    /// </summary>
    private static int ReadInt(string prompt)
    {
        while (true) { Console.Write(prompt); if (int.TryParse(Console.ReadLine(), out int v)) return v; Console.WriteLine("Введите целое число."); }
    }

    /// <summary>
    /// Выполняет операцию ReadDecimal в рамках данного компонента.
    /// </summary>
    private static decimal ReadDecimal(string prompt)
    {
        while (true) { Console.Write(prompt); if (decimal.TryParse(Console.ReadLine(), out decimal v)) return v; Console.WriteLine("Введите число."); }
    }
}
