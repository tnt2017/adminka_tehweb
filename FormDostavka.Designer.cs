namespace adminka
{
    partial class FormDostavka
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView_users = new System.Windows.Forms.DataGridView();
            this.button_arrived = new System.Windows.Forms.Button();
            this.button_bought = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Call = new System.Windows.Forms.Button();
            this.textBox_uname = new System.Windows.Forms.TextBox();
            this.textBox_groupid = new System.Windows.Forms.TextBox();
            this.button_notify = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_arrived_plus = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button15 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_users)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(24, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 66;
            this.dataGridView1.Size = new System.Drawing.Size(1519, 611);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            this.dataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dataGridView1_PreviewKeyDown);
            // 
            // dataGridView_users
            // 
            this.dataGridView_users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView_users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_users.Location = new System.Drawing.Point(1186, 398);
            this.dataGridView_users.Name = "dataGridView_users";
            this.dataGridView_users.Size = new System.Drawing.Size(107, 20);
            this.dataGridView_users.TabIndex = 2;
            this.dataGridView_users.Visible = false;
            this.dataGridView_users.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_users_CellContentClick);
            // 
            // button_arrived
            // 
            this.button_arrived.Location = new System.Drawing.Point(256, 11);
            this.button_arrived.Name = "button_arrived";
            this.button_arrived.Size = new System.Drawing.Size(75, 20);
            this.button_arrived.TabIndex = 4;
            this.button_arrived.Text = "Прибыл";
            this.button_arrived.UseVisualStyleBackColor = true;
            this.button_arrived.Click += new System.EventHandler(this.button_pribil_Click);
            // 
            // button_bought
            // 
            this.button_bought.Location = new System.Drawing.Point(418, 10);
            this.button_bought.Name = "button_bought";
            this.button_bought.Size = new System.Drawing.Size(75, 20);
            this.button_bought.TabIndex = 6;
            this.button_bought.Text = "Выкуплен";
            this.button_bought.UseVisualStyleBackColor = true;
            this.button_bought.Click += new System.EventHandler(this.button_bought_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(694, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // Call
            // 
            this.Call.Location = new System.Drawing.Point(1411, 66);
            this.Call.Name = "Call";
            this.Call.Size = new System.Drawing.Size(75, 23);
            this.Call.TabIndex = 13;
            this.Call.Text = "Call";
            this.Call.UseVisualStyleBackColor = true;
            this.Call.Visible = false;
            this.Call.Click += new System.EventHandler(this.Call_Click);
            // 
            // textBox_uname
            // 
            this.textBox_uname.Location = new System.Drawing.Point(24, 12);
            this.textBox_uname.Name = "textBox_uname";
            this.textBox_uname.Size = new System.Drawing.Size(64, 20);
            this.textBox_uname.TabIndex = 15;
            // 
            // textBox_groupid
            // 
            this.textBox_groupid.Location = new System.Drawing.Point(93, 11);
            this.textBox_groupid.Name = "textBox_groupid";
            this.textBox_groupid.Size = new System.Drawing.Size(21, 20);
            this.textBox_groupid.TabIndex = 16;
            // 
            // button_notify
            // 
            this.button_notify.Location = new System.Drawing.Point(502, 10);
            this.button_notify.Name = "button_notify";
            this.button_notify.Size = new System.Drawing.Size(75, 20);
            this.button_notify.TabIndex = 18;
            this.button_notify.Text = "Заметки";
            this.button_notify.UseVisualStyleBackColor = true;
            this.button_notify.Click += new System.EventHandler(this.button_notify_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1411, 95);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 20);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "25";
            this.textBox1.Visible = false;
            // 
            // button_arrived_plus
            // 
            this.button_arrived_plus.Location = new System.Drawing.Point(337, 10);
            this.button_arrived_plus.Name = "button_arrived_plus";
            this.button_arrived_plus.Size = new System.Drawing.Size(75, 21);
            this.button_arrived_plus.TabIndex = 20;
            this.button_arrived_plus.Text = "Прибыл +";
            this.button_arrived_plus.UseVisualStyleBackColor = true;
            this.button_arrived_plus.Click += new System.EventHandler(this.button_arrived_plus_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(583, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 20);
            this.button1.TabIndex = 21;
            this.button1.Text = "Отправленные";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(121, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(857, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Поиск";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(747, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(104, 20);
            this.textBox2.TabIndex = 24;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(1186, 66);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 25;
            this.listBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(941, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "label2";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(985, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "Сохранить ширину столбцов";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(1288, 5);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(122, 23);
            this.button15.TabIndex = 136;
            this.button15.Text = "Положить трубку";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1154, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 23);
            this.button4.TabIndex = 137;
            this.button4.Text = "Звонок с мобильного";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FormDostavka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 689);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_arrived_plus);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_notify);
            this.Controls.Add(this.textBox_groupid);
            this.Controls.Add(this.textBox_uname);
            this.Controls.Add(this.Call);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_bought);
            this.Controls.Add(this.button_arrived);
            this.Controls.Add(this.dataGridView_users);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormDostavka";
            this.Text = "FormDostavka";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDostavka_FormClosed);
            this.Load += new System.EventHandler(this.FormDostavka_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormDostavka_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_users)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView_users;
        private System.Windows.Forms.Button button_arrived;
        private System.Windows.Forms.Button button_bought;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Call;
        public System.Windows.Forms.TextBox textBox_uname;
        private System.Windows.Forms.TextBox textBox_groupid;
        private System.Windows.Forms.Button button_notify;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_arrived_plus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button4;
    }
}