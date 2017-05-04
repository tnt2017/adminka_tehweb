using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace adminka
{
    public partial class AdminForm : Form
    {
        DB_IDP db_idp = new DB_IDP();
        DB_RKS db_rks = new DB_RKS();
        DVG dgv = new DVG();
        bool first_start = true;
        public string username;

        public AdminForm()
        {
            InitializeComponent();
            dgv.SetDvgStyle(tabControl1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView_regions.DataSource = db_idp.table_regions;
            ReloadRKSComboBox();
            comboBox_queue4.SelectedIndex = 1;
            comboBox_usertype.SelectedIndex = 1;
            comboBox_queue1.SelectedIndex = 0;
            comboBox_queue2.SelectedIndex = 0;
            comboBox_queue3.SelectedIndex = 0;
            comboBox_queue4.SelectedIndex = 1;
            comboBox_queue5.SelectedIndex = 0;
            comboBox_queue6.SelectedItem = 0;
            comboBox_queue7.SelectedItem = 0;
            comboBox_valute.SelectedIndex = 0;
            //comboBox_status.SelectedIndex = 0;
            comboBox_region.SelectedIndex = 1;
            dataGridView_zp1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_pricelist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ReloadTables()
        {
            dataGridView_users.DataSource = db_idp.GetUsers("");
            dataGridView_limits.DataSource = db_idp.GetLimits();

            dataGridView_limits.Columns[1].HeaderText = "Оператор";
            dataGridView_limits.Columns[2].HeaderText = "РКС";
            dataGridView_limits.Columns[3].HeaderText = "Очередь";
            dataGridView_limits.Columns[4].HeaderText = "Лимит";
            
            DataTable dt=db_idp.GetUsers("2");

            comboBox_oper.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox_oper.Items.Add(dt.Rows[i][1].ToString());
            }

            try
            {
                comboBox_oper.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка назначения индекса");
            }
        }
        
        public void ReloadTableTovars()
        {
            dataGridView_tovars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView_tovars.DataSource = db_idp.GetTovars();

            comboBox_tovars1.Items.Clear();
            comboBox_tovars2.Items.Clear();

            for (int i = 0; i < dataGridView_tovars.RowCount; i++)
            {
                string s=dataGridView_tovars[1, i].Value.ToString();
                comboBox_tovars1.Items.Add(s);
                comboBox_tovars2.Items.Add(s);
            }
            comboBox_tovars1.SelectedIndex = 0;
            comboBox_tovars2.SelectedIndex = 0;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView_zakaz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView_zakaz.DataSource = db_idp.FindOrder(textBox_id.Text, "", comboBox_status.SelectedItem.ToString(), textBox_phone.Text, textBox_fio.Text, textBox_address.Text);
            string now = DateTime.Now.ToString("yyyy-MM-dd");
            dataGridView_zakaz.DataSource = db_rks.GetRKS(richTextBox1, "2016-01-01", now, "0", "", textBox_filter.Text, false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView_zp1.DataSource=db_idp.Get_idpoints_ZP(comboBox_region.SelectedItem.ToString(), checkBox_ShowBenuk.Checked);

            /*dataGridView_zp1.Columns[0].HeaderText = "ID";
            dataGridView_zp1.Columns[1].HeaderText = "Пользователь";
            dataGridView_zp1.Columns[2].HeaderText = "Дост";
            dataGridView_zp1.Columns[3].HeaderText = "На сумму";
            dataGridView_zp1.Columns[4].HeaderText = "Ср. чек";
            dataGridView_zp1.Columns[5].HeaderText = "Начислено";
            dataGridView_zp1.Columns[6].HeaderText = "Оплачено";
            dataGridView_zp1.Columns[7].HeaderText = "Остаток";
            dataGridView_zp1.Columns[8].HeaderText = "*";*/

            dataGridView_zp1.Columns["ID"].Width = 40;
            dataGridView_zp1.Columns["Дост."].Width = 50;
            dataGridView_zp1.Columns["valuta"].Width = 40;
            dataGridView_zp1.Columns["Ср. чек"].Width = 80;
            
            dataGridView_zp1.Sort(dataGridView_zp1.Columns[1], ListSortDirection.Ascending);
            label_orders_count.Text = dataGridView_zp1.RowCount.ToString();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox_xls_file.Text = openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReloadTableTovars();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db_idp.InsertTovar(textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            ReloadTableTovars();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            //button6_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            db_idp.DeleteTovar(dataGridView_tovars.CurrentRow.Cells[0].Value.ToString());
            ReloadTableTovars();
        }

         private void tabPage1_Enter(object sender, EventArgs e)
        {
            //ReloadTables();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            dataGridView_queue.ColumnCount = 8;
            Excel._Application excelapp = new Excel.Application();
            excelapp.Workbooks.Open(textBox_xls_file.Text);
            Excel.Worksheet activeSheet = (Excel.Worksheet)excelapp.ActiveWorkbook.ActiveSheet;
            MessageBox.Show("Строк в файле: " + activeSheet.Rows.Count.ToString());
            dataGridView_queue.Rows.Add(activeSheet.Rows.Count);

            progressBar1.Maximum = activeSheet.Rows.Count;
            int n = activeSheet.Rows.Count;
            n = 6000;

            for (int i = 0; i < n; i++) //activeSheet.Rows.Count
            {
                for (int j = 0; j < 7; j++)
                {
                    Excel.Range range = (Excel.Range)activeSheet.Cells[i + 1, j + 1];
                    dataGridView_queue[j, i].Value = range.Value;
                    Application.DoEvents();
                }
                progressBar1.Value = i;
                label_progress.Text = i.ToString() + " из " + n.ToString();

                string s = "INSERT into `Clients` SET RegionId='"+ textBox_rid.Text +"', Fio='" + dataGridView_queue[1, i].Value + "' " + ", Tel='" + dataGridView_queue[2, i].Value + "' , Adr='" + dataGridView_queue[3, i].Value + "' , LastZakaz='" + dataGridView_queue[4, i].Value + "' , Pass='" + dataGridView_queue[6, i].Value + "'";
                richTextBox1.AppendText(s + "\r\n");

                if (checkBox1.Checked)
                {
                    db_idp.SqlQuery(s,"");
                }
               
                dataGridView_queue[0, i].Value = i;
            }

            excelapp.Quit();
        }

        public void ReloadRKSComboBox()
        {
            comboBox_queue1.Items.Clear();
            comboBox_queue2.Items.Clear();
            comboBox_queue3.Items.Clear();
            comboBox_queue4.Items.Clear();
            comboBox_queue5.Items.Clear();
            comboBox_queue6.Items.Clear();

            comboBox_queue1.Items.Add("Все");
            comboBox_queue2.Items.Add("Все");
            comboBox_queue3.Items.Add("Все");
            comboBox_queue4.Items.Add("Все");
            comboBox_queue5.Items.Add("Все");
            comboBox_queue6.Items.Add("Все");

            for (int i = 0; i < db_idp.table_regions.Rows.Count; i++)
            {
                comboBox_queue1.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue2.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue3.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue4.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue5.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue6.Items.Add(db_idp.table_regions.Rows[i][1].ToString());
                comboBox_queue7.Items.Add(db_idp.table_regions.Rows[i][1].ToString());

            }

            dataGridView_rks.DataSource = db_idp.GetRKS();

            comboBox_rks.Items.Clear();
            for (int i = 0; i < dataGridView_rks.Rows.Count; i++)
            {
                comboBox_rks.Items.Add(dataGridView_rks[1, i].Value.ToString());
            }
            comboBox_rks.SelectedIndex = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView_ostatki.DataSource = db_idp.GetOstatkiAdmin(db_idp.GetRegionIdByName(comboBox_queue4.SelectedItem.ToString()), checkBox_hide_null.Checked);
            if(dataGridView_ostatki.DataSource!=null)
            dataGridView_ostatki.Sort(dataGridView_ostatki.Columns[1], ListSortDirection.Ascending);

            double a=0, b=0,c=0,d=0;

            for (int i = 0; i < dataGridView_ostatki.Rows.Count; i++)
            {
                if (dataGridView_ostatki.Rows[i].Cells[0].Value.ToString() == "")
                {
                    int ost=Convert.ToInt32(dataGridView_ostatki.Rows[i].Cells[2].Value.ToString()) - Convert.ToInt32(dataGridView_ostatki.Rows[i].Cells[3].Value.ToString());
                    dataGridView_ostatki.Rows[i].Cells[0].Value = ost.ToString();
                }

                if (dataGridView_ostatki.Rows[i].Cells[0].Value.ToString()!="")
                    a += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[0].Value);

                if (dataGridView_ostatki.Rows[i].Cells[2].Value.ToString() != "")
                    b += Convert.ToDouble(dataGridView_ostatki.Rows[i].Cells[2].Value);

            }

            richTextBox2.Text = "Остатки: " + a.ToString() + Environment.NewLine +
                                "Всего " + b.ToString() + Environment.NewLine +
                                "Расход " + c.ToString() + Environment.NewLine +
                                "Резерв " + d.ToString();
        }

        private void dataGridView_regions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {
        }

        private void tabPage8_Enter(object sender, EventArgs e)
        {
            button13_Click_1(sender, e);
        }
                
        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView_comments.DataSource = db_idp.GetComments();
            dataGridView_comments.Columns["tin"].Width = 300;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            db_idp.InsertUser(textBox_login.Text, textBox_password.Text, (comboBox_usertype.SelectedIndex+1).ToString(), textBox_stavka.Text);
            //db_rks.InsertUser(textBox_login.Text, textBox_password.Text, (comboBox_usertype.SelectedIndex + 1).ToString(), textBox_stavka.Text);
            ReloadTables();
            ReloadRKSComboBox();
            textBox_login.Text = "";
            textBox_password.Text = "";
        }

        private void AdminForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            comboBox_region.SelectedIndex = 0;
        }

        string GetUserIdByName(string s)
        {
            string uid = "";
            foreach (DataGridViewRow row in dataGridView_users.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(s))
                {
                    uid = row.Cells[0].Value.ToString();
                    return uid;
                }
            }
            return "";
        }

        string GetRksIdByName(string s)
        {
            string uid = "";
            foreach (DataGridViewRow row in dataGridView_rks.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(s))
                {
                    uid = row.Cells[0].Value.ToString();
                    return uid;
                }
            }
            return "";
        }
        
        private void button14_Click(object sender, EventArgs e)
        {
            string uid = GetUserIdByName(comboBox_oper.SelectedItem.ToString());
            string RksId = GetRksIdByName(comboBox_rks.SelectedItem.ToString());
            string RegionId = db_idp.GetRegionIdByName(comboBox_queue2.SelectedItem.ToString());
            db_idp.SetQueue(uid, RksId, RegionId, textBox_lim.Text);
            ReloadTables();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView_users_MouseUp(object sender, MouseEventArgs e)
        {
            for(int i=0;i< comboBox_oper.Items.Count;i++)
            {
                if (comboBox_oper.Items[i].ToString() == dataGridView_users.CurrentRow.Cells[1].Value.ToString())
                    comboBox_oper.SelectedIndex = i;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if(dataGridView_limits.CurrentRow!=null)
             db_idp.DeleteQueue(dataGridView_limits.CurrentRow.Cells[0].Value.ToString());
            ReloadTables();
        }

        private void tabPage10_Enter(object sender, EventArgs e)
        {
            //button17_Click(sender,e);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView_userhistory.DataSource = db_rks.Get_UserHistory(textBox_limit.Text);
            dataGridView_userhistory.Columns["Id"].Width = 50;
            dataGridView_userhistory.Columns["act"].Width = 50;
            dataGridView_userhistory.Columns["text"].Width = 600;

            DataGridViewTextBoxColumn idTextColumn = new DataGridViewTextBoxColumn();
            idTextColumn.HeaderText = "TitleID";
            idTextColumn.Resizable = DataGridViewTriState.True;
            idTextColumn.Width = 65;
            idTextColumn.DataPropertyName = "TitleID";
            idTextColumn.Name = "TitleID";
            idTextColumn.ReadOnly = false;
            //if(dataGridView_userhistory.Columns.Contains())
            dataGridView_userhistory.Columns.Add(idTextColumn);

            for (int i = 0; i < dataGridView_userhistory.Rows.Count; i++)
            {
                Int64 x = Convert.ToInt64(dataGridView_userhistory.Rows[i].Cells["ip"].Value.ToString());
                dataGridView_userhistory.Rows[i].Cells["TitleID"].Value = db_idp.LongToIPAddr(x);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            dataGridView_pricelist.DataSource=db_idp.GetPriceLst("","1");
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            db_idp.DeleteUser(dataGridView_users.CurrentRow.Cells[0].Value.ToString());
            ReloadTables();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            db_idp.ChangeUserPassword(dataGridView_users.CurrentRow.Cells[1].Value.ToString(), textBox_pass.Text);
            ReloadTables();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            dataGridView_spisan.DataSource=db_idp.GetSpisaniya();
        }

        private void dataGridView_regions_MouseUp(object sender, MouseEventArgs e)
        {
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

        private void tabPage6_Enter_1(object sender, EventArgs e)
        {
            /*if (first_start)
            {
                first_start = false;
                comboBox_queue1_SelectedIndexChanged(sender, e);
                button_queue_stat_Click(sender, e);
            }*/
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string tovar_id = "";

            for (int i = 0; i < dataGridView_tovars.Rows.Count; i++)
            {
                if (dataGridView_tovars[1, i].Value.ToString() == comboBox_tovars1.SelectedItem.ToString())
                    tovar_id = dataGridView_tovars[0, i].Value.ToString();
            }

            if(comboBox_valute.SelectedIndex == 0)
                db_idp.InsertPriceForTovar(tovar_id, textBox1.Text, textBox2.Text,"1");
            if(comboBox_valute.SelectedIndex == 1)
                db_idp.InsertPriceForTovar(tovar_id, textBox1.Text, textBox2.Text,"2");

            string dt = DateTime.Now.ToString("yyyy-MM-dd");
            /*string rksid = "11";

            for (int i = 0; i < dataGridView_regions.Rows.Count; i++)
            {
                if (dataGridView_regions.Rows[i].Cells["Name"].Value.ToString() == comboBox_queue7.SelectedItem.ToString())
                {
                    rksid = dataGridView_regions.Rows[i].Cells["id"].Value.ToString();
                }
            }*/
            button18_Click(null, null);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView_apprbydate.DataSource=db_idp.GetApprovedByDate("12");
        }

        private void dataGridView_zp1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dataGridView_zp1.CurrentRow != null)
            {
                textBox_user.Text = dataGridView_zp1.CurrentRow.Cells["Name"].Value.ToString();
                textBox_uid.Text = dataGridView_zp1.CurrentRow.Cells["Id"].Value.ToString();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            db_idp.GiveZP(textBox_uid.Text, textBox_summa.Text);
        }

        private void tabPage_prices_Enter(object sender, EventArgs e)
        {
            //ReloadTableTovars();
            //button18_Click(sender,e);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string id=dataGridView_pricelist.CurrentRow.Cells["Id"].Value.ToString();
            db_idp.DeletePriceForTovar(id);
            MessageBox.Show(id);
            button18_Click(null, null);
        }

        private void tabPage_comments_Enter(object sender, EventArgs e)
        {
            //button15_Click(sender, e);
        }

        private void tabPage_ostatki_Enter(object sender, EventArgs e)
        {
            //button12_Click(sender, e);
        }

        private void tabPage_queues_Click(object sender, EventArgs e)
        {

        }    

        private void comboBox_queue1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_queue1.SelectedItem.ToString() == "Все")
                richTextBox_queue_stat.Text = db_idp.GetQueueStatFullText("0");
            else
                richTextBox_queue_stat.Text = db_idp.GetQueueStatFullText(db_idp.GetRegionIdByName(comboBox_queue1.SelectedItem.ToString()));
        }

        private void comboBox_queue3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_rid.Text = db_idp.GetRegionIdByName(comboBox_queue3.SelectedItem.ToString());
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            Int64 x = 1505604313;
            x = Convert.ToInt64(dataGridView_userhistory.CurrentCell.Value.ToString());
            MessageBox.Show(db_idp.LongToIPAddr(x));
        }

        private void button22_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db_idp.getNeobrabotannie(db_idp.GetRegionIdByName(comboBox_queue5.SelectedItem.ToString()));
            label34.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            db_idp.InsertQueue(textBox_queue.Text);
            ReloadRKSComboBox();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            db_idp.ResetTime(dataGridView_users.CurrentRow.Cells[0].Value.ToString());
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (comboBox_queue6.SelectedItem != null)
            {
                if(comboBox_queue6.SelectedItem.ToString()=="Все")
                    dataGridView_sklad.DataSource = db_idp.GetSklad("", HideOpersCheckBox.Checked);
                else
                    dataGridView_sklad.DataSource = db_idp.GetSklad(comboBox_queue6.SelectedItem.ToString(), HideOpersCheckBox.Checked);
               
            }
            
            //dataGridView_ostatki.DataSource = db_idp.GetOstatkiAdmin(db_idp.GetRegionIdByName(comboBox_queue4.SelectedItem.ToString()), checkBox_hide_null.Checked);

            label33.Text = dataGridView_sklad.Rows.Count.ToString();
        }

        private void button26_Click(object sender, EventArgs e)
        {
        }

        private void comboBox_oper_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage_prices_Click(object sender, EventArgs e)
        {

        }


        public string GetTovarId(string s)
        {
            string tovar_id = "";

            for (int i = 0; i < dataGridView_tovars.Rows.Count; i++)
            {
                if (dataGridView_tovars[1, i].Value.ToString() == s)
                    tovar_id = dataGridView_tovars[0, i].Value.ToString();
            }
            return tovar_id;
        }

        public string GetRksId(string s)
        {
            string rksid = "11";

            for (int i = 0; i < dataGridView_regions.Rows.Count; i++)
            {
                if (dataGridView_regions.Rows[i].Cells["Name"].Value.ToString() == s)
                {
                    rksid = dataGridView_regions.Rows[i].Cells["id"].Value.ToString();
                }
            }

            return rksid;
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            if (comboBox_tovars2.SelectedItem == null)
            {
                MessageBox.Show("Не выбран товар");
                return;
            }
            if (comboBox_queue7.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана очередь");
                return;
            }

            string tovar_id = GetTovarId(comboBox_tovars2.SelectedItem.ToString());
            string rksid = GetRksId(comboBox_queue7.SelectedItem.ToString());
            string q = "INSERT into Sklad (`TovarId`, `Income`, `RegionId`, `TovarName`, `OperName`, `Version`) VALUE (" + tovar_id + ", " + textBox3.Text + ", 20, '" + comboBox_tovars2.SelectedItem.ToString() + "', '" + username + "', '" + db_idp.version + "')";
            db_idp.SqlQuery(q, "Добавили на склад");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (comboBox_tovars2.SelectedItem == null)
            {
                MessageBox.Show("Не выбран товар");
                return;
            }
            if (comboBox_queue7.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана очередь");
                return;
            }

            string tovar_id = GetTovarId(comboBox_tovars2.SelectedItem.ToString());
            string rksid = GetRksId(comboBox_queue7.SelectedItem.ToString());
            string q = "INSERT into Sklad (`TovarId`, `Income`, `RegionId`, `TovarName`, `OperName`, `Version`) VALUE (" + tovar_id + ", -" + textBox3.Text + ", 20, '" + comboBox_tovars2.SelectedItem.ToString() + "', '" + username + "', '" + db_idp.version + "')";
            MessageBox.Show(q);
            db_idp.SqlQuery(q, "Удалили со склада");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            dataGridView_pricelist.DataSource = db_idp.GetPriceLst("", "2");
        }

        private void tabPage_goods_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_tovars2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_tovars2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < comboBox_tovars2.Items.Count; i++)
            {
                if (!comboBox_tovars2.Items[i].ToString().Contains(comboBox_tovars2.Text))
                {
                    comboBox_tovars2.Items.Remove(i); 
                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            string s=dataGridView_users.CurrentRow.Cells[0].Value.ToString();
            string st = new_stavka.Text;
            string q = "UPDATE Users SET Stavka=" + st + ", ch0=" + st + ", ch1=" + st + ", ch2=" + st + ", ch3=" + st +  " WHERE id='" + s + "'";
            MessageBox.Show(q);
            db_idp.SqlQuery(q, "Поменяли ставку");
            ReloadTables();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            int pr_all = 0;
            int rs_all = 0;
            for (int i = 0; i < dataGridView_sklad.RowCount-1; i++)
            {
                string pr = dataGridView_sklad.Rows[i].Cells["Приход"].Value.ToString();
                string rs = dataGridView_sklad.Rows[i].Cells["Расход"].Value.ToString();
                pr_all += Convert.ToInt32(pr);
                rs_all += Convert.ToInt32(rs);
            }
            
            textBox8.Text = "Приход:" + pr_all.ToString() + " Расход:" + rs_all.ToString() + " Остаток:" + (pr_all - rs_all).ToString();
        }

        private void dataGridView_tovars_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView_tovars.CurrentCell.Value.ToString();
            string Id = dataGridView_tovars.CurrentRow.Cells["Id"].Value.ToString();

            string column_name = dataGridView_tovars.Columns[dataGridView_tovars.CurrentCell.ColumnIndex].Name.ToString();

            if (column_name == "Price")
            {
                string q = "UPDATE Tovar SET Price='" + s + "' WHERE Id=" + Id;
                db_idp.SqlQuery(q, "Обновили цену"); //Обновили комент
            }

            if (column_name == "Name")
            {
                string q = "UPDATE Tovar SET Name='" + s + "' WHERE Id=" + Id;
                db_idp.SqlQuery(q, "Обновили имя"); //Обновили комент
            }

            if (column_name == "Header")
            {
                string q = "UPDATE Tovar SET Header='" + s + "' WHERE Id=" + Id;
                db_idp.SqlQuery(q, "Обновили название"); //Обновили комент
            }

            if (column_name == "Descr")
            {
                string q = "UPDATE Tovar SET Descr='" + s + "' WHERE Id=" + Id;
                db_idp.SqlQuery(q, "Обновили описание"); //Обновили комент
            }
        }

        private void button32_Click(object sender, EventArgs e)
        { 
            DataTable dt = db_idp.GetOstatkiOper(db_idp.GetRegionIdByName(comboBox_queue4.SelectedItem.ToString()), true);
            if (dt != null)
            {
                dataGridView_ostatki2.DataSource = dt;
                dataGridView_ostatki2.Sort(dataGridView_ostatki2.Columns[1], ListSortDirection.Ascending);

                dataGridView_ostatki2.Columns["ostatok"].Width = 50;
                dataGridView_ostatki2.Columns["tid"].Width = 50;
                dataGridView_ostatki2.Columns["Name"].Width = 280;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            ReloadTables();
        }

        private void comboBox_queue4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_ostatki_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox_hide_null_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button34_Click(object sender, EventArgs e)
        {
        }

        private void button35_Click(object sender, EventArgs e)
        {
            db_idp.ChangeUserPassword(listBox1.SelectedItem.ToString(), textBox9.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox9.Clear();
            string s=listBox1.SelectedItem.ToString();
            s = s.Substring(0, s.Length - 2);

            int sum = 1;
            for (int i = 0; i < s.Length; i++)
            {
                sum *= (int)s[i];
            }

            sum = Math.Abs(sum) / 1000;

            textBox9.Text += sum.ToString();
            textBox10.Text = db_idp.GetMD5ofMD5(textBox9.Text);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox10.Text = db_idp.GetMD5ofMD5(textBox9.Text);
        }
    }
}
