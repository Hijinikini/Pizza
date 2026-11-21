using Shared;

namespace PizzaWinForms;

/// <summary>Пассивное представление: собирает ввод, сообщает о действиях событиями и отображает DTO.</summary>
public partial class Form1 : Form, IView
{
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
    /// Главная форма Windows Forms, реализующая интерфейс представления и передающая действия пользователя презентеру.
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        Load += (_, _) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        FormClosed += (_, _) => ViewClosed?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Запускает выбранное представление и обрабатывает взаимодействие с пользователем.
    /// </summary>
    public void Run() => Application.Run(this);

    /// <summary>
    /// Отображает переданный список пицц.
    /// </summary>
    public void ShowPizzas(IReadOnlyList<PizzaDto> pizzas)
    {
        Grid.DataSource = pizzas.Select(p => new PizzaDisplay
        {
            Id = p.Id, Название = p.Name, Цена = p.Price,
            ЦенаСоСкидкой = p.DiscountedPrice, ПП = p.IsHealthy ? "Да" : "Нет", Размер = p.Size
        }).ToList();
    }

    /// <summary>
    /// Отображает список правил скидок.
    /// </summary>
    public void ShowDiscounts(IReadOnlyList<DiscountDto> discounts)
    {
        DiscountGrid.DataSource = discounts.Select(d => new DiscountDisplay
        { Id = d.Id, Размер = d.Size, Скидка = d.DiscountPercent }).ToList();
    }

