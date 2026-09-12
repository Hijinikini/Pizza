namespace PizzaWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GridPizzas_SelectionChanged = new DataGridView();
            Пиццы = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            checkBox1 = new CheckBox();
            btnAdd = new Button();
            BtnUpdate = new Button();
            BtnDelete = new Button();
            RefreshGrid = new Button();
            btnFilterBySize = new Button();
            BtnSortByPrice = new Button();
            ((System.ComponentModel.ISupportInitialize)GridPizzas_SelectionChanged).BeginInit();
            SuspendLayout();
            // 
            // GridPizzas_SelectionChanged
            // 
            GridPizzas_SelectionChanged.BackgroundColor = Color.AliceBlue;
            GridPizzas_SelectionChanged.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GridPizzas_SelectionChanged.Location = new Point(12, 32);
            GridPizzas_SelectionChanged.Name = "GridPizzas_SelectionChanged";
            GridPizzas_SelectionChanged.RowHeadersWidth = 51;
            GridPizzas_SelectionChanged.Size = new Size(610, 359);
            GridPizzas_SelectionChanged.TabIndex = 0;
            GridPizzas_SelectionChanged.CellContentClick += GridPizzas_SelectionChanged_CellContentClick;
            GridPizzas_SelectionChanged.SelectionChanged += GridPizzas_SelectionChanged_SelectionChanged;
            // 
            // Пиццы
            // 
            Пиццы.AutoSize = true;
            Пиццы.Location = new Point(12, 9);
            Пиццы.Name = "Пиццы";
            Пиццы.Size = new Size(58, 20);
            Пиццы.TabIndex = 1;
            Пиццы.Text = "Пиццы";
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.ActiveBorder;
            textBox1.Location = new Point(641, 38);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 27);
            textBox1.TabIndex = 2;
            textBox1.Text = "Название";
            // 
            // textBox2
            // 
            textBox2.ForeColor = SystemColors.ActiveBorder;
            textBox2.Location = new Point(641, 83);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(183, 27);
            textBox2.TabIndex = 3;
            textBox2.Text = "Цена";
            // 
            // textBox3
            // 
            textBox3.ForeColor = SystemColors.ActiveBorder;
            textBox3.Location = new Point(641, 127);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(183, 27);
            textBox3.TabIndex = 4;
            textBox3.Text = "Размер";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(641, 169);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(110, 24);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "ПП Пицца?";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 420);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(122, 39);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click_Click;
            // 
            // BtnUpdate
            // 
            BtnUpdate.Location = new Point(140, 420);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(122, 39);
            BtnUpdate.TabIndex = 7;
            BtnUpdate.Text = "Изменить";
            BtnUpdate.UseVisualStyleBackColor = true;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Location = new Point(268, 420);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(122, 39);
            BtnDelete.TabIndex = 8;
            BtnDelete.Text = "Удалить";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // RefreshGrid
            // 
            RefreshGrid.Location = new Point(396, 420);
            RefreshGrid.Name = "RefreshGrid";
            RefreshGrid.Size = new Size(122, 39);
            RefreshGrid.TabIndex = 9;
            RefreshGrid.Text = "Обновить";
            RefreshGrid.UseVisualStyleBackColor = true;
            RefreshGrid.Click += RefreshGrid_Click;
            // 
            // btnFilterBySize
            // 
            btnFilterBySize.Location = new Point(12, 465);
            btnFilterBySize.Name = "btnFilterBySize";
            btnFilterBySize.Size = new Size(250, 39);
            btnFilterBySize.TabIndex = 10;
            btnFilterBySize.Text = "По размеру";
            btnFilterBySize.UseVisualStyleBackColor = true;
            btnFilterBySize.Click += btnFilterBySize_Click;
            // 
            // BtnSortByPrice
            // 
            BtnSortByPrice.Location = new Point(268, 465);
            BtnSortByPrice.Name = "BtnSortByPrice";
            BtnSortByPrice.Size = new Size(250, 39);
            BtnSortByPrice.TabIndex = 11;
            BtnSortByPrice.Text = "По цене";
            BtnSortByPrice.UseVisualStyleBackColor = true;
            BtnSortByPrice.Click += BtnSortByPrice_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 726);
            Controls.Add(BtnSortByPrice);
            Controls.Add(btnFilterBySize);
            Controls.Add(RefreshGrid);
            Controls.Add(BtnDelete);
            Controls.Add(BtnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(checkBox1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(Пиццы);
            Controls.Add(GridPizzas_SelectionChanged);
            Name = "Form1";
            Text = "Пиццерия";
            ((System.ComponentModel.ISupportInitialize)GridPizzas_SelectionChanged).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView GridPizzas_SelectionChanged;
        private Label Пиццы;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private CheckBox checkBox1;
        private Button btnAdd;
        private Button BtnUpdate;
        private Button BtnDelete;
        private Button RefreshGrid;
        private Button btnFilterBySize;
        private Button BtnSortByPrice;
    }
}
