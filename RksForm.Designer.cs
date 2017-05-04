namespace adminka
{
    partial class RksForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox_komu = new System.Windows.Forms.TextBox();
            this.textBox_kuda = new System.Windows.Forms.TextBox();
            this.textBox_index = new System.Windows.Forms.TextBox();
            this.textBox_otkogo = new System.Windows.Forms.TextBox();
            this.textBox_summa = new System.Windows.Forms.TextBox();
            this.textBox_adrotravitelya = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_find = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button14 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button12 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button11 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button13 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox_shab_word = new System.Windows.Forms.ComboBox();
            this.comboBox_shab_excel = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(160, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "За день";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1406, 280);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(241, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(144, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Питер",
            "Почта рц",
            "Ден почта",
            "Казахстан"});
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 28);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 430);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1406, 48);
            this.dataGridView2.TabIndex = 4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 484);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1406, 90);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // textBox_komu
            // 
            this.textBox_komu.Location = new System.Drawing.Point(406, 10);
            this.textBox_komu.Name = "textBox_komu";
            this.textBox_komu.Size = new System.Drawing.Size(178, 20);
            this.textBox_komu.TabIndex = 6;
            // 
            // textBox_kuda
            // 
            this.textBox_kuda.Location = new System.Drawing.Point(662, 10);
            this.textBox_kuda.Name = "textBox_kuda";
            this.textBox_kuda.Size = new System.Drawing.Size(342, 20);
            this.textBox_kuda.TabIndex = 7;
            // 
            // textBox_index
            // 
            this.textBox_index.Location = new System.Drawing.Point(590, 10);
            this.textBox_index.Name = "textBox_index";
            this.textBox_index.Size = new System.Drawing.Size(66, 20);
            this.textBox_index.TabIndex = 8;
            // 
            // textBox_otkogo
            // 
            this.textBox_otkogo.Location = new System.Drawing.Point(406, 39);
            this.textBox_otkogo.Name = "textBox_otkogo";
            this.textBox_otkogo.Size = new System.Drawing.Size(178, 20);
            this.textBox_otkogo.TabIndex = 9;
            // 
            // textBox_summa
            // 
            this.textBox_summa.Location = new System.Drawing.Point(590, 39);
            this.textBox_summa.Name = "textBox_summa";
            this.textBox_summa.Size = new System.Drawing.Size(66, 20);
            this.textBox_summa.TabIndex = 10;
            // 
            // textBox_adrotravitelya
            // 
            this.textBox_adrotravitelya.Location = new System.Drawing.Point(662, 39);
            this.textBox_adrotravitelya.Name = "textBox_adrotravitelya";
            this.textBox_adrotravitelya.Size = new System.Drawing.Size(342, 20);
            this.textBox_adrotravitelya.TabIndex = 11;
            this.textBox_adrotravitelya.Click += new System.EventHandler(this.textBox_adrotravitelya_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1010, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 21);
            this.button2.TabIndex = 12;
            this.button2.Text = "Make *.doc";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1010, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 20);
            this.button3.TabIndex = 13;
            this.button3.Text = "Make all *.doc";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1233, 35);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(111, 17);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Печатать отчеты";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button_find
            // 
            this.button_find.Location = new System.Drawing.Point(590, 65);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(66, 23);
            this.button_find.TabIndex = 0;
            this.button_find.Text = "поиск";
            this.button_find.UseVisualStyleBackColor = true;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(406, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(1233, 12);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(120, 17);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Открывать отчеты";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(241, 35);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(128, 17);
            this.checkBox3.TabIndex = 19;
            this.checkBox3.Text = "Только одобренные";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(662, 65);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(340, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1122, 10);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(105, 19);
            this.button5.TabIndex = 24;
            this.button5.Text = "Make *.xls";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1122, 36);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 19);
            this.button6.TabIndex = 25;
            this.button6.Text = "Make all *.xls";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(742, 12);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 28;
            this.button9.Text = "Одобрен";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(823, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(77, 23);
            this.button10.TabIndex = 29;
            this.button10.Text = "Выкуплен";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker3);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Location = new System.Drawing.Point(12, 94);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1406, 44);
            this.panel1.TabIndex = 31;
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(996, 12);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 40;
            this.button14.Text = "Брак";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "За период";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(395, 12);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(95, 23);
            this.button12.TabIndex = 38;
            this.button12.Text = "Доставлен";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1077, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 26);
            this.label1.TabIndex = 37;
            this.label1.Text = "label1";
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.Location = new System.Drawing.Point(231, 13);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new System.Drawing.Size(158, 20);
            this.dateTimePicker3.TabIndex = 36;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(82, 13);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(143, 20);
            this.dateTimePicker2.TabIndex = 35;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(906, 12);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(84, 23);
            this.button11.TabIndex = 34;
            this.button11.Text = "Прибыл";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(663, 12);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(77, 23);
            this.button8.TabIndex = 33;
            this.button8.Text = "Отправлен";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(586, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(76, 23);
            this.button7.TabIndex = 32;
            this.button7.Text = "Возврат";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(491, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 23);
            this.button4.TabIndex = 31;
            this.button4.Text = "Возврат почта";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(13, 62);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(224, 23);
            this.button13.TabIndex = 41;
            this.button13.Text = "Установить статус выбранного";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Брак",
            "Доставлен",
            "Возврат почта",
            "Возврат",
            "Отправлен",
            "Одобрен",
            "Выкуплен",
            "Прибыл"});
            this.comboBox2.Location = new System.Drawing.Point(243, 64);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(157, 21);
            this.comboBox2.TabIndex = 42;
            // 
            // comboBox_shab_word
            // 
            this.comboBox_shab_word.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_shab_word.FormattingEnabled = true;
            this.comboBox_shab_word.Items.AddRange(new object[] {
            "blank112_ru.doc",
            "blank112_kz.doc"});
            this.comboBox_shab_word.Location = new System.Drawing.Point(1010, 64);
            this.comboBox_shab_word.Name = "comboBox_shab_word";
            this.comboBox_shab_word.Size = new System.Drawing.Size(104, 21);
            this.comboBox_shab_word.TabIndex = 43;
            // 
            // comboBox_shab_excel
            // 
            this.comboBox_shab_excel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_shab_excel.FormattingEnabled = true;
            this.comboBox_shab_excel.Items.AddRange(new object[] {
            "f112_nat.xlsx",
            "f112_den.xlsx"});
            this.comboBox_shab_excel.Location = new System.Drawing.Point(1122, 65);
            this.comboBox_shab_excel.Name = "comboBox_shab_excel";
            this.comboBox_shab_excel.Size = new System.Drawing.Size(105, 21);
            this.comboBox_shab_excel.TabIndex = 44;
            // 
            // RksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1430, 584);
            this.Controls.Add(this.comboBox_shab_excel);
            this.Controls.Add(this.comboBox_shab_word);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_find);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_adrotravitelya);
            this.Controls.Add(this.textBox_summa);
            this.Controls.Add(this.textBox_otkogo);
            this.Controls.Add(this.textBox_index);
            this.Controls.Add(this.textBox_kuda);
            this.Controls.Add(this.textBox_komu);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "RksForm";
            this.Text = "Админка РКС v.0.2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox_komu;
        private System.Windows.Forms.TextBox textBox_kuda;
        private System.Windows.Forms.TextBox textBox_index;
        private System.Windows.Forms.TextBox textBox_otkogo;
        private System.Windows.Forms.TextBox textBox_summa;
        private System.Windows.Forms.TextBox textBox_adrotravitelya;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.ComboBox comboBox_shab_word;
        private System.Windows.Forms.ComboBox comboBox_shab_excel;
    }
}

