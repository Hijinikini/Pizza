using Microsoft.VisualBasic.Logging;
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

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void BtnAdd_Click_Click(object sender, EventArgs e)
        {

            try
            {

                _logic.Create(
                    textBox1.Text,
                    decimal.Parse(textBox2.Text),
                    checkBox1.Checked,
                    int.Parse(textBox3.Text)
                );


                RefreshGrid2();


                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                checkBox1.Checked = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(textBox4.Text);   // читаем Id из поля

                _logic.Update(
                    id,
                    textBox1.Text,
                    decimal.Parse(textBox2.Text),
                    checkBox1.Checked,
                    int.Parse(textBox3.Text)
                );

                RefreshGrid2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Grid.CurrentRow == null)
            {
                MessageBox.Show("Выберите пиццу в таблице");
                return;
            }

            var val = Grid.CurrentRow.Cells["Id"].Value;
            if (val == null) return;
            int id = Convert.ToInt32(val);

            _logic.Delete(id);

            RefreshGrid2();
        }



        private void btnFilterBySize_Click(object sender, EventArgs e) { }

        private void BtnSortByPrice_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            _logic.Create("Пипперони", 500m, true, 30);
            _logic.Create("Маргарита", 350m, false, 25);
            _logic.Create("Песто", 700m, true, 20);
            _logic.Create("Грибная", 650m, false, 30);
            RefreshGrid2();

        }
        private void RefreshGrid2()
        {
            Grid.DataSource = null;
            Grid.DataSource = _logic.ReadAll()
                .Select(p => new
                {
                    p.Id,
                    Название = p.Name,
                    Цена = p.Price,
                    Вегетарианская = p.Type ? "Да" : "Нет",
                    Размер = p.Size
                })
                .ToList();
        }


        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _logic.Create("Пипперони", 500m, true, 30);
            _logic.Create("Маргарита", 350m, false, 25);
            _logic.Create("Песто", 700m, true, 20);
            _logic.Create("Грибная", 650m, false, 30);
            RefreshGrid2();
        }

        private void RefreshGrid_Click(object sender, EventArgs e)
        {

        }

        
    }
}
