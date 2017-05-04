using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Drawing2D;

namespace adminka
{
    public partial class OperForm : Form
    {
        int seconds=0;

        public OperForm()
        {
            InitializeComponent();
        }

        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();
        DateTime queue_dt;
        System.TimeSpan delta;
        string last_workid;
        bool stop_query=false;

        public void FormatColumns(DataGridView dgv)
        {
            try
            {
                dgv.Columns["num"].Width = 50;
                dgv.Columns["rid"].Width = 30;
                dgv.Columns["sid"].Width = 30;
                dgv.Columns["Fio"].Width = 170;
                dgv.Columns["Adr"].Width = 170;
                dgv.Columns["try_total"].Width = 50;
                dgv.Columns["LastSum"].Width = 50;
            }
            catch
            {

            }
            label_rowcount.Text = dataGridView_orders.RowCount.ToString();
        }

        public void RefreshOstatki()
        {
            DataTable dt = db_idp.GetOstatkiOper(textBox_regionid.Text,true);
            if (dt!=null)
            {
                dataGridView_ostatki.DataSource = dt;
                dataGridView_ostatki.Sort(dataGridView_ostatki.Columns[1], ListSortDirection.Ascending);

                dataGridView_ostatki.Columns["ostatok"].Width = 50;
                dataGridView_ostatki.Columns["tid"].Width = 50;
                dataGridView_ostatki.Columns["Name"].Width = 280;

                comboBox_tov1.Items.Clear();
                comboBox_tov2.Items.Clear();
                comboBox_tov3.Items.Clear();
                comboBox_tov4.Items.Clear();
                comboBox_tov5.Items.Clear();

                for (int i = 0; i < dataGridView_ostatki.RowCount ; i++)
                {
                    if (dataGridView_ostatki[1, i].Value != null)
                    {
                        string s = dataGridView_ostatki[1, i].Value.ToString();
                        comboBox_tov1.Items.Add(s);
                        comboBox_tov2.Items.Add(s);
                        comboBox_tov3.Items.Add(s);
                        comboBox_tov4.Items.Add(s);
                        comboBox_tov5.Items.Add(s);
                    }
                }
            }
        }

        public string PrintDouble(double n)
        {
            return String.Format("{0:0.00}", n);            
        }
        
        public string GetUserInfo(string uid)
        {
            string id = "0";
            string summ = "0";
            string stavka = "0";
            string dost = "0";
            string paid = "0";
            string accr = "0";

            /*
             * 
             DataTable table_uinfo=db_idp.GetUserInfo(uid);
             if (table_uinfo != null)
            {
                dataGridView1.DataSource = table_uinfo;

                if (dataGridView1.CurrentRow != null)
                {
                    id = table_uinfo.Rows[0]["id1"].ToString();
                    summ = table_uinfo.Rows[0]["summ"].ToString();
                    stavka = table_uinfo.Rows[0]["stavka"].ToString();
                    dost = table_uinfo.Rows[0]["dost"].ToString();
                }
            }*/

            stavka = db_idp.SqlQueryWithResult("SELECT Stavka from Users WHERE Id=" + uid);
            dost = db_rks.SqlQueryWithResult("SELECT COUNT(*) from RKS WHERE UserId='" + uid + "' AND StatusName='Доставлен'");
            summ = db_rks.SqlQueryWithResult("SELECT SUM(TotalSumm-740) from RKS WHERE UserId='" + uid + "' AND StatusName='Доставлен'");

            if (summ == "")
                summ = "0";

            double ZP = Convert.ToDouble(summ) / 100 * Convert.ToDouble(stavka);
            double srcheck = Convert.ToDouble(summ) / Convert.ToDouble(dost);
            double ostatok = 0;

            paid = db_idp.GetUserPaid(textBox_uid.Text);
            accr = db_idp.GetUserAccr(textBox_uid.Text);

            if (accr != "" && paid != "")
            {
                    ostatok = Convert.ToDouble(accr) - Convert.ToDouble(paid);
            }
            else
            {
                    accr = "0";
                    paid = "0";
            }


            string s = "ID: " + textBox_uid.Text + "\r\n" +
                             //"Доставлено :" + dost + "\r\n" +
                             "Выручка: " + summ + "\r\n" +
                             "Ставка: " + stavka + "\r\n" +
                             "Ср.чек: " + PrintDouble(srcheck) + "\r\n";

            if (accr != "0")
                s += "ЗП: " + accr + "\r\n";
            else
                s += "ЗП: " + ZP + "\r\n";

            s += "Оплачено: " + paid + "\r\n";

            if (ostatok != 0)
                s += "Остаток: " + ostatok + "\r\n";
            else
                s += "Остаток: " + PrintDouble(ZP - Convert.ToDouble(paid)) + "\r\n"; //ostatok
            
            string user_stat = db_idp.GetUserStat(textBox_uid.Text);
            s += "_____________________\r\n" + user_stat + "\r\n";
            return s;            
        }
        

