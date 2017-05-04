namespace adminka
{
    partial class FormZP
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
            this.checkBox_ShowBenuk = new System.Windows.Forms.CheckBox();
            this.comboBox_region = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView_zp1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_zp1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_ShowBenuk
            // 
            this.checkBox_ShowBenuk.AutoSize = true;
            this.checkBox_ShowBenuk.Checked = true;
            this.checkBox_ShowBenuk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ShowBenuk.Location = new System.Drawing.Point(294, 24);
            this.checkBox_ShowBenuk.Name = "checkBox_ShowBenuk";
            this.checkBox_ShowBenuk.Size = new System.Drawing.Size(103, 17);
            this.checkBox_ShowBenuk.TabIndex = 16;
            this.checkBox_ShowBenuk.Text = "Скрыть пустые";
            this.checkBox_ShowBenuk.UseVisualStyleBackColor = true;
            // 
            // comboBox_region
            // 
            this.comboBox_region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_region.FormattingEnabled = true;
            this.comboBox_region.Items.AddRange(new object[] {
            "Все",
            "Почта (рай.центры)",
            "почта Денис",
            "Казахстан",
            "почта улет"});
            this.comboBox_region.Location = new System.Drawing.Point(167, 22);
            this.comboBox_region.Name = "comboBox_region";
            this.comboBox_region.Size = new System.Drawing.Size(121, 21);
            this.comboBox_region.TabIndex = 15;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(138, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "Получить таблицу";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView_zp1
            // 
            this.dataGridView_zp1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_zp1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_zp1.Location = new System.Drawing.Point(12, 49);
            this.dataGridView_zp1.Name = "dataGridView_zp1";
            this.dataGridView_zp1.Size = new System.Drawing.Size(899, 406);
            this.dataGridView_zp1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(729, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(729, 491);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "label2";
            // 
            // FormZP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 523);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_ShowBenuk);
            this.Controls.Add(this.comboBox_region);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridView_zp1);
            this.Name = "FormZP";
            this.Text = "FormZP";
            this.Load += new System.EventHandler(this.FormZP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_zp1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_ShowBenuk;
        private System.Windows.Forms.ComboBox comboBox_region;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView_zp1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}