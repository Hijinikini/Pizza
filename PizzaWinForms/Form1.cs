using PizzaModel;



namespace PizzaWinForms
{
    public partial class Form1 : Form
    {
        private readonly Logic _logic = new Logic();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshGrid_Click(object sender, EventArgs e)
        {
            RefreshGrid2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                List<Pizza> pizzas =
                    _logic.FilterByPrice(minPrice, maxPrice);

                if (pizzas.Count == 0)
                {
                    MessageBox.Show("Пицц в этом диапазоне нет.");
                    return;
                }

                Grid.DataSource = null;

                Grid.DataSource = pizzas
                    .Select(p => new
                    {
                        p.Id,
                        Название = p.Name,
                        Цена = p.Price,
                        ПП = p.Type ? "Да" : "Нет",
                        Размер = p.Size
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        private void RefreshGrid2()
        {
            Grid.DataSource = null;

            Grid.DataSource = _logic.ReadAll()
                .Select(p => new
                {
                    p.Id,
                    Название = p.Name,
                    Цена = p.Price,
                    ПП = p.Type ? "Да" : "Нет",
                    Размер = p.Size
                })
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            checkBox1.Checked = false;
        }



        

        private void Grid_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid.CurrentRow == null)
                return;

            // Защита от строки "новой записи" (если AllowUserToAddRows = true)
            if (Grid.CurrentRow.IsNewRow)
                return;

            textBox1.Text = Grid.CurrentRow.Cells["Название"].Value?.ToString() ?? "";
            textBox2.Text = Grid.CurrentRow.Cells["Цена"].Value?.ToString() ?? "";
            textBox3.Text = Grid.CurrentRow.Cells["Размер"].Value?.ToString() ?? "";

            string pp = Grid.CurrentRow.Cells["ПП"].Value?.ToString() ?? "Нет";
            checkBox1.Checked = pp == "Да";
        }

    }
    }