        public void FillPrices(ComboBox comboBox_tov1, ComboBox comboBox_price1, TextBox tov1_ost)
        {
            if (comboBox_tov1.SelectedIndex != -1)
            {
                for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
                {
                    if (comboBox_tov1.SelectedItem == dataGridView_ostatki[1, i].Value)
                        tov1_ost.Text = dataGridView_ostatki[0, i].Value.ToString();
                }

                DataTable dt;

                string tovar = comboBox_tov1.SelectedItem.ToString();
                tovar = tovar.Replace("'","\\'");

                if (textBox_regionid.Text == "17") // если казахстан цены казахские 
                 dt = db_idp.GetPriceLst(tovar, "2");
                else
                 dt = db_idp.GetPriceLst(tovar, "1");
 
                comboBox_price1.Items.Clear();

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int price = Convert.ToInt32(dt.Rows[i][2].ToString()) * Convert.ToInt32(dt.Rows[i][3].ToString());
                        string s = dt.Rows[i][2].ToString() + "x" + dt.Rows[i][3].ToString() + "=" + price.ToString();

                        if (tovar.Contains("духи"))
                        {
                            if (Convert.ToInt32(dt.Rows[i][3].ToString()) > 2499)
                            {
                                comboBox_price1.Items.Add(s);
                                comboBox_price1.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            comboBox_price1.Items.Add(s);
                            comboBox_price1.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        
        public int SumFromComboBox(ComboBox cb)
        {
            int sum = 0;
            if (cb.SelectedItem != null)
            { 
                string s = cb.SelectedItem.ToString();

                String[] nums = s.Split(new char[] { '=' });
                s = nums[0];

                nums = s.Split(new char[] { 'x' });
                sum = Convert.ToInt32(nums[0]) * Convert.ToInt32(nums[1]);
            }
            return sum;
        }

        private void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            //MessageBox.Show(resolution.Width.ToString());

            if (resolution.Width < 1500) //1920
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            }

            if (Environment.MachineName == db_idp.mycomp)
            {
                ForceAddRawOrders.Visible = true;
            }
            else
            {
                ForceAddRawOrders.Visible = false;
            }

            dvg.SetDvgStyle(tabControl1);
            dateTimePicker_recall.Format= DateTimePickerFormat.Custom;

            if (textBox_regionid.Text == "17")
            {
                textBox_dlv_price.Text = "2600";
            }
            else
            {
                textBox_dlv_price.Text = "840";
                comboBox_oblast.Items.Clear();
            }

            button_GetLastTime_Click(null, null);
            button_show_raw_orders_Click(null, null);
            //button_AddRawOrders_Click(null, null); // 04-01-2017

            RefreshOstatki();
            button1_Click_1(null, null);
            

            this.comboBox_tov1.MouseWheel += this.comboBox1_MouseWheel;
            this.comboBox_tov2.MouseWheel += this.comboBox1_MouseWheel;
            this.comboBox_tov3.MouseWheel += this.comboBox1_MouseWheel;
            this.comboBox_tov4.MouseWheel += this.comboBox1_MouseWheel;
            this.comboBox_tov5.MouseWheel += this.comboBox1_MouseWheel;
        }

        void ShowSelected(DataGridView dgv, int i)
        {
            try
            {
                textBox_index.Text = "";
                comboBox_oblast.Text = "";
                textBox_raion.Text = "";
                textBox_gorod.Text = "";
                textBox_ulica.Text = "";
                textBox_dom.Text = "";
                textBox_kv.Text = "";
                CommentsIM.Text = "";
                ClearTov1_Btn_Click(null, null);
                ClearTov2_Btn_Click(null, null);
                ClearTov3_Btn_Click(null, null);
                ClearTov4_Btn_Click(null, null);
                ClearTov5_Btn_Click(null, null);

                if (dgv.RowCount > 0)
                {
                    dgv.Rows[i].Selected = true;
                    textBox_workid.Text = dgv.Rows[i].Cells["num"].Value.ToString();
                    textBox_status.Text = dgv.Rows[i].Cells["status"].Value.ToString();
                    textBox_fio.Text = dgv.Rows[i].Cells["Fio"].Value.ToString();
                    textBox_phone.Text = dgv.Rows[i].Cells["Tel"].Value.ToString();
                    textBox_address.Text = dgv.Rows[i].Cells["Adr"].Value.ToString();
                    textBox_lastzakaz.Text = dgv.Rows[i].Cells["LastZakaz"].Value.ToString() + " на сумму " + dgv.Rows[i].Cells["LastSum"].Value.ToString();
                    textBox_lkpass.Text = dgv.Rows[i].Cells["Pass"].Value.ToString();

                    if (textBox_status.Text == "Одобрен")
                    {
                        //MessageBox.Show("Переодобряем датой: " + dgv.Rows[i].Cells["UpdTime"].Value.ToString());
                        DateTime dt = DateTime.Parse(dgv.Rows[i].Cells["UpdTime"].Value.ToString());
                        dateTimePicker_delivery.Visible = true;
                        label26.Visible = true;
                        dateTimePicker_delivery.Value = dt;
                    }
                    else
                    {
                        dateTimePicker_delivery.Visible = false;
                        label26.Visible = false;
                        dateTimePicker_delivery.Value = DateTime.Now;
                    }
                    

                    if (show_repeats.Checked)
                    {
                        DataTable repeats = db_rks.GetPovtory(textBox_phone.Text, textBox_fio.Text);
                        dataGridView_povtori.DataSource = repeats;
                    }

                    string q = "SELECT * from RKS WHERE WorkId=" + textBox_workid.Text;
                    dataGridView2.DataSource = db_rks.Get_DataTable(q);
                    if (dataGridView2.RowCount > 0)
                    {
                        CommentsIM.Text = dataGridView2.Rows[0].Cells["CommentsIM"].Value.ToString();
                        textBox_address.Text= dataGridView2.Rows[0].Cells["Adress"].Value.ToString();
                        textBox_fio.Text= dataGridView2.Rows[0].Cells["ClientName"].Value.ToString();
                        textBox_phone.Text= dataGridView2.Rows[0].Cells["Phone"].Value.ToString();
                        GetRegionTimeBtn_Click(null, null);

                        SplitAdrButton_Click(null,null);
                        string tovars=dataGridView2.Rows[0].Cells["PriceName"].Value.ToString();
                        //MessageBox.Show(tovars);

                        string[] separator = { " " };
                        String[] words = tovars.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        int x = 0;
                        string tov="",cena;

                        Dictionary<string, string> list=new Dictionary<string, string>();

                        foreach (string w in words)
                        {
                            if (w[0] == '1' || w[0] == '2')
                            {
                                cena = w;
                                //MessageBox.Show("tov=" + tov + "cena=" + cena);
                                list.Add(tov, cena);
                                tov = "";
                            }
                            else
                            {
                                if(tov=="")
                                    tov = w;
                                else
                                    tov = tov + " " + w;
                            }
                        }
                        
                        comboBox_tov1.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox_price1.DropDownStyle = ComboBoxStyle.DropDown;
                       
                        comboBox_tov1.Text=list.Keys.ElementAt(0);
                        comboBox_price1.Text = list.Values.ElementAt(0);

                        if (list.Count > 1)
                        {
                            comboBox_tov2.DropDownStyle = ComboBoxStyle.DropDown;
                            comboBox_price2.DropDownStyle = ComboBoxStyle.DropDown;
                            comboBox_tov2.Text = list.Keys.ElementAt(1);
                            comboBox_price2.Text = list.Values.ElementAt(1);
                        }
                        UpdateOrderSum();
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show("Редактирование прибывших недоступно" + ex);
            }

            if (textBox_status.Text == "Заказ с сайта")
            {
                CommentsIM.Text = "Заказ с сайта: " + db_idp.UznatTovar(textBox_regionid.Text, textBox_workid.Text);
            }

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
 
        private void OperForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            textBox_userinfo.Text = GetUserInfo(textBox_uid.Text);
        }

        private void button22_Click_2(object sender, EventArgs e)
        {
            RefreshOstatki();
        } 

        private void comboBox_tov1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillPrices(comboBox_tov1, comboBox_price1, tov1_ost);
        }

        private void comboBox_tov2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillPrices(comboBox_tov2, comboBox_price2, tov2_ost);
        }

        private void comboBox_tov3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillPrices(comboBox_tov3, comboBox_price3, tov3_ost);
        }

        private void comboBox_tov4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            FillPrices(comboBox_tov4, comboBox_price4, tov4_ost);
        }

