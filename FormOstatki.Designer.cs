namespace adminka
{
    partial class FormOstatki
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_hide_null = new System.Windows.Forms.CheckBox();
            this.comboBox_queue4 = new System.Windows.Forms.ComboBox();
            this.dataGridView_ostatki = new System.Windows.Forms.DataGridView();
            this.button12 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ostatki)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_hide_null
            // 
            this.checkBox_hide_null.AutoSize = true;
            this.checkBox_hide_null.Checked = true;
            this.checkBox_hide_null.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hide_null.Location = new System.Drawing.Point(445, 24);
            this.checkBox_hide_null.Name = "checkBox_hide_null";
            this.checkBox_hide_null.Size = new System.Drawing.Size(211, 17);
            this.checkBox_hide_null.TabIndex = 38;
            this.checkBox_hide_null.Text = "Скрывать поля с нулевым остатком";
            this.checkBox_hide_null.UseVisualStyleBackColor = true;
            // 
            // comboBox_queue4
            // 
            this.comboBox_queue4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_queue4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox_queue4.FormattingEnabled = true;
            this.comboBox_queue4.Items.AddRange(new object[] {
            "Все",
            "Почта (рай.центры)",
            "почта Денис",
            "Казахстан",
            "почта улет"});
            this.comboBox_queue4.Location = new System.Drawing.Point(12, 24);
            this.comboBox_queue4.Name = "comboBox_queue4";
            this.comboBox_queue4.Size = new System.Drawing.Size(209, 28);
            this.comboBox_queue4.TabIndex = 37;
            // 
            // dataGridView_ostatki
            // 
            this.dataGridView_ostatki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_ostatki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ostatki.Location = new System.Drawing.Point(12, 66);
            this.dataGridView_ostatki.Name = "dataGridView_ostatki";
            this.dataGridView_ostatki.Size = new System.Drawing.Size(911, 503);
            this.dataGridView_ostatki.TabIndex = 36;
            this.dataGridView_ostatki.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ostatki_CellContentClick);
            this.dataGridView_ostatki.Click += new System.EventHandler(this.dataGridView_ostatki_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(227, 22);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(188, 30);
            this.button12.TabIndex = 35;
            this.button12.Text = "Получить остатки";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(751, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(183, 54);
            this.richTextBox1.TabIndex = 39;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 583);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "label1";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Location = new System.Drawing.Point(602, 583);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(163, 20);
            this.dateTimePicker1.TabIndex = 41;
            this.dateTimePicker1.Value = new System.DateTime(2017, 1, 1, 11, 13, 0, 0);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker2.Location = new System.Drawing.Point(781, 583);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(143, 20);
            this.dateTimePicker2.TabIndex = 42;
            // 
            // FormOstatki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 615);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.checkBox_hide_null);
            this.Controls.Add(this.comboBox_queue4);
            this.Controls.Add(this.dataGridView_ostatki);
            this.Controls.Add(this.button12);
            this.Name = "FormOstatki";
            this.Text = "FormOstatki";
            this.Load += new System.EventHandler(this.FormOstatki_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ostatki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_hide_null;
        private System.Windows.Forms.ComboBox comboBox_queue4;
        private System.Windows.Forms.DataGridView dataGridView_ostatki;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}