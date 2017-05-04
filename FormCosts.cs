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
    public partial class FormCosts : Form
    {
        public string current_id = "";
        int vhojdenienum = 0;

        public FormCosts()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }

        DB_RKS db_rks = new DB_RKS();
        DVG dvg = new DVG();


        void GetInfo()
        {
            dataGridView1.DataSource = db_rks.GetCosts(current_id);
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[5].Width = 300;
            label1.Text = "Сальдо: " + db_rks.GetSaldo(current_id);
        }

        private void FormCosts_Load(object sender, EventArgs e)
        {
             GetInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
         }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
         }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void FormCosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            db_rks.AddCosts(current_id, textBox_debet.Text, textBox_credit.Text, textBox_comment.Text);
            GetInfo();
            textBox_debet.Text = "";
            textBox_credit.Text = "";
            textBox_comment.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text.ToLower();
            listBox1.Items.Clear();

            if (s != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                }
                // FormatColumns();

                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        string cell = dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower();
                        if (cell.IndexOf(s) > -1)
                        {
                            listBox1.Items.Add(i.ToString());
                        }
                    }
                }


                if (listBox1.Items.Count == vhojdenienum)
                    vhojdenienum = 0;

                label5.Text = (vhojdenienum + 1).ToString() + " из " + listBox1.Items.Count.ToString();

                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = vhojdenienum;
                    int num = Convert.ToInt32(listBox1.SelectedItem.ToString());
                    dataGridView1.FirstDisplayedScrollingRowIndex = num;
                    dataGridView1.Rows[num].DefaultCellStyle.BackColor = Color.Orange;
                    dataGridView1.Rows[num].Selected = true;
                    vhojdenienum++;
                }





            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button8_Click(sender, e);
            }
        }
    }
}
