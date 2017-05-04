namespace adminka
{
    partial class FormRksBonus
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
            this.dataGridView_bonus = new System.Windows.Forms.DataGridView();
            this.button9 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bonus)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_bonus
            // 
            this.dataGridView_bonus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_bonus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_bonus.Location = new System.Drawing.Point(21, 41);
            this.dataGridView_bonus.Name = "dataGridView_bonus";
            this.dataGridView_bonus.Size = new System.Drawing.Size(518, 418);
            this.dataGridView_bonus.TabIndex = 3;
            this.dataGridView_bonus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_bonus_KeyDown);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(21, 12);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 2;
            this.button9.Text = "Обновить";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(426, 475);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Поставить бонус";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(320, 475);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Питер",
            "Почта рц",
            "Ден почта",
            "Казахстан",
            "Почта улет",
            "Новосибирск"});
            this.comboBox1.Location = new System.Drawing.Point(21, 477);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FormRksBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 524);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView_bonus);
            this.Controls.Add(this.button9);
            this.Name = "FormRksBonus";
            this.Text = "FormRksBonus";
            this.Load += new System.EventHandler(this.FormRksBonus_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormRksBonus_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_bonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_bonus;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}