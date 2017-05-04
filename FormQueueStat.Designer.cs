namespace adminka
{
    partial class FormQueueStat
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
            this.button26 = new System.Windows.Forms.Button();
            this.dataGridView_regions = new System.Windows.Forms.DataGridView();
            this.comboBox_queue1 = new System.Windows.Forms.ComboBox();
            this.richTextBox_queue_stat = new System.Windows.Forms.RichTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button_queue_stat = new System.Windows.Forms.Button();
            this.dataGridView_queue_stat = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_regions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_queue_stat)).BeginInit();
            this.SuspendLayout();
            // 
            // button26
            // 
            this.button26.Location = new System.Drawing.Point(444, 31);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(136, 23);
            this.button26.TabIndex = 53;
            this.button26.Text = "Чистка нулей";
            this.button26.UseVisualStyleBackColor = true;
            // 
            // dataGridView_regions
            // 
            this.dataGridView_regions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_regions.Location = new System.Drawing.Point(11, 406);
            this.dataGridView_regions.Name = "dataGridView_regions";
            this.dataGridView_regions.Size = new System.Drawing.Size(270, 125);
            this.dataGridView_regions.TabIndex = 52;
            // 
            // comboBox_queue1
            // 
            this.comboBox_queue1.FormattingEnabled = true;
            this.comboBox_queue1.Location = new System.Drawing.Point(11, 62);
            this.comboBox_queue1.Name = "comboBox_queue1";
            this.comboBox_queue1.Size = new System.Drawing.Size(270, 21);
            this.comboBox_queue1.TabIndex = 51;
            this.comboBox_queue1.SelectedIndexChanged += new System.EventHandler(this.comboBox_queue1_SelectedIndexChanged);
            // 
            // richTextBox_queue_stat
            // 
            this.richTextBox_queue_stat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox_queue_stat.Location = new System.Drawing.Point(11, 89);
            this.richTextBox_queue_stat.Name = "richTextBox_queue_stat";
            this.richTextBox_queue_stat.Size = new System.Drawing.Size(270, 311);
            this.richTextBox_queue_stat.TabIndex = 50;
            this.richTextBox_queue_stat.Text = "";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(7, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(151, 20);
            this.label16.TabIndex = 49;
            this.label16.Text = "Таблица очередей";
            // 
            // button_queue_stat
            // 
            this.button_queue_stat.Location = new System.Drawing.Point(287, 31);
            this.button_queue_stat.Name = "button_queue_stat";
            this.button_queue_stat.Size = new System.Drawing.Size(151, 23);
            this.button_queue_stat.TabIndex = 48;
            this.button_queue_stat.Text = "Статистика очередей";
            this.button_queue_stat.UseVisualStyleBackColor = true;
            this.button_queue_stat.Click += new System.EventHandler(this.button_queue_stat_Click);
            // 
            // dataGridView_queue_stat
            // 
            this.dataGridView_queue_stat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_queue_stat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_queue_stat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_queue_stat.Location = new System.Drawing.Point(287, 60);
            this.dataGridView_queue_stat.Name = "dataGridView_queue_stat";
            this.dataGridView_queue_stat.Size = new System.Drawing.Size(1158, 471);
            this.dataGridView_queue_stat.TabIndex = 47;
            // 
            // FormQueueStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 554);
            this.Controls.Add(this.button26);
            this.Controls.Add(this.dataGridView_regions);
            this.Controls.Add(this.comboBox_queue1);
            this.Controls.Add(this.richTextBox_queue_stat);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button_queue_stat);
            this.Controls.Add(this.dataGridView_queue_stat);
            this.Name = "FormQueueStat";
            this.Text = "FormQueueStat";
            this.Load += new System.EventHandler(this.FormQueueStat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_regions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_queue_stat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.DataGridView dataGridView_regions;
        private System.Windows.Forms.ComboBox comboBox_queue1;
        private System.Windows.Forms.RichTextBox richTextBox_queue_stat;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button_queue_stat;
        private System.Windows.Forms.DataGridView dataGridView_queue_stat;
    }
}