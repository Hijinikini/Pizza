namespace PizzaWinForms
{

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
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
            Grid = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            DiscountsTitle = new Label();
            DiscountGrid = new DataGridView();
            DiscountSizeLabel = new Label();
            DiscountSizeTextBox = new TextBox();
            DiscountPercentLabel = new Label();
            DiscountPercentTextBox = new TextBox();
            SaveDiscountButton = new Button();
            DeleteDiscountButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiscountGrid).BeginInit();
            SuspendLayout();
            // 
            // Пиццы
            // 
            Пиццы.AutoSize = true;
            Пиццы.Location = new Point(12, 6);
            Пиццы.Name = "Пиццы";
            Пиццы.Size = new Size(58, 20);
            Пиццы.TabIndex = 1;
            Пиццы.Text = "Пиццы";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(707, 58);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(707, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(183, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(707, 169);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(183, 27);
            textBox3.TabIndex = 4;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(707, 202);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(110, 24);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "ПП Пицца?";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(687, 234);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(122, 39);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click_Click;
            // 
            // BtnUpdate
            // 
            BtnUpdate.Location = new Point(824, 234);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(122, 39);
            BtnUpdate.TabIndex = 7;
            BtnUpdate.Text = "Изменить";
            BtnUpdate.UseVisualStyleBackColor = true;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Location = new Point(824, 279);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(122, 39);
            BtnDelete.TabIndex = 8;
            BtnDelete.Text = "Удалить";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // RefreshGrid
            // 
            RefreshGrid.Location = new Point(687, 279);
            RefreshGrid.Name = "RefreshGrid";
            RefreshGrid.Size = new Size(122, 39);
            RefreshGrid.TabIndex = 9;
            RefreshGrid.Text = "Обновить";
            RefreshGrid.UseVisualStyleBackColor = true;
            RefreshGrid.Click += RefreshGrid_Click;
            // 
            // btnFilterBySize
            // 
            btnFilterBySize.Location = new Point(12, 446);
            btnFilterBySize.Name = "btnFilterBySize";
            btnFilterBySize.Size = new Size(250, 39);
            btnFilterBySize.TabIndex = 10;
            btnFilterBySize.Text = "По диапазону цены";
            btnFilterBySize.UseVisualStyleBackColor = true;
            btnFilterBySize.Click += BtnFilterByPrice_Click;
            // 
            // BtnSortByPrice
            // 
            BtnSortByPrice.Location = new Point(268, 446);
            BtnSortByPrice.Name = "BtnSortByPrice";
            BtnSortByPrice.Size = new Size(250, 39);
            BtnSortByPrice.TabIndex = 11;
            BtnSortByPrice.Text = "Статистика";
            BtnSortByPrice.UseVisualStyleBackColor = true;
            BtnSortByPrice.Click += BtnStatistics_Click;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Location = new Point(12, 35);
            Grid.Name = "Grid";
            Grid.RowHeadersWidth = 51;
            Grid.Size = new Size(670, 405);
            Grid.TabIndex = 12;
            Grid.CellClick += Grid_SelectionChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(707, 35);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 13;
            label1.Text = "Название";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(707, 146);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 14;
            label2.Text = "Размер";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(707, 88);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 15;
            label3.Text = "Цена";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(156, 453);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(125, 27);
            textBox5.TabIndex = 18;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(444, 453);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 456);
            label5.Name = "label5";
            label5.Size = new Size(138, 20);
            label5.TabIndex = 20;
            label5.Text = "Минимальная цена:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(300, 456);
            label6.Name = "label6";
            label6.Size = new Size(138, 20);
            label6.TabIndex = 21;
            label6.Text = "Максимальная цена:";
            // 
            // DiscountsTitle
            // 
            DiscountsTitle.AutoSize = true;
            DiscountsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            DiscountsTitle.Location = new Point(12, 500);
            DiscountsTitle.Name = "DiscountsTitle";
            DiscountsTitle.Size = new Size(180, 23);
            DiscountsTitle.TabIndex = 22;
            DiscountsTitle.Text = "Скидки по размерам";
            // 
            // DiscountGrid
            // 
            DiscountGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DiscountGrid.Location = new Point(12, 530);
            DiscountGrid.Name = "DiscountGrid";
            DiscountGrid.RowHeadersWidth = 51;
            DiscountGrid.Size = new Size(500, 170);
            DiscountGrid.TabIndex = 23;
            DiscountGrid.CellClick += DiscountGrid_CellClick;
            // 
            // DiscountSizeLabel
            // 
            DiscountSizeLabel.AutoSize = true;
            DiscountSizeLabel.Location = new Point(535, 530);
            DiscountSizeLabel.Name = "DiscountSizeLabel";
            DiscountSizeLabel.Size = new Size(85, 20);
            DiscountSizeLabel.TabIndex = 24;
            DiscountSizeLabel.Text = "Размер (см)";
            // 
            // DiscountSizeTextBox
            // 
            DiscountSizeTextBox.Location = new Point(535, 553);
            DiscountSizeTextBox.Name = "DiscountSizeTextBox";
            DiscountSizeTextBox.Size = new Size(170, 27);
            DiscountSizeTextBox.TabIndex = 25;
            // 
            // DiscountPercentLabel
            // 
            DiscountPercentLabel.AutoSize = true;
            DiscountPercentLabel.Location = new Point(725, 530);
            DiscountPercentLabel.Name = "DiscountPercentLabel";
            DiscountPercentLabel.Size = new Size(75, 20);
            DiscountPercentLabel.TabIndex = 26;
            DiscountPercentLabel.Text = "Скидка (%)";
            // 
            // DiscountPercentTextBox
            // 
            DiscountPercentTextBox.Location = new Point(725, 553);
            DiscountPercentTextBox.Name = "DiscountPercentTextBox";
            DiscountPercentTextBox.Size = new Size(170, 27);
            DiscountPercentTextBox.TabIndex = 27;
            // 
            // SaveDiscountButton
            // 
            SaveDiscountButton.Location = new Point(535, 596);
            SaveDiscountButton.Name = "SaveDiscountButton";
            SaveDiscountButton.Size = new Size(170, 40);
            SaveDiscountButton.TabIndex = 28;
            SaveDiscountButton.Text = "Сохранить скидку";
            SaveDiscountButton.UseVisualStyleBackColor = true;
            SaveDiscountButton.Click += SaveDiscountButton_Click;
            // 
            // DeleteDiscountButton
            // 
            DeleteDiscountButton.Location = new Point(725, 596);
            DeleteDiscountButton.Name = "DeleteDiscountButton";
            DeleteDiscountButton.Size = new Size(170, 40);
            DeleteDiscountButton.TabIndex = 29;
            DeleteDiscountButton.Text = "Удалить правило";
            DeleteDiscountButton.UseVisualStyleBackColor = true;
            DeleteDiscountButton.Click += DeleteDiscountButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 720);
            Controls.Add(DeleteDiscountButton);
            Controls.Add(SaveDiscountButton);
            Controls.Add(DiscountPercentTextBox);
            Controls.Add(DiscountPercentLabel);
            Controls.Add(DiscountSizeTextBox);
            Controls.Add(DiscountSizeLabel);
            Controls.Add(DiscountGrid);
            Controls.Add(DiscountsTitle);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Grid);
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
            Name = "Form1";
            Text = "Пиццерия — скидки по размерам";
            ((System.ComponentModel.ISupportInitialize)Grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiscountGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

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
        private DataGridView Grid;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label5;
        private Label label6;

        private Label DiscountsTitle;
        private DataGridView DiscountGrid;
        private Label DiscountSizeLabel;
        private TextBox DiscountSizeTextBox;
        private Label DiscountPercentLabel;
        private TextBox DiscountPercentTextBox;
        private Button SaveDiscountButton;
        private Button DeleteDiscountButton;
    }
}