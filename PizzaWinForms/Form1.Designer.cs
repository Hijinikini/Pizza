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
            textBox4 = new TextBox();
            label4 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)Grid).BeginInit();
            SuspendLayout();
            // 
            // Пиццы
            // 
            Пиццы.Anchor = AnchorStyles.Left;
            Пиццы.AutoSize = true;
            Пиццы.Location = new Point(12, 6);
            Пиццы.Name = "Пиццы";
            Пиццы.Size = new Size(58, 20);
            Пиццы.TabIndex = 1;
            Пиццы.Text = "Пиццы";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Left;
            textBox1.ForeColor = SystemColors.ActiveCaptionText;
            textBox1.Location = new Point(707, 58);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(183, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Left;
            textBox2.ForeColor = SystemColors.ActiveCaptionText;
            textBox2.Location = new Point(707, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(183, 27);
            textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Left;
            textBox3.ForeColor = SystemColors.ActiveCaptionText;
            textBox3.Location = new Point(707, 169);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(183, 27);
            textBox3.TabIndex = 4;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.Left;
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
            btnAdd.Anchor = AnchorStyles.Left;
            btnAdd.Location = new Point(688, 294);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(122, 39);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click_Click;
            // 
            // BtnUpdate
            // 
            BtnUpdate.Anchor = AnchorStyles.Left;
            BtnUpdate.Location = new Point(825, 294);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(122, 39);
            BtnUpdate.TabIndex = 7;
            BtnUpdate.Text = "Изменить";
            BtnUpdate.UseVisualStyleBackColor = true;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.Anchor = AnchorStyles.Left;
            BtnDelete.Location = new Point(825, 339);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(122, 39);
            BtnDelete.TabIndex = 8;
            BtnDelete.Text = "Удалить";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // RefreshGrid
            // 
            RefreshGrid.Anchor = AnchorStyles.Left;
            RefreshGrid.Location = new Point(688, 339);
            RefreshGrid.Name = "RefreshGrid";
            RefreshGrid.Size = new Size(122, 39);
            RefreshGrid.TabIndex = 9;
            RefreshGrid.Text = "Обновить";
            RefreshGrid.UseVisualStyleBackColor = true;
            RefreshGrid.Click += RefreshGrid_Click;
            // 
            // btnFilterBySize
            // 
            btnFilterBySize.Anchor = AnchorStyles.Left;
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
            BtnSortByPrice.Anchor = AnchorStyles.Left;
            BtnSortByPrice.Location = new Point(268, 446);
            BtnSortByPrice.Name = "BtnSortByPrice";
            BtnSortByPrice.Size = new Size(250, 39);
            BtnSortByPrice.TabIndex = 11;
            BtnSortByPrice.Text = "Статистика";
            BtnSortByPrice.UseVisualStyleBackColor = true;
            BtnSortByPrice.Click += BtnSortByPrice_Click;
            // 
            // Grid
            // 
            Grid.Anchor = AnchorStyles.Left;
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Location = new Point(12, 35);
            Grid.Name = "Grid";
            Grid.RowHeadersWidth = 51;
            Grid.Size = new Size(670, 405);
            Grid.TabIndex = 12;
            Grid.CellContentClick += Grid_CellContentClick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(707, 35);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 13;
            label1.Text = "Название";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(707, 146);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 14;
            label2.Text = "Размер";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(707, 88);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 15;
            label3.Text = "Цена";
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Left;
            textBox4.ForeColor = SystemColors.ActiveCaptionText;
            textBox4.Location = new Point(704, 252);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(63, 27);
            textBox4.TabIndex = 16;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(707, 229);
            label4.Name = "label4";
            label4.Size = new Size(24, 20);
            label4.TabIndex = 17;
            label4.Text = "ID";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(156, 505);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(125, 27);
            textBox5.TabIndex = 18;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(156, 544);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 505);
            label5.Name = "label5";
            label5.Size = new Size(150, 20);
            label5.TabIndex = 20;
            label5.Text = "Минимальная цена:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 547);
            label6.Name = "label6";
            label6.Size = new Size(154, 20);
            label6.TabIndex = 21;
            label6.Text = "Максимальная цена:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 609);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(label4);
            Controls.Add(textBox4);
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
            Text = "Пиццерия";
            ((System.ComponentModel.ISupportInitialize)Grid).EndInit();
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
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label5;
        private Label label6;
    }
}
