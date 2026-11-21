
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PizzaModel;
using PizzaLogic;

namespace PizzaWinForms
{
    /// <summary>
    /// Главная форма приложения для управления пиццами и скидками.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly ILogic _logic;

        /// <summary>
        /// Создаёт главную форму и получает бизнес-логику через внедрение зависимостей.
        /// </summary>
        /// <param name="logic">Интерфейс бизнес-логики приложения.</param>
        public Form1(ILogic logic)
        {
            InitializeComponent();

            _logic = logic ?? throw new ArgumentNullException(nameof(logic));

            RefreshGrid2();
            RefreshDiscountGrid();
        }

        /// <summary>
        /// Добавляет новую пиццу.
        /// </summary>
        private void BtnAdd_Click_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(textBox2.Text, out decimal price))
                {
                    MessageBox.Show("Цена введена неправильно.");
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int size))
                {
                    MessageBox.Show("Размер введён неправильно.");
                    return;
                }

                _logic.Create(
                    textBox1.Text,
                    price,
                    checkBox1.Checked,
                    size
                );

                RefreshGrid2();
                ClearFields();

                MessageBox.Show("Пицца добавлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Изменяет данные выбранной пиццы.
        /// </summary>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid.CurrentRow == null)
                {
                    MessageBox.Show("Сначала выберите пиццу в таблице.");
                    return;
                }

                object value = Grid.CurrentRow.Cells["Id"].Value;

                if (value == null || !int.TryParse(value.ToString(), out int id))
                {
                    MessageBox.Show("Не удалось определить ID пиццы.");
                    return;
                }

                if (!decimal.TryParse(textBox2.Text, out decimal price))
                {
                    MessageBox.Show("Цена введена неправильно.");
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int size))
                {
                    MessageBox.Show("Размер введён неправильно.");
                    return;
                }

                bool result = _logic.Update(
                    id,
                    textBox1.Text,
                    price,
                    checkBox1.Checked,
                    size
                );

                if (result)
                {
                    RefreshGrid2();
                    MessageBox.Show("Пицца изменена.");
                }
                else
                {
                    MessageBox.Show("Пицца не найдена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Удаляет выбранную пиццу.
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Grid.CurrentRow == null)
                {
                    MessageBox.Show("Выберите пиццу в таблице.");
                    return;
                }

                object value = Grid.CurrentRow.Cells["Id"].Value;

                if (value == null)
                {
                    MessageBox.Show("Не удалось получить ID.");
                    return;
                }

                if (!int.TryParse(value.ToString(), out int id))
                {
                    MessageBox.Show("ID введён неправильно.");
                    return;
                }

                if (_logic.Delete(id))
                {
                    RefreshGrid2();
                    MessageBox.Show("Пицца удалена.");
                }
                else
                {
                    MessageBox.Show("Пицца не найдена.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Обновляет таблицу пицц.
        /// </summary>
        private void RefreshGrid_Click(object sender, EventArgs e)
        {
            RefreshGrid2();
        }

        /// <summary>
        /// Фильтрует пиццы по диапазону цен.
        /// </summary>
        private void BtnFilterByPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(textBox5.Text, out decimal minPrice))
                {
                    MessageBox.Show("Минимальная цена введена неправильно.");
                    return;
                }

                if (!decimal.TryParse(textBox6.Text, out decimal maxPrice))
                {
                    MessageBox.Show("Максимальная цена введена неправильно.");
                    return;
                }

                List<Pizza> pizzas = _logic.FilterByPrice(minPrice, maxPrice);

                if (pizzas.Count == 0)
                {
                    MessageBox.Show("Пицц в этом диапазоне нет.");
                    return;
                }

                Grid.DataSource = null;
                Grid.DataSource = BuildPizzaRows(pizzas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Показывает статистику по пиццам.
        /// </summary>
        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            try
            {
                string statistics = _logic.GetStatistics();

                MessageBox.Show(
                    statistics,
                    "Статистика",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Обновляет таблицу пицц данными из бизнес-логики.
        /// </summary>
        private void RefreshGrid2()
        {
            Grid.DataSource = null;
            Grid.DataSource = BuildPizzaRows(_logic.ReadAll());
        }

        /// <summary>
        /// Преобразует список пицц в данные для отображения в таблице.
        /// </summary>
        /// <param name="pizzas">Список пицц.</param>
        /// <returns>Список подготовленных строк таблицы.</returns>
        private List<PizzaDisplay> BuildPizzaRows(List<Pizza> pizzas)
        {
            return pizzas.Select(p => new PizzaDisplay
            {
                Id = p.Id,
                Название = p.Name,
                Цена = p.Price,
                ЦенаСоСкидкой = _logic.GetDiscountedPrice(p),
                ПП = p.Type ? "Да" : "Нет",
                Размер = p.Size
            }).ToList();
        }

        /// <summary>
        /// Очищает поля ввода данных пиццы.
        /// </summary>
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
        }

        /// <summary>
        /// Заполняет поля данными выбранной пиццы.
        /// </summary>
        private void Grid_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            if (Grid.CurrentRow.IsNewRow)
                return;

            textBox1.Text = Grid.CurrentRow.Cells["Название"].Value?.ToString() ?? "";
            textBox2.Text = Grid.CurrentRow.Cells["Цена"].Value?.ToString() ?? "";
            textBox3.Text = Grid.CurrentRow.Cells["Размер"].Value?.ToString() ?? "";

            string pp = Grid.CurrentRow.Cells["ПП"].Value?.ToString() ?? "Нет";
            checkBox1.Checked = pp == "Да";
        }

        /// <summary>
        /// Обновляет таблицу правил скидок.
        /// </summary>
        private void RefreshDiscountGrid()
        {
            DiscountGrid.DataSource = null;
            DiscountGrid.DataSource = _logic.ReadDiscounts()
                .Select(d => new
                {
                    d.Id,
                    Размер = d.Size,
                    Скидка = d.DiscountPercent
                }).ToList();
        }

        /// <summary>
        /// Сохраняет новое правило скидки.
        /// </summary>
        private void SaveDiscountButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(DiscountSizeTextBox.Text, out int size))
                {
                    MessageBox.Show("Размер введён неправильно.");
                    return;
                }

                if (!decimal.TryParse(DiscountPercentTextBox.Text, out decimal discount))
                {
                    MessageBox.Show("Размер скидки введён неправильно.");
                    return;
                }

                _logic.SaveSizeDiscount(size, discount);

                RefreshDiscountGrid();
                RefreshGrid2();
                ClearDiscountFields();

                MessageBox.Show(
                    $"Для размера {size} см установлена скидка {discount:0.##}%."
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Удаляет выбранное правило скидки.
        /// </summary>
        private void DeleteDiscountButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (DiscountGrid.CurrentRow == null)
                {
                    MessageBox.Show("Выберите правило скидки.");
                    return;
                }

                if (!int.TryParse(
                    DiscountGrid.CurrentRow.Cells["Id"].Value?.ToString(),
                    out int id))
                {
                    return;
                }

                if (_logic.DeleteSizeDiscount(id))
                {
                    RefreshDiscountGrid();
                    RefreshGrid2();
                    ClearDiscountFields();

                    MessageBox.Show("Правило скидки удалено.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Заполняет поля данными выбранного правила скидки.
        /// </summary>
        private void DiscountGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DiscountGrid.CurrentRow == null || DiscountGrid.CurrentRow.IsNewRow)
                return;

            DiscountSizeTextBox.Text =
                DiscountGrid.CurrentRow.Cells["Размер"].Value?.ToString() ?? "";

            DiscountPercentTextBox.Text =
                DiscountGrid.CurrentRow.Cells["Скидка"].Value?.ToString() ?? "";
        }

        /// <summary>
        /// Очищает поля ввода правила скидки.
        /// </summary>
        private void ClearDiscountFields()
        {
            DiscountSizeTextBox.Text = "";
            DiscountPercentTextBox.Text = "";
        }

        /// <summary>
        /// Обрабатывает изменение текста в поле названия пиццы.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }

    /// <summary>
    /// Модель строки для отображения информации о пицце в таблице.
    /// </summary>
    public class PizzaDisplay
    {
        /// <summary>
        /// Идентификатор пиццы.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название пиццы.
        /// </summary>
        public string Название { get; set; }

        /// <summary>
        /// Цена пиццы без скидки.
        /// </summary>
        public decimal Цена { get; set; }

        /// <summary>
        /// Цена пиццы с учётом скидки.
        /// </summary>
        public decimal ЦенаСоСкидкой { get; set; }

        /// <summary>
        /// Признак ПП-пиццы.
        /// </summary>
        public string ПП { get; set; }

        /// <summary>
        /// Размер пиццы в сантиметрах.
        /// </summary>
        public int Размер { get; set; }
    }
}