    /// <summary>
    /// Показывает статистику по пиццам.
    /// </summary>
    public void ShowStatistics(string statistics) => MessageBox.Show(statistics, "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
    /// <summary>
    /// Показывает пользователю информационное сообщение.
    /// </summary>
    public void ShowMessage(string message) => MessageBox.Show(message);
    /// <summary>
    /// Показывает пользователю сообщение об ошибке.
    /// </summary>
    public void ShowError(string message) => MessageBox.Show("Ошибка: " + message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>
    /// Обрабатывает нажатие кнопки добавления пиццы.
    /// </summary>
    private void BtnAdd_Click_Click(object sender, EventArgs e)
    {
        if (!TryReadPizza(out var name, out var price, out var size)) return;
        AddPizzaRequested?.Invoke(this, new PizzaInputEventArgs(0, name, price, checkBox1.Checked, size));
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки изменения пиццы.
    /// </summary>
    private void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (!TryGetSelectedPizzaId(out int id) || !TryReadPizza(out var name, out var price, out var size)) return;
        UpdatePizzaRequested?.Invoke(this, new PizzaInputEventArgs(id, name, price, checkBox1.Checked, size));
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки удаления пиццы.
    /// </summary>
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (TryGetSelectedPizzaId(out int id)) DeletePizzaRequested?.Invoke(this, new IdEventArgs(id));
    }

    /// <summary>
    /// Обрабатывает запрос пользователя на обновление списка пицц.
    /// </summary>
    private void RefreshGrid_Click(object sender, EventArgs e) => RefreshRequested?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Обрабатывает нажатие кнопки фильтрации пицц по цене.
    /// </summary>
    private void BtnFilterByPrice_Click(object sender, EventArgs e)
    {
        if (!decimal.TryParse(textBox5.Text, out decimal min) || !decimal.TryParse(textBox6.Text, out decimal max))
        { ShowError("Введите корректные минимальную и максимальную цены."); return; }
        FilterRequested?.Invoke(this, new PriceRangeEventArgs(min, max));
    }

    /// <summary>
    /// Обрабатывает запрос на отображение статистики.
    /// </summary>
    private void BtnStatistics_Click(object sender, EventArgs e) => StatisticsRequested?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Обрабатывает нажатие кнопки сохранения правила скидки.
    /// </summary>
    private void SaveDiscountButton_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(DiscountSizeTextBox.Text, out int size) || !decimal.TryParse(DiscountPercentTextBox.Text, out decimal percent))
        { ShowError("Проверьте размер пиццы и процент скидки."); return; }
        SaveDiscountRequested?.Invoke(this, new DiscountInputEventArgs(size, percent));
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки удаления правила скидки.
    /// </summary>
    private void DeleteDiscountButton_Click(object sender, EventArgs e)
    {
        if (DiscountGrid.CurrentRow == null || !int.TryParse(DiscountGrid.CurrentRow.Cells["Id"].Value?.ToString(), out int id))
        { ShowError("Выберите правило скидки."); return; }
        DeleteDiscountRequested?.Invoke(this, new IdEventArgs(id));
    }

    /// <summary>
    /// Проверяет введённые данные и получает значения полей пиццы.
    /// </summary>
    private bool TryReadPizza(out string name, out decimal price, out int size)
    {
        name = textBox1.Text;
        if (!decimal.TryParse(textBox2.Text, out price))
        { ShowError("Цена введена неправильно."); size = 0; return false; }
        if (!int.TryParse(textBox3.Text, out size))
        { ShowError("Размер введён неправильно."); return false; }
        return true;
    }

    /// <summary>
    /// Пытается получить идентификатор выбранной пиццы.
    /// </summary>
    private bool TryGetSelectedPizzaId(out int id)
    {
        id = 0;
        if (Grid.CurrentRow == null || !int.TryParse(Grid.CurrentRow.Cells["Id"].Value?.ToString(), out id))
        { ShowError("Выберите пиццу в таблице."); return false; }
        return true;
    }

    /// <summary>
    /// Обрабатывает изменение выбранной строки таблицы пицц.
    /// </summary>
    private void Grid_SelectionChanged(object sender, DataGridViewCellEventArgs e)
    {
        if (Grid.CurrentRow == null || Grid.CurrentRow.IsNewRow) return;
        textBox1.Text = Grid.CurrentRow.Cells["Название"].Value?.ToString() ?? "";
        textBox2.Text = Grid.CurrentRow.Cells["Цена"].Value?.ToString() ?? "";
        textBox3.Text = Grid.CurrentRow.Cells["Размер"].Value?.ToString() ?? "";
        checkBox1.Checked = (Grid.CurrentRow.Cells["ПП"].Value?.ToString() ?? "Нет") == "Да";
    }

    /// <summary>
    /// Обрабатывает выбор строки с правилом скидки.
    /// </summary>
    private void DiscountGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DiscountGrid.CurrentRow == null || DiscountGrid.CurrentRow.IsNewRow) return;
        DiscountSizeTextBox.Text = DiscountGrid.CurrentRow.Cells["Размер"].Value?.ToString() ?? "";
        DiscountPercentTextBox.Text = DiscountGrid.CurrentRow.Cells["Скидка"].Value?.ToString() ?? "";
    }

    /// <summary>
    /// Очищает поля ввода данных пиццы.
    /// </summary>
    private void ClearFields() { textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; checkBox1.Checked = false; }
    /// <summary>
    /// Очищает поля ввода параметров скидки.
    /// </summary>
    private void ClearDiscountFields() { DiscountSizeTextBox.Text = ""; DiscountPercentTextBox.Text = ""; }
    /// <summary>
    /// Выполняет операцию textBox1_TextChanged в рамках данного компонента.
    /// </summary>
    private void textBox1_TextChanged(object sender, EventArgs e) { }
    /// <summary>
    /// Выполняет операцию DiscountPercentTextBox_TextChanged в рамках данного компонента.
    /// </summary>
    private void DiscountPercentTextBox_TextChanged(object sender, EventArgs e) { }
}

/// <summary>
/// Модель строки таблицы, содержащая данные пиццы для отображения в Windows Forms.
/// </summary>
public sealed class PizzaDisplay
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Возвращает или задаёт значение свойства Название.
    /// </summary>
    public string Название { get; set; } = "";
    /// <summary>
    /// Возвращает или задаёт значение свойства Цена.
    /// </summary>
    public decimal Цена { get; set; }
    /// <summary>
    /// Возвращает или задаёт значение свойства ЦенаСоСкидкой.
    /// </summary>
    public decimal ЦенаСоСкидкой { get; set; }
    /// <summary>
    /// Возвращает или задаёт значение свойства ПП.
    /// </summary>
    public string ПП { get; set; } = "Нет";
    /// <summary>
    /// Возвращает или задаёт значение свойства Размер.
    /// </summary>
    public int Размер { get; set; }
}

/// <summary>
/// Модель строки таблицы, содержащая данные правила скидки для отображения в Windows Forms.
/// </summary>
public sealed class DiscountDisplay
{
    /// <summary>
    /// Идентификатор объекта.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Возвращает или задаёт значение свойства Размер.
    /// </summary>
    public int Размер { get; set; }
    /// <summary>
    /// Возвращает или задаёт значение свойства Скидка.
    /// </summary>
    public decimal Скидка { get; set; }
}
