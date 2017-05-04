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
    public partial class FormDayStat : Form
    {
        public FormDayStat()
        {
            InitializeComponent();
        }

        DB_IDP db_idp = new DB_IDP();
        DB_RKS db_rks = new DB_RKS();
        DVG dvg = new DVG();

        DataTable GetDayStats(string RegionId, string dt) //
        {
            string q = @"SELECT UserName, StatusName, SUM(TotalSumm) as `SM`, AVG(TotalSumm) as AVG, COUNT(*) as `Заказов`
                    FROM RKS
                    WHERE RegionId = " + RegionId + " AND DATE_FORMAT(DateDelivery, '%Y-%m-%d') = '" + dt + "'  GROUP BY  StatusId, UserId";
            return db_rks.Get_DataTable(q);
        }
        

        private void FormDayStat_Load(object sender, EventArgs e)
        {
            dvg.SetDvgStyle(this);
            comboBox1.SelectedIndex = 0;
        }

        private void FormDayStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dt = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            dataGridView1.DataSource = GetDayStats(db_idp.GetRegionId_ByName(comboBox1.SelectedItem.ToString()), dt);
         }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Count > 3)
            {
                dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Descending);
            }

            int summ = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string num = dataGridView1.Rows[i].Cells[4].Value.ToString();
                summ += Convert.ToInt32(num);
            }
            label1.Text = "Всего заказов: " + summ.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
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
