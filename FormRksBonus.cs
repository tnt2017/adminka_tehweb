using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace adminka
{
    public partial class FormRksBonus : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();

        string RegionId = "20";
        public FormRksBonus()
        {
            InitializeComponent();
        }

        private void FormRksBonus_Load(object sender, EventArgs e)
        {
            dvg.SetDvgStyle(this);
            dataGridView_bonus.DataSource = db_rks.Get_Salary();
            comboBox1.SelectedIndex = 4;
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void FormRksBonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView_bonus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db_rks.AddSalary(RegionId, textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Питер")
            {
                RegionId = "1";
            }
            if (comboBox1.SelectedItem.ToString() == "Почта рц")
            {
                RegionId = "11";
            }
            if (comboBox1.SelectedItem.ToString() == "Ден почта")
            {
                RegionId = "15";
            }
            if (comboBox1.SelectedItem.ToString() == "Казахстан")
            {
                RegionId = "17";
            }
            if (comboBox1.SelectedItem.ToString() == "Почта улет")
            {
                RegionId = "20";
            }
            if (comboBox1.SelectedItem.ToString() == "Новосибирск")
            {
                RegionId = "21";
            }
        }
    }
}
