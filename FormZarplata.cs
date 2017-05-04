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
    public partial class FormZarplata : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();

        public FormZarplata()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }

        private void FormZarplata_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db_idp.Get_DataTable("SELECT Id, Name from Users");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                int sovpad = 0;
                string s1 = dataGridView2.Rows[i].Cells[0].Value.ToString();

                for (int j = 0; j < dataGridView3.RowCount; j++)
                {
                    string s2 = dataGridView3.Rows[j].Cells[0].Value.ToString();

                    if (s1 == s2)
                    {
                        //MessageBox.Show(s1 + " " + s2);
                        sovpad = 1;
                    }
                }

                if (sovpad == 0)
                {
                    //MessageBox.Show(s1);
                    listBox1.Items.Add(s1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
        }

 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string q = "SELECT* from accruals WHERE workid = " + listBox1.Items[i].ToString();
                DataTable dt= db_idp.Get_DataTable(q);
                if (dt.Rows.Count == 0)
                {
                    listBox2.Items.Add(listBox1.Items[i].ToString());
                    //MessageBox.Show(listBox1.Items[i].ToString() + " - нет  в accruals");
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 1; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                dataGridView1_CellMouseDoubleClick(null,null);
                Application.DoEvents();
                button1_Click(null, null);
                Application.DoEvents();
                string un = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                for (int j = 0; j < listBox1.Items.Count; j++)
                {
                    textBox1.Text = textBox1.Text + un + ":" + listBox1.Items[j].ToString() + "\r\n";
                } 

                button1_Click_1(null, null);
                Application.DoEvents();

                for (int j = 0; j < listBox2.Items.Count; j++)
                {
                    textBox2.Text = textBox2.Text + un + ":" + listBox2.Items[j].ToString() + "\r\n";
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string uid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string uname = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            string s1 = "SELECT WorkId from RKS WHERE UserName='" + uname + "' AND StatusId=7";
            //MessageBox.Show(s1);
            dataGridView2.DataSource = db_rks.Get_DataTable(s1);

            string s2 = "SELECT * from Work WHERE UserId='" + uid + "' AND StatusId=7";
            //MessageBox.Show(s2);
            dataGridView3.DataSource = db_idp.Get_DataTable(s2);

            string s3 = "SELECT * from accruals WHERE UserId='" + uid + "'";
            // MessageBox.Show(s3);
            dataGridView4.DataSource = db_idp.Get_DataTable(s3);

            label1.Text = dataGridView2.RowCount.ToString();
            label2.Text = dataGridView3.RowCount.ToString();
            label3.Text = dataGridView4.RowCount.ToString();

            string s4 = "SELECT SUM(TotalSumm), SUM(Bonus) from RKS where UserName = '" + uname + "'";
            //MessageBox.Show(s4);
            dataGridView5.DataSource = db_idp.Get_DataTable(s4);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormZP2 f = new FormZP2();
            f.Show();
        }
    }
}
