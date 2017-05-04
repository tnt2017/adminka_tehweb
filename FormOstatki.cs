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
    public partial class FormOstatki : Form
    {
        public FormOstatki()
        {
            InitializeComponent();
        }
        DB_IDP db_idp = new DB_IDP();
        DB_RKS db_rks = new DB_RKS();

        DVG dvg = new DVG();

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView_ostatki.DataSource = db_idp.GetOstatkiAdmin(db_idp.GetRegionIdByName(comboBox_queue4.SelectedItem.ToString()), checkBox_hide_null.Checked);
            if (dataGridView_ostatki.DataSource != null)
                dataGridView_ostatki.Sort(dataGridView_ostatki.Columns[1], ListSortDirection.Ascending);

            double a = 0, b = 0, c = 0, d = 0;

            for (int i = 0; i < dataGridView_ostatki.Rows.Count-1; i++)
            {
                if (dataGridView_ostatki.Rows[i].Cells[4].Value.ToString() == "")
                {
                    dataGridView_ostatki.Rows[i].Cells[4].Value = "0";
                }

                if (dataGridView_ostatki.Rows[i].Cells[0].Value.ToString() == "")
                {
                    int ost = Convert.ToInt32(dataGridView_ostatki.Rows[i].Cells[2].Value.ToString()) - Convert.ToInt32(dataGridView_ostatki.Rows[i].Cells[3].Value.ToString());
                    dataGridView_ostatki.Rows[i].Cells[0].Value = ost.ToString();
                }

                if (dataGridView_ostatki.Rows[i].Cells[0].Value.ToString() != "")
                    a += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[0].Value);

                if (dataGridView_ostatki.Rows[i].Cells[2].Value.ToString() != "")
                    b += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[2].Value);

                if (dataGridView_ostatki.Rows[i].Cells[3].Value.ToString() != "")
                    c += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[3].Value);

                if (dataGridView_ostatki.Rows[i].Cells[4].Value.ToString() != "")
                    d += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[4].Value);
            }

            richTextBox1.Text = "Остатки: " + a.ToString() + Environment.NewLine +
                                "Всего " + b.ToString() + Environment.NewLine +
                                "Расход " + c.ToString() + Environment.NewLine +
                                "Резерв " + d.ToString();
        }

        private void FormOstatki_Load(object sender, EventArgs e)
        {
            comboBox_queue4.SelectedIndex = 4;
            dvg.SetDvgStyle(this);
        }

        private void dataGridView_ostatki_Click(object sender, EventArgs e)
        {
            string s = dataGridView_ostatki.CurrentRow.Cells[1].Value.ToString();

            string dt1 = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
            string dt2 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + dateTimePicker2.Value.Day.ToString();


            string expr = " AND DateDelivery>'" + dt1 + "' AND DateDelivery<'" + dt2 + "'";

            string s1 = db_rks.SqlQueryWithResult("SELECT COUNT(*) from RKS WHERE PriceName LIKE '%" + s + "%'" + expr);
            string s2 = db_rks.SqlQueryWithResult("SELECT COUNT(*) from RKS WHERE PriceName LIKE '%" + s + "%' AND StatusId = 7" + expr);
            double proc = Convert.ToDouble(s2) / Convert.ToDouble(s1)*100;
            label1.Text = s + " заказов " + s1 + " доставлено " + s2 + " Процент: " + proc;
        }

        private void dataGridView_ostatki_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
