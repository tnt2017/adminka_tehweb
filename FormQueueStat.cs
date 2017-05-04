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
    public partial class FormQueueStat : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dgv = new DVG();

        public FormQueueStat()
        {
            InitializeComponent();
            dgv.SetDvgStyle(this);
        }

        public void ReloadRKSComboBox()
        {
            comboBox_queue1.Items.Clear();
            comboBox_queue1.Items.Add("Все");

            for (int i = 0; i < db_idp.table_regions.Rows.Count; i++)
            {
                comboBox_queue1.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
           }

           /* dataGridView_rks.DataSource = db_idp.GetRKS();

            comboBox_rks.Items.Clear();
            for (int i = 0; i < dataGridView_rks.Rows.Count; i++)
            {
                comboBox_rks.Items.Add(dataGridView_rks[1, i].Value.ToString());
            }
            comboBox_rks.SelectedIndex = 0;*/
        }
        private void FormQueueStat_Load(object sender, EventArgs e)
        {
            dataGridView_regions.DataSource = db_idp.table_regions;
            ReloadRKSComboBox();
            comboBox_queue1.SelectedIndex = 0;
        }

        private void button_queue_stat_Click(object sender, EventArgs e)
        {
            DataTable dt_full = new DataTable();

            if (dataGridView_regions.CurrentRow != null)
            {
                for (int i = 0; i < dataGridView_regions.Rows.Count; i++)
                {
                    DataTable dt = db_idp.GetQueueStat(dataGridView_regions.Rows[i].Cells["id"].Value.ToString());
                    if (dt != null)
                    {
                        dt_full.Merge(dt);
                        dataGridView_queue_stat.DataSource = dt_full;
                    }
                    Application.DoEvents();
                }
            }
        }

        private void comboBox_queue1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_queue1.SelectedItem.ToString() == "Все")
                richTextBox_queue_stat.Text = db_idp.GetQueueStatFullText("0");
            else
                richTextBox_queue_stat.Text = db_idp.GetQueueStatFullText(db_idp.GetRegionIdByName(comboBox_queue1.SelectedItem.ToString()));
        }
    }
}
