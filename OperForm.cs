using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace adminka
{
    public partial class OperForm : Form
    {
        public OperForm()
        {
            InitializeComponent();
        }

        DB db = new DB();       
        
        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView_zakazi.MultiSelect = false;
            dataGridView_zakazi.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView4.AllowUserToAddRows = false;

            comboBox1.SelectedIndex = 1;
        }

        void ShowSelected(int i)
        {
            if (dataGridView_zakazi.RowCount > 0)
            {
                //MessageBox.Show(dataGridView_zakazi.SelectedCells[0].RowIndex.ToString());
                //int i = dataGridView_zakazi.SelectedCells[0].RowIndex;
                dataGridView_zakazi.Rows[i].Selected = true;

                textBox_cid.Text = dataGridView_zakazi.Rows[i].Cells["num"].Value.ToString();
                textBox_status.Text = dataGridView_zakazi.Rows[i].Cells["status"].Value.ToString();
                textBox_fio.Text = dataGridView_zakazi.Rows[i].Cells["Fio"].Value.ToString(); //dataGridView_zakazi[3, i].Value.ToString();
                textBox_phone.Text = dataGridView_zakazi.Rows[i].Cells["Tel"].Value.ToString(); //dataGridView_zakazi[4, i].Value.ToString();

                string full_addr = dataGridView_zakazi.Rows[i].Cells["Adr"].Value.ToString(); //dataGridView_zakazi[5, i].Value.ToString();
                richTextBox_address.Text = full_addr;

                String[] words = full_addr.Split(new char[] { ',' });

                // MessageBox.Show(words.Length.ToString());
                //string Oblast = words[1];
                //string Raion = words[2];
                //string Gorod = words[3];
                //string Ulica = words[4];
                //textBox_index.Text = words[1];

                //DataGridView dataGridView3 = new DataGridView();
                dataGridView3.DataSource = db.GetSQL("SELECT pass from Clients WHERE tel='" + textBox_phone.Text + "'");
                string s = dataGridView3[0, 0].Value.ToString();
                textBox_lkpass.Text = s;
                richTextBox_sms.Text = "yourspresent.ru Ваш ключ пароль " + s;
                //textBox_lkpass.Text = db.GetSQL2("SELECT pass from Clients WHERE tel='" + textBox_phone.Text + "'");

                dataGridView2.DataSource = db.GetHistory(textBox_cid.Text);
            }
        }

         public string GetRegionId()
         {
             string s3 = "";
             if (comboBox1.SelectedItem.ToString() == "Почта рц")
                 s3 = "11";

             if (comboBox1.SelectedItem.ToString() == "Ден почта")
                 s3 = "15";

             if (comboBox1.SelectedItem.ToString() == "Казахстан")
                 s3 = "17";

             return s3;
         }
        
        private void button1_Click(object sender, EventArgs e)
        { 
            dataGridView_zakazi.DataSource = db.GetZakazi(textBox_uid.Text);
            ShowSelected(0);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            comboBox_tov1.SelectedIndex = 0;
            tov1_ost.Text = "";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            comboBox_tov2.SelectedIndex = 0;
            tov2_ost.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            comboBox_tov3.SelectedIndex = 0;
            tov3_ost.Text = "";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            comboBox_tov4.SelectedIndex = 0;
            tov4_ost.Text = "";
        }

        private void button22_Click(object sender, EventArgs e)
        {
      
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox_sms.Text = "yourspresent.ru Ваш ключ пароль " + textBox_lkpass.Text;
        }
        
        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox_sms.Text = "Здравствуйте, активируйте свои бонусные баллы 2012 рублей до 02.11.2016 Сайт yourspresent.ru Ваш ключ пароль " + textBox_lkpass.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            richTextBox_sms.Text = textBox_fio.Text + " ваши бонусы активированы на Ионизатор воздуха К оплате сумма 9600";
        }

        private void comboBox_tov1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
            {
                if (comboBox_tov1.SelectedItem == dataGridView_ostatki[1, i].Value)
                    tov1_ost.Text=dataGridView_ostatki[0, i].Value.ToString();
            }
        }

        private void comboBox_tov2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
            {
                if (comboBox_tov2.SelectedItem == dataGridView_ostatki[1, i].Value)
                    tov2_ost.Text = dataGridView_ostatki[0, i].Value.ToString();
            }
        }

        private void comboBox_tov3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
            {
                if (comboBox_tov3.SelectedItem == dataGridView_ostatki[1, i].Value)
                    tov3_ost.Text = dataGridView_ostatki[0, i].Value.ToString();
            }
        }

        private void comboBox_tov4_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
            {
                if (comboBox_tov4.SelectedItem == dataGridView_ostatki[1, i].Value)
                    tov4_ost.Text = dataGridView_ostatki[0, i].Value.ToString();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click_1(object sender, EventArgs e)
        {
            DataTable dt = db.GetOstatki(GetRegionId());
            DataRow[] foundRows;
            foundRows = dt.Select("ostatok > 0");//ostatok > 0
            dt = foundRows.CopyToDataTable();
            dataGridView_ostatki.DataSource = dt;
            dataGridView_ostatki.Sort(dataGridView_ostatki.Columns[0], ListSortDirection.Descending);

            for (int i = 1; i < dataGridView_ostatki.RowCount - 1; i++)
            {
                //string s1 = dataGridView_ostatki[0, i].Value.ToString();
                //string s2 = dataGridView_ostatki[1, i].Value.ToString();

                if (dataGridView_ostatki[1, i].Value != null)
                {
                    string s = dataGridView_ostatki[1, i].Value.ToString();
                    comboBox_tov1.Items.Add(s);
                    comboBox_tov2.Items.Add(s);
                    comboBox_tov3.Items.Add(s);
                    comboBox_tov4.Items.Add(s);
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            dataGridView_zakazi.Rows[dataGridView_zakazi.SelectedCells[0].RowIndex + 1].Selected = true;
            ShowSelected(dataGridView_zakazi.SelectedCells[0].RowIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=db.GetUserInfo(textBox_uid.Text);

            string id = dataGridView1.CurrentRow.Cells["id1"].Value.ToString();
            string summ = dataGridView1.CurrentRow.Cells["summ"].Value.ToString();
            string stavka= dataGridView1.CurrentRow.Cells["stavka"].Value.ToString();
            string dost = dataGridView1.CurrentRow.Cells["dost"].Value.ToString();

            double ZP = Convert.ToDouble(summ) / 100 * Convert.ToDouble(stavka);
            double srcheck = Convert.ToDouble(summ) / Convert.ToDouble(dost);

            string s = "ID: " + id + "\r\n" +
                     "Доставлено :" + dost + "\r\n" +
                     "Выручка: " + summ + "\r\n" +
                     "Ставка: " + stavka + "\r\n" +
                     "Ср.чек: " + srcheck + "\r\n" +
                     "ЗП: " + ZP + "\r\n";

            MessageBox.Show(s);

            //$paid = mysql_result((mysql_query("SELECT  SUM(`Sum`) AS p FROM `Zp` WHERE `UserId` = $uid;")), 0);
        }

        private void dataGridView_zakazi_SelectionChanged(object sender, EventArgs e)
        {
            label24.Text = dataGridView_zakazi.RowCount.ToString();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox_uid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