        private void comboBox_tov5_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPrices(comboBox_tov5, comboBox_price5, tov5_ost);
        }
                
        public void UpdateOrderSum()
        {
            int sum = SumFromComboBox(comboBox_price1) +
            SumFromComboBox(comboBox_price2) +
            SumFromComboBox(comboBox_price3) +
            SumFromComboBox(comboBox_price4) +
            SumFromComboBox(comboBox_price5) + Convert.ToInt32(textBox_dlv_price.Text);
            textBox_order_sum.Text =  sum.ToString();
        }

        private void comboBox_price1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSum();
        }

        private void comboBox_price2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSum();
        }

        private void comboBox_price3_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSum();
        }

        private void comboBox_price4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSum();
        }

        private void comboBox_price5_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOrderSum();
        }
        
        public string ClrPrice(string s)
        {
            String[] nums = s.Split(new char[] { '=' });
            s = nums[0];
            return s;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (textBox_status.Text == "Одобрен")
            {
                MessageBox.Show("Заказ одобрен, данный статус установить нельзя.");
                return;
            }

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string q = @"UPDATE `Work` SET StatusId=3, UpdTime = '" + now + "' WHERE Id=" + textBox_workid.Text; //3 = недозвон
            db_idp.SqlQuery(q, ""); //Заказ недозвон
            //db_idp.InsertToHistory("3", textBox_workid.Text, CommentsIM.Text); // status 3 = недозвон
            button1_Click_1(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            db_idp.CallPhone(textBox_phone.Text);
        }
 
        public void HideTel(DataGridView dgv)
        {
            try
            {
                dgv.Columns["Tel"].Width = 0;
                dgv.Columns["Tel"].Visible = false;
                //dgv.Columns["Phone"].Visible = false;
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Перезвон", "");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Недозвон","");
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Отказ","");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Доставлен","");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Одобрен","");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "", "");
        }


        // перекодирование текста смс в UCS2 
        public static string StringToUCS2(string str)
        {
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] ucs2 = ue.GetBytes(str);

            int i = 0;
            while (i < ucs2.Length)
            {
                byte b = ucs2[i + 1];
                ucs2[i + 1] = ucs2[i];
                ucs2[i] = b;
                i += 2;
            }
            return BitConverter.ToString(ucs2).Replace("-", "");
        }

        private void send_sms_btn_Click(object sender, EventArgs e)
        {
            db_rks.SendSMS(db_idp.sms_sender, textBox_phone.Text, textBox_sms.Text);
            textBox_sms.Text = textBox_sms.Text.Replace("'", "\\'");
            db_idp.InsertToHistory("12",textBox_workid.Text, textBox_sms.Text); // status 12=sms
        }

        private void sms1_btn_Click(object sender, EventArgs e)
        {
            textBox_sms.Text = db_idp.sitename + " Ваш ключ пароль " + textBox_lkpass.Text;
        }

        private void sms2_btn_Click(object sender, EventArgs e)
        {
            string dt = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd");
            textBox_sms.Text = "Здравствуйте, активируйте свои бонусные баллы 1904 рублей до " + dt + ". Cайт " + db_idp.sitename + " Ваш ключ пароль " + textBox_lkpass.Text;
        }

        private void sms3_btn_Click(object sender, EventArgs e)
        {
            textBox_sms.Text = textBox_fio.Text + " ваши бонусы активированы на " + GetTovarList() + ". К оплате сумма :" + textBox_order_sum.Text;
        }

        private void sms4_btn_Click(object sender, EventArgs e)
        {
            textBox_sms.Text = "Ваш заказ на: " + GetTovarList() + " оформлен, сумма доплаты:" + textBox_order_sum.Text + " руб";
        }

        private void sms5_btn_Click(object sender, EventArgs e)
        {
            textBox_sms.Text = "Ваш подарочный сертификат на сумму 3000 рублей активирован на: " + GetTovarList() + ". к доплате :" + textBox_order_sum.Text;
        }

        string GetTovarList()
        {
            string tovars = "";
            if (comboBox_tov1.SelectedIndex != -1)
                tovars = comboBox_tov1.SelectedItem.ToString();
            if (comboBox_tov2.SelectedIndex != -1)
                tovars += ", " + comboBox_tov2.SelectedItem.ToString();
            if (comboBox_tov3.SelectedIndex != -1)
                tovars += ", " + comboBox_tov3.SelectedItem.ToString();
            if (comboBox_tov4.SelectedIndex != -1)
                tovars += ", " + comboBox_tov4.SelectedItem.ToString();
            if (comboBox_tov5.SelectedIndex != -1)
                tovars += ", " + comboBox_tov5.SelectedItem.ToString();
            return tovars;
        }
        
        public void SelectTovarInCombo(ComboBox comboBox_tov1)
        {
            for (int i = 0; i < comboBox_tov1.Items.Count; i++)
            {
                if (comboBox_tov1.Items[i].ToString() == dataGridView_ostatki.CurrentRow.Cells["Name"].Value.ToString())
                {
                    //if (comboBox_tov1.SelectedIndex == -1)
                    comboBox_tov1.SelectedIndex = i;
                }
            }
        }

        private void dataGridView_ostatki_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBox_tov1.SelectedIndex == -1)
            {
                SelectTovarInCombo(comboBox_tov1);
                return;
            }

            if (comboBox_tov2.SelectedIndex == -1)
            {
                SelectTovarInCombo(comboBox_tov2);
                return;
            }

            if (comboBox_tov3.SelectedIndex == -1)
            {
                SelectTovarInCombo(comboBox_tov3);
                return;
            }

            if (comboBox_tov4.SelectedIndex == -1)
            {
                SelectTovarInCombo(comboBox_tov4);
                return;
            }
 
                SelectTovarInCombo(comboBox_tov5);
         }

        
        private void dataGridView_zakazi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView_orders.CurrentRow != null)
            {
                ShowSelected(dataGridView_orders, dataGridView_orders.CurrentRow.Index);
                tabControl1.SelectedIndex--;
            }
        }

        private void button_userinfo_Click(object sender, EventArgs e)
        {
            textBox_userinfo.Text = GetUserInfo(textBox_uid.Text);
            //string uinfo = await Task.Factory.StartNew<string>(() => GetUserInfo(textBox_uid.Text), TaskCreationOptions.LongRunning);
            //textBox_userinfo.Text = uinfo;
        }       

        private async void GetRegionTimeBtn_Click(object sender, EventArgs e)
        {
            textBox_phone.Text = textBox_phone.Text.Replace("-", "");

            if (textBox_phone.Text.Length > 3)
            {
                string region = await Task.Factory.StartNew<string>(() => db_idp.GetRegionFromPhone(textBox_phone.Text), TaskCreationOptions.LongRunning);
                comboBox_oblast.Text = region;

                string time = await Task.Factory.StartNew<string>(() => db_idp.GetTimeByRegion(region), TaskCreationOptions.LongRunning);
                textBox_time.Text = time;
            }
        }

        private void ClearTov1_Btn_Click(object sender, EventArgs e)
        {
            comboBox_tov1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_price1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tov1.SelectedIndex = -1;
            comboBox_price1.SelectedIndex = -1;
            comboBox_price1.Items.Clear();
            tov1_ost.Text = "";
        }

        private void ClearTov2_Btn_Click(object sender, EventArgs e)
        {
            comboBox_tov2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_price2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tov2.SelectedIndex = -1;
            comboBox_price2.SelectedIndex = -1;
            comboBox_price2.Items.Clear();
            tov2_ost.Text = "";
        }

        private void ClearTov3_Btn_Click(object sender, EventArgs e)
        {
            comboBox_tov3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_price3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tov3.SelectedIndex = -1;
            comboBox_price3.SelectedIndex = -1;
            comboBox_price3.Items.Clear();
            tov3_ost.Text = "";
        }

        private void ClearTov4_Btn_Click(object sender, EventArgs e)
        {
            comboBox_tov4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_price4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tov4.SelectedIndex = -1;
            comboBox_price4.SelectedIndex = -1;
            comboBox_price4.Items.Clear();
            tov4_ost.Text = "";
        }

        private void ClearTov5_Btn_Click(object sender, EventArgs e)
        {
            comboBox_tov5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_price5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tov5.SelectedIndex = -1;
            comboBox_price5.SelectedIndex = -1;
            comboBox_price5.Items.Clear();
            tov5_ost.Text = "";
        }
                
        private void dataGridView_current_order_DataSourceChanged(object sender, EventArgs e)
        {
            FormatColumns(dataGridView_current_order);
            HideTel(dataGridView_current_order);
        }

        private void dataGridView_orders_DataSourceChanged(object sender, EventArgs e)
        {
            FormatColumns(dataGridView_orders);
            HideTel(dataGridView_orders);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd");
            dataGridView_orders.DataSource = db_rks.Get_Arrived(textBox_username.Text, "2016-01-14", now, textBox_regionid.Text,"");
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            delta = queue_dt.Subtract(DateTime.Now);
            textBox3.Text = delta.Hours.ToString() + ":" + delta.Minutes.ToString() + ":" + delta.Seconds.ToString();

           /* if (delta.Hours < 0)
            {
                MessageBox.Show("1111111");
            }*/
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd");
            dataGridView_orders.DataSource = db_rks.Get_Vozvrat(textBox_username.Text, "2016-01-14", now, textBox_regionid.Text, "");
            /*string s=dataGridView_orders.Rows[dataGridView_orders.CurrentRow.Index].Cells["num"].Value.ToString();
            string q="DELETE from Work WHERE Id=" + s;
            db_idp.SqlQuery(q, "");
            MessageBox.Show(s + " удален");*/
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_find_Click(null, null);
            }
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            dataGridView_orders.MultiSelect = false;
            for (int i = 0; i < dataGridView_orders.Rows.Count; i++)
            {
                if (dataGridView_orders.Rows[i].Cells["num"].Value.ToString() == textBox1.Text)
                {
                    dataGridView_orders.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_orders.Rows[i].Selected = true;
                    dataGridView_orders.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                if (dataGridView_orders.Rows[i].Cells["Tel"].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    dataGridView_orders.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_orders.Rows[i].Selected = true;
                    dataGridView_orders.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                if (dataGridView_orders.Rows[i].Cells["Fio"].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    dataGridView_orders.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_orders.Rows[i].Selected = true;
                    dataGridView_orders.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                if (dataGridView_orders.Rows[i].Cells["Adr"].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    dataGridView_orders.FirstDisplayedScrollingRowIndex = i;
                    dataGridView_orders.Rows[i].Selected = true;
                    dataGridView_orders.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            last_workid = textBox_workid.Text;
            dateTimePicker_recall.Value = DateTime.Now;
            //dataGridView_ostatki.Enabled = false;

            button1.Enabled = false;
            button_GoBack.Enabled = false;
            button_ApproveOrder.Enabled = false;
            button_RefuseOrder.Enabled = false;
            button8.Enabled = false;
            button_RecallOrder.Enabled = false;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            DataTable curr_order;
            curr_order = await Task.Factory.StartNew<DataTable>(() => db_idp.GetZakazSite(textBox_uid.Text), TaskCreationOptions.LongRunning);

            if (curr_order != null)
            {
                dataGridView_current_order.DataSource = curr_order;
               // CommentsIM.Text = "Заказ с сайта: " + db_idp.UznatTovar(textBox_regionid.Text, textBox_workid.Text);
            }

            if (curr_order != null)
            {
                if (curr_order.Rows.Count == 0)
                {
                    curr_order = await Task.Factory.StartNew<DataTable>(() => db_idp.GetPerezvon(textBox_uid.Text), TaskCreationOptions.LongRunning);
                }
            }

            if (curr_order != null)
            {
                if (curr_order.Rows.Count == 0)
                {
                    curr_order = await Task.Factory.StartNew<DataTable>(() => db_idp.GetCurrentOrder(textBox_uid.Text,1,textBox_regionid.Text), TaskCreationOptions.LongRunning);
                }
                dataGridView_current_order.DataSource = curr_order;
            }


            if (dataGridView_current_order.RowCount > 0)
            {
                dataGridView_current_order.Rows[0].Selected = true; //dataGridView_zakazi.SelectedCells[0].RowIndex+1
                ShowSelected(dataGridView_current_order, dataGridView_current_order.SelectedCells[0].RowIndex);
            }
            
            if (stop_query)
                goto End;

            textBox_phone.Text = textBox_phone.Text.Replace("-", "");

            string region = await Task.Factory.StartNew<string>(() => db_idp.GetRegionFromPhone(textBox_phone.Text), TaskCreationOptions.LongRunning);
            comboBox_oblast.Text = region;

            if (region == "empty_phone")
            {
                ForceAddRawOrders_Click(null, null);
                button1_Click_1(null, null);
            }

            if (stop_query)
                goto End;

            string time = await Task.Factory.StartNew<string>(() => db_idp.GetTimeByRegion(region), TaskCreationOptions.LongRunning);
            textBox_time.Text = time;
            textBox_sms.Text = db_idp.sitename + " Ваш ключ пароль " + textBox_lkpass.Text;

            DataTable history = await Task.Factory.StartNew<DataTable>(() => db_idp.GetHistory(textBox_workid.Text), TaskCreationOptions.LongRunning);
            dataGridView_history.DataSource = history;

            if (stop_query)
                goto End;

            if (show_repeats.Checked)
            {
                DataTable repeats = await Task.Factory.StartNew<DataTable>(() => db_rks.GetPovtory(textBox_phone.Text, textBox_fio.Text), TaskCreationOptions.LongRunning);
                dataGridView_povtori.DataSource = repeats;
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}.{1:00}", ts.Seconds, ts.Milliseconds / 10);
            Text = db_idp.version + " ::: Operator: " + textBox_username.Text + " (request_time: " + elapsedTime + ")";

            End:
            button_ApproveOrder.Enabled = true;
            button_RefuseOrder.Enabled = true;
            button8.Enabled = true;
            button_RecallOrder.Enabled = true;
            button1.Enabled = true;
            button_GoBack.Enabled = true;
            //dataGridView_ostatki.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Возврат к заказу: " + last_workid);
            dataGridView_current_order.DataSource = db_idp.GetOrderByWorkId(last_workid);

            if (dataGridView_current_order.RowCount > 0)
            {
                dataGridView_current_order.Rows[0].Selected = true; //dataGridView_zakazi.SelectedCells[0].RowIndex+1
                ShowSelected(dataGridView_current_order, dataGridView_current_order.SelectedCells[0].RowIndex);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button_show_raw_orders_Click(object sender, EventArgs e)
        {
            dataGridView_orders.DataSource = db_idp.GetZakazi(textBox_uid.Text, "Не обработан", "");
            HideTel(dataGridView_orders);
        }

        private void button_GetLastTime_Click(object sender, EventArgs e)
        {
            textBox2.Text = db_idp.GetLastTime2(textBox_uid.Text, textBox_regionid.Text);
            try
            {
                queue_dt = Convert.ToDateTime(textBox2.Text).AddHours(14);
            }
            catch
            {
                //MessageBox.Show("Неизвестно время задания очереди");
            }
        }

        private void show_repeats_CheckedChanged(object sender, EventArgs e)
        {

        }

        bool IsTovarNotExists(string s)
        {
            for (int i = 0; i < dataGridView_ostatki.RowCount; i++)
            {
                if (s == dataGridView_ostatki.Rows[i].Cells["Name"].Value.ToString())
                    return false;
            }
            return true;
        }

        private void button_ApproveOrder_Click(object sender, EventArgs e)
        {
            if (textBox_order_sum.Text == "")
            {
                MessageBox.Show("Заполните заказ, потом одобряйте");
                return;
            }

            int Total = Convert.ToInt32(textBox_order_sum.Text);
            int try_total = Convert.ToInt32(textBox_order_sum.Text) - Convert.ToInt32(textBox_dlv_price.Text);

            if (try_total < 2500)
            {
                MessageBox.Show("Нельзя одобрить заказ на сумму меньше 2500");
                return;
            }

            string PriceName = "";

            if (comboBox_tov1.SelectedIndex != -1)
            {
                PriceName += comboBox_tov1.SelectedItem.ToString() + " " + ClrPrice(comboBox_price1.SelectedItem.ToString()) + " ";

                if (IsTovarNotExists(comboBox_tov1.SelectedItem.ToString()) == true) // перепроверяем на месте ли товар
                {
                    MessageBox.Show(comboBox_tov1.SelectedItem.ToString() + " - товар не существует. Одобрить не удалось!");
                    return;
                }
            }

            if (comboBox_tov2.SelectedIndex != -1)
            {
                PriceName += comboBox_tov2.SelectedItem.ToString() + " " + ClrPrice(comboBox_price2.SelectedItem.ToString()) + ", ";

                if (IsTovarNotExists(comboBox_tov2.SelectedItem.ToString()) == true) // перепроверяем на месте ли товар
                {
                    MessageBox.Show(comboBox_tov2.SelectedItem.ToString() + " - товар не существует. Одобрить не удалось!");
                    return;
                }
            }

            if (comboBox_tov3.SelectedIndex != -1)
                PriceName += comboBox_tov3.SelectedItem.ToString() + " " + ClrPrice(comboBox_price3.SelectedItem.ToString()) + ", ";
            if (comboBox_tov4.SelectedIndex != -1)
                PriceName += comboBox_tov4.SelectedItem.ToString() + " " + ClrPrice(comboBox_price4.SelectedItem.ToString()) + ", ";
            if (comboBox_tov5.SelectedIndex != -1)
                PriceName += comboBox_tov5.SelectedItem.ToString() + " " + ClrPrice(comboBox_price5.SelectedItem.ToString()) + ", ";

            if (PriceName.Length < 5)
            {
                MessageBox.Show("Вы пытаетесь одобрить заказ без товара");
                return;
            }

            string result = db_rks.SqlQueryWithResult("SELECT Id from RKS WHERE WorkId = " + textBox_workid.Text);

            if (result != "0")
            {
                db_rks.SqlQueryWithResult("DELETE from RKS WHERE WorkId = " + textBox_workid.Text);
            }

            result = db_rks.SqlQueryWithResult("SELECT Id from RKS WHERE WorkId = " + textBox_workid.Text);

            if ( result == "0")
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string q = @"UPDATE `Work` SET StatusId=6, Total=" + Total + ", try_total=" + try_total + ", UpdTime='" + now + "' WHERE Id='" + textBox_workid.Text + "'"; //6=одобрен
                int summ_bez_dost = Convert.ToInt32(textBox_order_sum.Text) - Convert.ToInt32(textBox_dlv_price.Text);

                PriceName = PriceName.Replace("'", "\\'");
                db_idp.SqlQuery(q, "Заказ одобрен. Сумма без доставки: " + summ_bez_dost.ToString());

                q = @"INSERT `RKS` SET UserId='" + textBox_uid.Text + "', " +
                                   "RegionId='" + textBox_regionid.Text + "', " +
                                   "RksId='" + textBox_rksid.Text + "', " +
                                   "UserName='" + textBox_username.Text + "', " +
                                   "WorkId='" + textBox_workid.Text + "', " +
                                   "ClientName='" + textBox_fio.Text + "', " +
                                   "Phone='" + textBox_phone.Text + "', " +
                                   "Adress='" + textBox_address.Text + "', " +
                                   "StatusId='6', StatusName='Одобрен', ";

                q = q + "PriceName='" + PriceName + "', ";
                                
                string dt = dateTimePicker_delivery.Value.Year + "-" + dateTimePicker_delivery.Value.Month + "-" + dateTimePicker_delivery.Value.Day;

                //DateTime newdt = DateTime.Now;
                //string dt = newdt.Year + "-" + newdt.Month + "-" + newdt.Day;

                int goodsprice = 0;
                string tid1="0", tid2="0", tid3="0", tid4="0", tid5 = "0";

                if (comboBox_tov1.SelectedIndex != -1)
                {
                    goodsprice += Convert.ToInt32(db_idp.GetGoodsPrice(comboBox_tov1.SelectedItem.ToString()));
                    tid1 = db_idp.GetTovarId(comboBox_tov1.SelectedItem.ToString());
                }
                if (comboBox_tov2.SelectedIndex != -1)
                {
                    goodsprice += Convert.ToInt32(db_idp.GetGoodsPrice(comboBox_tov2.SelectedItem.ToString()));
                    tid2 = db_idp.GetTovarId(comboBox_tov2.SelectedItem.ToString());
                }
                if (comboBox_tov3.SelectedIndex != -1)
                {
                    goodsprice += Convert.ToInt32(db_idp.GetGoodsPrice(comboBox_tov3.SelectedItem.ToString()));
                    tid3 = db_idp.GetTovarId(comboBox_tov3.SelectedItem.ToString());
                }
                if (comboBox_tov4.SelectedIndex != -1)
                {
                    goodsprice += Convert.ToInt32(db_idp.GetGoodsPrice(comboBox_tov4.SelectedItem.ToString()));
                    tid4 = db_idp.GetTovarId(comboBox_tov4.SelectedItem.ToString());
                }
                if (comboBox_tov5.SelectedIndex != -1)
                {
                    goodsprice += Convert.ToInt32(db_idp.GetGoodsPrice(comboBox_tov5.SelectedItem.ToString()));
                    tid5 = db_idp.GetTovarId(comboBox_tov5.SelectedItem.ToString());
                }
                
                q = q + "TotalSumm='" + textBox_order_sum.Text + "', " +
                        "CommentsIM='" + CommentsIM.Text + "', " +
                        "DateDelivery='" + dt + "' ," +
                        "GoodsPrice='" + goodsprice + "', " +
                        "TID1='" + tid1 + "', TID2='" + tid2 + "', TID3='" + tid3 + "', TID4='" + tid4 + "', TID5='" + tid5 + "'";

                sqltext.Text = q;
                db_rks.SqlQuery(q, "Заказ добавлен в RKS");

                if (comboBox_tov1.SelectedIndex != -1)
                    db_idp.SpisatTovarOper(textBox_username.Text, tid1);
                if (comboBox_tov2.SelectedIndex != -1)
                    db_idp.SpisatTovarOper(textBox_username.Text, tid2);
                if (comboBox_tov3.SelectedIndex != -1)
                    db_idp.SpisatTovarOper(textBox_username.Text, tid3);
                if (comboBox_tov4.SelectedIndex != -1)
                    db_idp.SpisatTovarOper(textBox_username.Text, tid4);
                if (comboBox_tov5.SelectedIndex != -1)
                    db_idp.SpisatTovarOper(textBox_username.Text, tid5);

                sms4_btn_Click(sender, e);

                if (checkBox_sendsms.Checked)
                {
                    send_sms_btn_Click(sender, e);
                }

                RefreshOstatki();
                button1_Click_1(sender, e);
            }
            else
            {
                MessageBox.Show("Заказ уже есть в РКС");
            }

        }

        private void button_RefuseOrder_Click(object sender, EventArgs e)
        {
            if (textBox_status.Text == "Одобрен" || textBox_status.Text == "Доставлен" || textBox_status.Text == "Прибыл")
            {
                MessageBox.Show("Заказ одобрен, данный статус установить нельзя.");
                return;
            }

            string q1 = @"UPDATE `Work` SET StatusId=5 WHERE Id='" + textBox_workid.Text + "'"; //5 = отказ
            db_idp.SqlQuery(q1, ""); //Заказ отказ

            string q2 = "DELETE from `RKS` WHERE WorkId = '" + textBox_workid.Text + "'";
            db_rks.SqlQuery(q2, ""); //Заказ удален из РКС

            if (textBox_status.Text != "Заказ с сайта")
            {
                //if (comboBox_tov1.Text != "" || comboBox_tov1.SelectedIndex != -1)
                //    db_idp.VozvratNaSklad(textBox_regionid.Text, textBox_workid.Text);
            }
            else
            {

            }
            
            button1_Click_1(sender, e);
        }

        private void button_RecallOrder_Click(object sender, EventArgs e)
        {
            if (textBox_status.Text == "Одобрен")
            {
                MessageBox.Show("Заказ одобрен, данный статус установить нельзя.");
                return;
            }

            string PerezvonData = dateTimePicker_recall.Value.ToString("yyyy-MM-dd HH:mm");
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            string q1 = @"UPDATE `Work` SET StatusId=4, PerezvonData='" + PerezvonData + "', UpdTime='" + now + "' WHERE Id='" + textBox_workid.Text + "'"; //4 = перезвон
            db_idp.SqlQuery(q1, "Заказ перезвон");
            db_idp.InsertToHistory("4", textBox_workid.Text, CommentsIM.Text); // status 4 = перезвон
            button1_Click_1(sender, e);
        }

        private void button_GoBack_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Возврат к заказу: " + last_workid);
            dataGridView_current_order.DataSource = db_idp.GetOrderByWorkId(last_workid);

            if (dataGridView_current_order.RowCount > 0)
            {
                dataGridView_current_order.Rows[0].Selected = true; //dataGridView_zakazi.SelectedCells[0].RowIndex+1
                ShowSelected(dataGridView_current_order, dataGridView_current_order.SelectedCells[0].RowIndex);
            }
        }

        private void button_AddRawOrders_Click(object sender, EventArgs e)
        {
            if (delta.Hours < 0)
            {
                db_idp.GetRawOrders(textBox_uid.Text, textBox_regionid.Text);
                button_show_raw_orders_Click(null, null);
                button_GetLastTime_Click(null, null);
            }
            else
            {
                MessageBox.Show("Рановато");
            }
        }

        private void dataGridView_ostatki_MouseMove(object sender, MouseEventArgs e)
        {
            dataGridView_ostatki.Focus();
            //MessageBox.Show("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = db_idp.GetCurrentOrder(textBox_uid.Text,0, textBox_regionid.Text);
            label_utro_count.Text = dataGridView2.RowCount.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            seconds++;
            /*if (seconds%100==0)
            {
                string server_version = db_idp.GetServerVersion();

                if (db_idp.version != server_version)
                {
                    DialogResult dialogResult = MessageBox.Show("Вышло обновление админки", "Обновить сейчас ?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("Текущая версия:" + db_idp.version + "\r\nВерсия на сервере:" + server_version + "\r\nЗапускаем обновление");
                        textBox3.Text = "Текущая версия:" + db_idp.version + "\r\nВерсия на сервере:" + server_version + "\r\nЗапускаем обновление";
                        db_idp.UpdateExe();
                    }
                }
            }*/

         /*   if (seconds%20==0)
            {
                Thread t = new Thread(delegate () { screens.SendScreen(textBox_username.Text); });
                t.Start();
            }*/
        }

        private void ForceAddRawOrders_Click(object sender, EventArgs e)
        {
            db_idp.GetRawOrders(textBox_uid.Text, textBox_regionid.Text);
            button_show_raw_orders_Click(null, null);
            button_GetLastTime_Click(null, null);
        }

        private void SplitAdrButton_Click(object sender, EventArgs e)
        {
            try
            {
                String[] words = textBox_address.Text.Split(',');
                //MessageBox.Show(words.Count().ToString());
                textBox_index.Text = words[0];
                comboBox_oblast.Text = words[1];
                textBox_raion.Text = words[2];
                textBox_gorod.Text = words[3];
                textBox_ulica.Text = words[4];
                textBox_dom.Text = words[5];
                textBox_kv.Text = words[6];
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CommentsIM.Text="Заказ с сайта: " + db_idp.UznatTovar(textBox_regionid.Text, textBox_workid.Text);
        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_orders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_ostatki_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);


        public void ShowHint(IntPtr handle, string help)
        {
            Bitmap b = new Bitmap(300, 350);

            try
            {
                using (var gr = Graphics.FromImage(b))
                {
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush WhiteBrush = new SolidBrush(Color.White);
                    SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 128));
                    Pen pen = new Pen(Brushes.Black, 5);
                    pen.LineJoin = LineJoin.Bevel;   
                    int dx = 10;
                    int dy = 12;
                    int width = 135;                   
                    gr.FillRectangle(blackBrush, 10, 30, 180, 50);                  
                    var t2 = Graphics.FromHwnd(handle);
                    t2.DrawImage(b, 0, 0);
                }
            }
            catch
            {

            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            IntPtr HWND1 = FindWindow(null, "EyeBeam");
            IntPtr HWND2 = FindWindow("Funky Window", null);
            ShowWindow(HWND1, 0);

            if(HWND1!=IntPtr.Zero)
            ShowHint(HWND2, "111");
            //ShowWindow(HWND2, 0);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            db_idp.HangUp();
        }

        private void dataGridView_current_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DataSourceChanged(object sender, EventArgs e)
        {
            HideTel(dataGridView2);
        }
    }
}

