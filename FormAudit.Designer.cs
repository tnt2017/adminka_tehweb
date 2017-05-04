namespace adminka
{
    partial class FormAudit
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.WorkId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_fio = new System.Windows.Forms.TextBox();
            this.textBox_tel = new System.Windows.Forms.TextBox();
            this.textBox_status = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_rid = new System.Windows.Forms.TextBox();
            this.textBox_wid = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_data = new System.Windows.Forms.TextBox();
            this.textBox_flov = new System.Windows.Forms.TextBox();
            this.textBox_cstatus = new System.Windows.Forms.TextBox();
            this.textBox_nabranni = new System.Windows.Forms.TextBox();
            this.textBox_nomer = new System.Windows.Forms.TextBox();
            this.Найти = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(978, 628);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.WorkId);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox_fio);
            this.tabPage1.Controls.Add(this.textBox_tel);
            this.tabPage1.Controls.Add(this.textBox_status);
            this.tabPage1.Controls.Add(this.textBox_username);
            this.tabPage1.Controls.Add(this.textBox_rid);
            this.tabPage1.Controls.Add(this.textBox_wid);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(970, 602);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Поиск";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "RID";
            // 
            // WorkId
            // 
            this.WorkId.AutoSize = true;
            this.WorkId.Location = new System.Drawing.Point(45, 22);
            this.WorkId.Name = "WorkId";
            this.WorkId.Size = new System.Drawing.Size(42, 13);
            this.WorkId.TabIndex = 23;
            this.WorkId.Text = "WorkId";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Юзер";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(623, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Найти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Статус";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(430, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Фио";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Тел";
            // 
            // textBox_fio
            // 
            this.textBox_fio.Location = new System.Drawing.Point(433, 38);
            this.textBox_fio.Name = "textBox_fio";
            this.textBox_fio.Size = new System.Drawing.Size(184, 20);
            this.textBox_fio.TabIndex = 17;
            // 
            // textBox_tel
            // 
            this.textBox_tel.Location = new System.Drawing.Point(316, 38);
            this.textBox_tel.Name = "textBox_tel";
            this.textBox_tel.Size = new System.Drawing.Size(111, 20);
            this.textBox_tel.TabIndex = 16;
            // 
            // textBox_status
            // 
            this.textBox_status.Location = new System.Drawing.Point(224, 38);
            this.textBox_status.Name = "textBox_status";
            this.textBox_status.Size = new System.Drawing.Size(86, 20);
            this.textBox_status.TabIndex = 15;
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(145, 38);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(73, 20);
            this.textBox_username.TabIndex = 14;
            // 
            // textBox_rid
            // 
            this.textBox_rid.Location = new System.Drawing.Point(100, 38);
            this.textBox_rid.Name = "textBox_rid";
            this.textBox_rid.Size = new System.Drawing.Size(39, 20);
            this.textBox_rid.TabIndex = 13;
            // 
            // textBox_wid
            // 
            this.textBox_wid.Location = new System.Drawing.Point(48, 38);
            this.textBox_wid.Name = "textBox_wid";
            this.textBox_wid.Size = new System.Drawing.Size(46, 20);
            this.textBox_wid.TabIndex = 12;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(870, 393);
            this.dataGridView1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBox_data);
            this.tabPage2.Controls.Add(this.textBox_flov);
            this.tabPage2.Controls.Add(this.textBox_cstatus);
            this.tabPage2.Controls.Add(this.textBox_nabranni);
            this.tabPage2.Controls.Add(this.textBox_nomer);
            this.tabPage2.Controls.Add(this.Найти);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(970, 602);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Записи";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(670, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 20);
            this.button2.TabIndex = 12;
            this.button2.Text = "Послушать запись";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(442, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Дата";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(370, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Flov";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(258, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Статус";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Набранный";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Номер";
            // 
            // textBox_data
            // 
            this.textBox_data.Location = new System.Drawing.Point(445, 41);
            this.textBox_data.Name = "textBox_data";
            this.textBox_data.Size = new System.Drawing.Size(100, 20);
            this.textBox_data.TabIndex = 6;
            // 
            // textBox_flov
            // 
            this.textBox_flov.Location = new System.Drawing.Point(367, 41);
            this.textBox_flov.Name = "textBox_flov";
            this.textBox_flov.Size = new System.Drawing.Size(72, 20);
            this.textBox_flov.TabIndex = 5;
            // 
            // textBox_cstatus
            // 
            this.textBox_cstatus.Location = new System.Drawing.Point(261, 41);
            this.textBox_cstatus.Name = "textBox_cstatus";
            this.textBox_cstatus.Size = new System.Drawing.Size(100, 20);
            this.textBox_cstatus.TabIndex = 4;
            // 
            // textBox_nabranni
            // 
            this.textBox_nabranni.Location = new System.Drawing.Point(141, 41);
            this.textBox_nabranni.Name = "textBox_nabranni";
            this.textBox_nabranni.Size = new System.Drawing.Size(114, 20);
            this.textBox_nabranni.TabIndex = 3;
            // 
            // textBox_nomer
            // 
            this.textBox_nomer.Location = new System.Drawing.Point(35, 41);
            this.textBox_nomer.Name = "textBox_nomer";
            this.textBox_nomer.Size = new System.Drawing.Size(100, 20);
            this.textBox_nomer.TabIndex = 2;
            // 
            // Найти
            // 
            this.Найти.Location = new System.Drawing.Point(560, 41);
            this.Найти.Name = "Найти";
            this.Найти.Size = new System.Drawing.Size(80, 20);
            this.Найти.TabIndex = 1;
            this.Найти.Text = "Найти";
            this.Найти.UseVisualStyleBackColor = true;
            this.Найти.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(22, 75);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(925, 495);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // FormAudit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 652);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormAudit";
            this.Text = "FormAudit";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAudit_FormClosed);
            this.Load += new System.EventHandler(this.FormAudit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_fio;
        private System.Windows.Forms.TextBox textBox_tel;
        private System.Windows.Forms.TextBox textBox_status;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_rid;
        private System.Windows.Forms.TextBox textBox_wid;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label WorkId;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button Найти;
        private System.Windows.Forms.TextBox textBox_data;
        private System.Windows.Forms.TextBox textBox_flov;
        private System.Windows.Forms.TextBox textBox_cstatus;
        private System.Windows.Forms.TextBox textBox_nabranni;
        private System.Windows.Forms.TextBox textBox_nomer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
    }
}