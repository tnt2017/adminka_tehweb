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
{    public partial class FormUsers : Form
    {
        DB_IDP db_idp = new DB_IDP();
        DB_RKS db_rks = new DB_RKS();
        DVG dgv = new DVG();
        public FormUsers()
        {
            InitializeComponent();
        }

        public void ReloadTables()
        {
            dataGridView_users.DataSource = db_idp.GetUsers("2");
           /* dataGridView_limits.DataSource = db_idp.GetLimits();

            dataGridView_limits.Columns[1].HeaderText = "Оператор";
            dataGridView_limits.Columns[2].HeaderText = "РКС";
            dataGridView_limits.Columns[3].HeaderText = "Очередь";
            dataGridView_limits.Columns[4].HeaderText = "Лимит";

            DataTable dt = db_idp.GetUsers("2");

            comboBox_oper.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_oper.Items.Add(dt.Rows[i][1].ToString());
            }

            try
            {
                comboBox_oper.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка назначения индекса");
            }*/
        }


        private void FormUsers_Load(object sender, EventArgs e)
        {
            ReloadTables();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_users.RowCount; i++)
            {
                for (int j = 0; j < dataGridView_users.ColumnCount; j++)
                {
                    if (dataGridView_users[j, i].Selected)
                    {
                        string s = dataGridView_users[0, i].Value.ToString();
                        //MessageBox.Show(dataGridView_users[0, i].Value.ToString());
                        string st = new_stavka.Text;
                        string q = "UPDATE Users SET Stavka=" + st + ", ch0=" + st + ", ch1=" + st + ", ch2=" + st + ", ch3=" + st + " WHERE id='" + s + "'";
                        db_idp.SqlQuery(q, "");//Поменяли ставку
                        continue;
                    }
                }
            }

            ReloadTables();

            /* string s = dataGridView_users.CurrentRow.Cells[0].Value.ToString();

             */
        }
    }
}
