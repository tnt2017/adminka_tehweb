namespace adminka
{
    partial class FormUsers
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
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridView_users = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.new_stavka = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_users)).BeginInit();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(9, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(185, 20);
            this.label14.TabIndex = 28;
            this.label14.Text = "Список пользователей";
            // 
            // dataGridView_users
            // 
            this.dataGridView_users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView_users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_users.Location = new System.Drawing.Point(12, 43);
            this.dataGridView_users.Name = "dataGridView_users";
            this.dataGridView_users.Size = new System.Drawing.Size(477, 431);
            this.dataGridView_users.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Поменять ставку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // new_stavka
            // 
            this.new_stavka.Location = new System.Drawing.Point(499, 88);
            this.new_stavka.Name = "new_stavka";
            this.new_stavka.Size = new System.Drawing.Size(129, 20);
            this.new_stavka.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(496, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Новая ставка";
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 486);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.new_stavka);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dataGridView_users);
            this.Name = "FormUsers";
            this.Text = "FormUsers";
            this.Load += new System.EventHandler(this.FormUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_users)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridView_users;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox new_stavka;
        private System.Windows.Forms.Label label1;
    }
}