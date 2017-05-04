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
    public partial class FormZP2 : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();

        public FormZP2()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string s=listBox1.SelectedItem.ToString();
            int i=s.IndexOf(":");
            s=s.Substring(i+1,s.Length-i-1);
            MessageBox.Show(s);
            dataGridView1.DataSource = db_rks.Get_DataTable("SELECT id, UserName, WorkId, TotalSumm, Bonus from RKS WHERE workid=" + s);
        }

        private void FormZP2_Load(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt_full = new DataTable();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                string s = listBox1.SelectedItem.ToString();
                int n = s.IndexOf(":");
                s = s.Substring(n + 1, s.Length - n - 1);
                //MessageBox.Show(s);
                DataTable dt=db_rks.Get_DataTable("SELECT id, UserName, WorkId, TotalSumm, Bonus from RKS WHERE workid=" + s);
                //dataGridView1.DataSource = dt;
                dt_full.Merge(dt);
                dataGridView1.DataSource = dt_full;
                Application.DoEvents();
            }
        }
    }
}
