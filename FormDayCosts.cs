using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adminka
{
    public partial class FormDayCosts : Form
    {
        public string RegionId = "";
        DB_RKS db_rks = new DB_RKS();

        public FormDayCosts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string month = dateTimePicker1.Value.Month.ToString();

            if (month.Length == 1)
                month = "0" + month;

            string now = dateTimePicker1.Value.Year.ToString() + "-" + month + "-" + 
                        dateTimePicker1.Value.Day.ToString();

            string q = "SELECT Costs from `DayСosts` WHERE Date='" + now + "'";
            string res=db_rks.SqlQueryWithResult(q);

            if (res != "0")
            {
                string q1 = "UPDATE `DayСosts` SET Costs='" + textBox1.Text + "' WHERE Date='" + now + "'";
                db_rks.SqlQuery(q1, "Расход дня обновлен");
            }
            else
            {
                string q2 = @"INSERT into `DayСosts` VALUES(NULL,'" + RegionId + "','" + now + "'," + textBox1.Text + ");";
                db_rks.SqlQuery(q2, "Расход дня добавлен");
            }
        }

        private void FormDayCosts_Load(object sender, EventArgs e)
        {

        }

        private void FormDayCosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
