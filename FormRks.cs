using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;

namespace adminka
{
    public partial class RksForm : Form
    {
        string RegionId = "20";
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();
        bool show_phones = false;

        public string rksname;

        int ipos = -1;
        int end_pos = 0;

        string last_button = "1";
        int vhojdenienum = 0;

        public RksForm()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
            dvg.SetDvgStyle(tabControl1);
        }

        public void ChangeFormText(string s)
        {
            this.Text = "FormRKS " + db_idp.version + " " + rksname + " " + s;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowTemplate.MinimumHeight = 80;
            dateTimePicker2.Value = new DateTime(2016, 1, 1);
            //GetPasportData();

            comboBox1.SelectedIndex = 1;
            comboBox_stat.SelectedIndex = 0;
            comboBox_status.SelectedIndex = 0;
            comboBox_shab_word.SelectedIndex = 0;
            comboBox_shab_excel.SelectedIndex = 0;

            if (rksname.ToLower().IndexOf("rks") > 0 || rksname.ToLower() == "denis")
                comboBox1.SelectedIndex = 4;

            if (rksname.ToLower() == "olyarks" || rksname.ToLower() == "sonyarks" || rksname.ToLower() == "live")
                show_phones = true;

            if (rksname == "live")
            {
                /*comboBox_stat.SelectedIndex = 8; // прибыл новый акк лайва
                textBox_filter.Text = "U2";
                FilterTextBtn_Click(null, null);
                GetStatus_LiveInform2_Click(null, null);
                dateTimePicker2.Value = new DateTime(2017, 4, 1); // 4 апреля

                comboBox_stat.SelectedIndex = 5; // отправлен новый акк лайва
                FilterTextBtn_Click(null, null);
                GetStatus_LiveInform2_Click(null, null);*/

                textBox_filter.Text = "U2";
                comboBox_stat.SelectedIndex = 5; // отправлен
                FilterTextBtn_Click(null, null);
                GetStatus_LiveInform1_Click(null, null);

                comboBox_stat.SelectedIndex = 8; // прибыл 
                FilterTextBtn_Click(null, null);
                //dateTimePicker2.Value = new DateTime(2016, 4, 1); // 4 апреля 2016 года
                GetStatus_LiveInform1_Click(null, null);
            }

            if (Environment.MachineName == db_idp.mycomp)
            {
                //Mass_Status.Visible = true;
                GetStatus_LiveInform1.Visible = true;
            }
            else
            {
                Mass_Status.Visible = true;
                GetStatus_LiveInform1.Visible = false;
            }

            ChangeFormText("");
        }

        void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to release the object " + ex);
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        void MakeWordFile(string shab_fname)
        {
            Word._Application oWord = null;
            try
            {
                oWord = new Word.Application();
            }
            catch
            {
                MessageBox.Show("Не удается подключиться к Ворду");
                return;
            }

            string dir = comboBox1.SelectedItem.ToString() + "_" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            DirectoryInfo directoryinfo = Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + dir);

            Word._Document oDoc = null;
            try
            {
                oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\" + shab_fname);
            }
            catch
            {
                MessageBox.Show("Разрешите редактирование бланка");
            }

            string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
            string fname = Environment.CurrentDirectory + "\\" + dir + "\\" + WorkId + "_112ЭК.doc";

            try
            {
                SetTemplate(oDoc);
            }
            catch
            {
                MessageBox.Show("SetTemplate err. WorkId=" + WorkId);
                return;
            }

            try
            {
                oDoc.SaveAs(FileName: fname);
            }
            catch (Exception ex)
            {
                richTextBox2.Text += "Save " + fname + " error" + ex.Message + Environment.NewLine;

                if (ex.Message.Contains("RPC_E_CALL_REJECTED"))
                {
                    Thread.Sleep(500);
                    richTextBox2.Text += fname + "пробуем по новой" + Environment.NewLine;

                    oDoc.SaveAs(FileName: fname);
                }
            }

            if (checkBox1.Checked)
                oDoc.PrintOut();

            oDoc.Close();
            oWord.Quit();

            //ReleaseObject(oWord);

            if (checkBox2.Checked)
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = fname;
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
        }

        private bool SetTemplate(Word._Document oDoc)
        {
            PriceInWords PriceInWords = new PriceInWords();

            try
            {
                oDoc.Bookmarks["Summa"].Range.Text = textBox_summa.Text;

                if (comboBox1.SelectedItem.ToString() == "Казахстан")
                    oDoc.Bookmarks["SummaPr"].Range.Text = PriceInWords.SummaPropis(textBox_summa.Text, "тенге");
                else
                    oDoc.Bookmarks["SummaPr"].Range.Text = PriceInWords.SummaPropis(textBox_summa.Text, "рублей 00 копеек");

                oDoc.Bookmarks["Komu"].Range.Text = textBox_komu.Text;
                oDoc.Bookmarks["Kuda"].Range.Text = textBox_kuda.Text;
                oDoc.Bookmarks["Otkogo"].Range.Text = textBox_otkogo.Text;
            }
            catch (Exception ex)
            {
                richTextBox2.Text += "ошибка1 " + ex.Message + Environment.NewLine;
            }

            string s = textBox_adrotravitelya.Text;
            String[] words = s.Split(new char[] { ',' });

            // MessageBox.Show(words.Length.ToString());

            string Oblast = words[1];
            Oblast = Oblast.Replace("Ханты-Мансийский Автономный округ", "ХМАО");
            Oblast = Oblast.Replace("Ханты - Мансийский Автономный округ", "ХМАО");

            if (!Oblast.Trim().ToLower().Contains(" "))
                Oblast = Oblast + " обл ";

            string Raion = words[2];
            string NasPunkt = words[3];
            string Ulica = words[4];

            try
            {
                if (!words[4].Contains("ул"))
                    words[4] = "ул. " + words[4] + ", ";

                if (words.Length >= 7)
                    oDoc.Bookmarks["Gorod"].Range.Text = words[4] + " д. " + words[5] + ", " + " кв. " + words[6];
                else
                    oDoc.Bookmarks["Gorod"].Range.Text = words[4] + " д. " + words[5];

                if (Raion.Length > 0)
                    oDoc.Bookmarks["Ulica"].Range.Text = Oblast + ", " + Raion;
                else
                    oDoc.Bookmarks["Ulica"].Range.Text = Oblast;


                if (!NasPunkt.Contains("г ") && !NasPunkt.Contains("с ") && !NasPunkt.Contains("пгт ") && !NasPunkt.Contains("п ") && !NasPunkt.Contains("рп ") && !NasPunkt.Contains("д "))
                    NasPunkt = ", г. " + NasPunkt;
                else
                    NasPunkt = ", " + NasPunkt;
            }
            catch (Exception ex)
            {
                richTextBox2.Text += "ошибка2 " + ex.Message + Environment.NewLine;
            }

            try
            {
                oDoc.Bookmarks["Kvart"].Range.Text = NasPunkt;

                string ZIP = words[0].Trim();
                oDoc.Bookmarks["I1"].Range.Text = ZIP[0].ToString();
                oDoc.Bookmarks["I2"].Range.Text = ZIP[1].ToString();
                oDoc.Bookmarks["I3"].Range.Text = ZIP[2].ToString();
                oDoc.Bookmarks["I4"].Range.Text = ZIP[3].ToString();
                oDoc.Bookmarks["I5"].Range.Text = ZIP[4].ToString();
                oDoc.Bookmarks["I6"].Range.Text = ZIP[5].ToString();
            }
            catch (Exception ex)
            {
                richTextBox2.Text += "ошибка3 " + ex.Message + Environment.NewLine;
                return false;
            }
            return true;
        }


        void MakeExcelFile(string shab_fname)
        {
            Excel._Application oExcel = new Excel.Application();

            string dir = comboBox1.SelectedItem.ToString() + "_" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            DirectoryInfo directoryinfo = Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + dir);

            string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();

            string fname = Environment.CurrentDirectory + "\\" + dir + "\\" + WorkId + "_" + shab_fname;

            Excel::Application excelAplication = new Excel.Application();
            excelAplication.Visible = false;
            Excel::Workbook excelBook = excelAplication.Workbooks.Open(Environment.CurrentDirectory + "\\" + shab_fname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // сохранение шаблона под другим именем, чтобы не испортить исходный файл 

            Excel::Sheets excelSheet = excelBook.Worksheets;
            Excel::Worksheet excelWorksheet = (Excel::Worksheet)excelSheet.get_Item("f112ep"); // заполнение рабочего листа ланными из dataGridView 

            excelWorksheet.Range["P37"].Value = textBox_otkogo.Text;
            excelWorksheet.Range["AB39"].Value = textBox_adrotravitelya.Text;
            excelWorksheet.Range["T28"].Value = dataGridView1.CurrentRow.Cells[0].Value;
            excelAplication.ActiveWorkbook.SaveAs(fname, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelAplication.Quit();

            if (checkBox2.Checked)
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = fname;
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //textBox_komu.Text = "";
            //textBox_kuda.Text = "";
            //textBox_index.Text = "";
            textBox_otkogo.Text = "";
            textBox_summa.Text = "";
            textBox_adrotravitelya.Text = "";

            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                GetFieldsFromDataGrid();
            }
        }

        void GetPasportData()
        {
            try
            {
                dataGridView2.DataSource = db_idp.GetPASP("379"); //dataGridView1.CurrentRow.Cells["RksId"].Value.ToString()
                if (dataGridView2.CurrentRow != null)
                {
                    textBox_komu.Text = dataGridView2.CurrentRow.Cells["Name"].Value.ToString();
                    textBox_kuda.Text = dataGridView2.CurrentRow.Cells["Adres"].Value.ToString();
                    textBox_index.Text = dataGridView2.CurrentRow.Cells["Zip"].Value.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка получения отправителя");
            }
        }

        void GetFieldsFromDataGrid()
        {
            textBox_otkogo.Text = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
            textBox_summa.Text = dataGridView1.CurrentRow.Cells["TotalSumm"].Value.ToString();
            textBox_adrotravitelya.Text = dataGridView1.CurrentRow.Cells["Adress"].Value.ToString();
            //GetPasportData();
        }

        public void FormatColumns()
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Columns["1"].Width = 30;
                dataGridView1.Columns["WorkId"].Width = 50;
                dataGridView1.Columns["RksId"].Width = 30;
                dataGridView1.Columns["DateInsert"].Width = 60;
                dataGridView1.Columns["UserName"].Width = 70;


                dataGridView1.Columns["Overheads"].Width = 20;
                dataGridView1.Columns["StatusId"].Width = 20;

                dataGridView1.Columns["TID1"].Width = 30;
                dataGridView1.Columns["TID2"].Width = 30;
                dataGridView1.Columns["TID3"].Width = 30;
                dataGridView1.Columns["TID4"].Width = 30;
                dataGridView1.Columns["TID5"].Width = 30;

                try
                {
                    dataGridView1.Columns["Phone"].Width = 80;
                }
                catch
                {

                }
                dataGridView1.Columns["ClientName"].Width = 100;
                dataGridView1.Columns["Adress"].Width = 200;
                dataGridView1.Columns["PriceName"].Width = 200;
                dataGridView1.Columns["TotalSumm"].Width = 50;
                dataGridView1.Columns["CommentsIM"].Width = 200;
                dataGridView1.Columns["GoodsPrice"].Width = 50;
                dataGridView1.Columns["DLVPice"].Width = 50;
                dataGridView1.Columns["Bonus"].Width = 50;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string status = dataGridView1.Rows[i].Cells["StatusName"].Value.ToString();

                    if (status == "Отказ MGR")
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    if (status == "Доставлен")
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    if (status == "Возврат Почта")
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(153, 0, 0);
                    if (status == "Прибыл")
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 20, 147);
                    if (status == "Выкуплен")
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(0, 191, 255);

                }
            }
        }

        private void RKS_Selector(string s1, string s2, string filter)
        {
            string date1 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + (dateTimePicker2.Value.Day - 1).ToString();
            string date2 = dateTimePicker3.Value.AddDays(1).Year.ToString() + "-" + dateTimePicker3.Value.AddDays(1).Month.ToString() + "-" + dateTimePicker3.Value.AddDays(1).Day.ToString();

            if (FindAllRegId.Checked)
                dataGridView1.DataSource = db_rks.GetRKS(richTextBox1, date1, date2, "0", s1, filter, show_phones);
            else
                dataGridView1.DataSource = db_rks.GetRKS(richTextBox1, date1, date2, RegionId, s1, filter, show_phones);

            FormatColumns();
            label1.Text = "Всего заказов: " + (dataGridView1.RowCount).ToString();
        }

        public void SetDeliveredStatus(string WorkId, string username)
        {
            string dostavka = "";

            if (comboBox1.SelectedItem.ToString() == "Казахстан")
                dostavka = "2600";
            else
                dostavka = "740";

            string q1 = @"SELECT ch0 from `Users` WHERE Name = '" + username + "'";
            string STAVKA = db_idp.SqlQueryWithResult(q1);

            double stavka = Convert.ToDouble(STAVKA);
            stavka = stavka / 100;
            STAVKA = stavka.ToString().Replace(",", ".");

            //MessageBox.Show("STAVKA=" + STAVKA.ToString());

            string qq = "UPDATE `RKS` SET Overheads = 75 WHERE WorkId = " + WorkId; //DLVPice
            db_rks.SqlQuery(qq, ""); //Проставлена цена доставки

            string q2 = @"UPDATE `RKS` SET Bonus=(TotalSumm-" + dostavka + ")*" + STAVKA + " WHERE WorkId='" + WorkId + "'";
            //MessageBox.Show(q2);
            db_rks.SqlQuery(q2, ""); //Бонус проставлен

            string userid = db_idp.SqlQueryWithResult("SELECT Id from Users WHERE Name='" + username + "'");
            string totalsumm = db_rks.SqlQueryWithResult("SELECT TotalSumm from `RKS` WHERE WorkId='" + WorkId + "'");

            //MessageBox.Show("totalsumm=" + totalsumm);
            double accr = (Convert.ToInt32(totalsumm) - Convert.ToInt32(dostavka)) * stavka;
            accr = Convert.ToInt32(accr);

            string accr_exists = db_idp.SqlQueryWithResult("SELECT accr from accruals WHERE WorkId=" + WorkId);
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (accr_exists != "0")
            {
                string q3 = "UPDATE accruals SET userid=" + userid + ", accr=" + accr + ", op_data='" + now + "' WHERE WorkId=" + WorkId;
                MessageBox.Show("По этому заказу начисление уже есть. Пересчитываем");
                db_idp.SqlQuery(q3, ""); //accruals update
            }
            else
            {
                string q3 = "INSERT into `accruals` VALUES(NULL, " + userid + ", " + WorkId + " ," + accr.ToString() + ", '" + now + "')";
                db_idp.SqlQuery(q3, ""); //accruals insert
            }
            Application.DoEvents();
            MessageBox.Show("В зарплату проставилось: (" + Convert.ToInt32(totalsumm) + "-" + Convert.ToInt32(dostavka) + ")*" + stavka + "=" + accr + "Cтавка=" + stavka + "!!!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Казахстан")
                comboBox_shab_word.SelectedIndex = 1;
            else
                comboBox_shab_word.SelectedIndex = 0;

            RegionId = db_idp.GetRegionId_ByName(comboBox1.SelectedItem.ToString());
        }

        private void RksForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Maximum = dataGridView1.Rows.Count;
            progressBar1.Value = ipos;
            ipos++;

            ChangeFormText("выгрузка " + ipos.ToString() + " из " + end_pos.ToString());

            dataGridView1.Rows[ipos].Selected = true;
            dataGridView1.CurrentCell = dataGridView1[0, ipos];
            GetFieldsFromDataGrid();
            MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
        }

        private void button4_Click_2(object sender, EventArgs e)
        {

        }

        private void button7_Click_2(object sender, EventArgs e)
        {
        }

        private void button8_Click_2(object sender, EventArgs e)
        {
            int prihod = 0;
            int rashod = 0;
            int pribil = 0;
            int goodsprice = 0;
            int overheads = 0;
            int dlvpice = 0;
            int bonus = 0;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                goodsprice += Convert.ToInt32(dataGridView1.Rows[i].Cells["GoodsPrice"].Value.ToString());
                overheads += Convert.ToInt32(dataGridView1.Rows[i].Cells["Overheads"].Value.ToString());
                dlvpice += Convert.ToInt32(dataGridView1.Rows[i].Cells["DLVPice"].Value.ToString());
                bonus += Convert.ToInt32(dataGridView1.Rows[i].Cells["Bonus"].Value.ToString());

                if (dataGridView1.Rows[i].Cells["StatusName"].Value.ToString() == "Доставлен")
                {
                    string s2 = dataGridView1.Rows[i].Cells["TotalSumm"].Value.ToString();
                    int i2 = Convert.ToInt32(s2);
                    prihod += i2;
                }
            }

            rashod = goodsprice + overheads + dlvpice + bonus;
            pribil = prihod - rashod;

            string s = "";
            s += "Приход=" + prihod.ToString() + Environment.NewLine;
            s += "Расход=" + rashod.ToString() + Environment.NewLine;
            s += "Прибыль=" + pribil.ToString() + Environment.NewLine;
            textBox_stata1.Text = s;
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string column_name = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name.ToString();

            if (column_name == "Phone")
            {
                string s = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                db_idp.CallPhone(s);
            }
            else
            {
                FormRksStatus f = new FormRksStatus();

                f.rksname = rksname;
                f.tid1 = dataGridView1.CurrentRow.Cells["TID1"].Value.ToString();
                f.tid2 = dataGridView1.CurrentRow.Cells["TID2"].Value.ToString();
                f.tid3 = dataGridView1.CurrentRow.Cells["TID3"].Value.ToString();
                f.tid4 = dataGridView1.CurrentRow.Cells["TID4"].Value.ToString();
                f.tid5 = dataGridView1.CurrentRow.Cells["TID5"].Value.ToString();

                f.textBox_fio.Text = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
                f.textBox2.Text = dataGridView1.CurrentRow.Cells["Adress"].Value.ToString();
                f.textBox3.Text = dataGridView1.CurrentRow.Cells["CommentsIM"].Value.ToString();

                try
                {
                    f.textBox_phone.Text = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                }
                catch
                {

                }

                f.textBox_workid.Text = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
                f.textBox_tracking.Text = dataGridView1.CurrentRow.Cells["CommentsKs"].Value.ToString();

                f.RegionId = RegionId;
                f.ShowDialog();

                FilterDayBtn_Click(null, null);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();

            string column_name = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name.ToString();

            if (column_name == "CommentsIM")
            {
                string q = "UPDATE RKS SET CommentsIM='" + s + "' WHERE WorkId=" + WorkId;
                db_rks.SqlQuery(q, ""); //Обновили комент
            }
            if (column_name == "CommentsKs")
            {
                //if (s.Length == 14)
                //{ 
                //string q = "UPDATE RKS SET CommentsKs='" + s + "' WHERE WorkId=" + WorkId;
                //db_rks.SqlQuery(q, ""); //Обновили комент
                //MessageBox.Show("Откройте заказ 2 раза щелкнув на него и там уже исправляйте!");
            }
            if (column_name == "DLVPice")
            {
                string q = "UPDATE RKS SET DLVPice='" + s + "' WHERE WorkId=" + WorkId;
                db_rks.SqlQuery(q, ""); //Обновили цену доставки
            }

            //ClientName='" + textBox1.Text + "', Adress='" + textBox2.Text + "',, Phone='" + textBox4.Text + "'" +
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            FormTableProfit f = new FormTableProfit();
            f.Show();
        }

        private void button_find_Click_1(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            string s = textBox1.Text.ToLower();
            listBox1.Items.Clear();

            if (s != "")
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                }
                FormatColumns();

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
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

                label6.Text = (vhojdenienum + 1).ToString() + " из " + listBox1.Items.Count.ToString();

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

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_find_Click_1(sender, e);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox_print2.Text = dataGridView1.Rows.Count.ToString();

            int start_pos = 0;
            int end_pos = dataGridView1.Rows.Count - 1;
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;
                if (index != -1)
                    start_pos = index;

                textBox_print1.Text = start_pos.ToString();
            }

            if (checkBox_print.Checked)
            {
                start_pos = Convert.ToInt32(textBox_print1.Text);
                end_pos = Convert.ToInt32(textBox_print2.Text);
            }

            progressBar1.Maximum = dataGridView1.Rows.Count;
            for (int i = start_pos; i <= end_pos; i++)
            {
                progressBar1.Value = i;
                ChangeFormText("выгрузка " + ipos.ToString() + " из " + end_pos.ToString());
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                GetFieldsFromDataGrid();
                MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
                Application.DoEvents();
            }

            progressBar1.Value = 0;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                MakeExcelFile(comboBox_shab_excel.SelectedItem.ToString());
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            progressBar1.Maximum = dataGridView1.Rows.Count;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                progressBar1.Value = i;
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                GetFieldsFromDataGrid();
                MakeExcelFile(comboBox_shab_excel.SelectedItem.ToString());
                Application.DoEvents();
            }
        }

        void HideTel()
        {
            try
            {
                dataGridView1.Columns["Tel"].Visible = false;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                    dataGridView1.Rows[i].Cells["Phone"].Value = "*************";
            }
            catch
            {

            }
        }

        private void FilterDayBtn_Click(object sender, EventArgs e)
        {
            last_button = "1";
            int index = dataGridView1.FirstDisplayedScrollingRowIndex;

            //string dt = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            string s1 = dateTimePicker1.Value.Year.ToString() + "-" +
            dateTimePicker1.Value.Month.ToString() + "-" +
            (dateTimePicker1.Value.Day - 1).ToString();


            string s2 = dateTimePicker1.Value.Year.ToString() + "-" +
            dateTimePicker1.Value.Month.ToString() + "-" +
            (dateTimePicker1.Value.Day + 1).ToString();

            if (dateTimePicker1.Value.Day == 31)
            {
                s2 = dateTimePicker1.Value.Year.ToString() + "-" +
                (dateTimePicker1.Value.Month + 1).ToString() + "-" +
                (1).ToString();
            }

            comboBox1_SelectedIndexChanged(sender, e);
            string s3 = RegionId;

            dataGridView1.DataSource = db_rks.GetRKS(richTextBox1, s1, s2, s3, "", "", show_phones);
            HideTel();

            FormatColumns();
            label1.Text = "Всего заказов: " + (dataGridView1.RowCount).ToString();
            textBox_print2.Text = (dataGridView1.RowCount).ToString();
            //button8_Click_2(null, null);

            textBox_stata1.Text = db_rks.GetFullStatTable1(s1, s2, s3);
            textBox_stata2.Text = db_rks.GetFullStatTable2(s1, s2, s3);

            if (index > 0)
            {
                try
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = index;
                }
                catch (Exception ex)
                {

                }
            }
            dataGridView1.Update();
        }

        private void FilterTextBtn_Click(object sender, EventArgs e)
        {
            last_button = "2";

            if (comboBox_stat.SelectedItem.ToString() == "Все статусы")
            {
                if (textBox_filter.Text != "")
                {
                    if (comboBox_stat.SelectedItem.ToString() == "Все статусы")
                        RKS_Selector("", "", textBox_filter.Text);
                }
            }
            else
            {
                if (comboBox_stat.SelectedItem.ToString() == "Одобрен")
                    RKS_Selector("Одобрен", "Одобрено: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Возврат Почта")
                    RKS_Selector("Возврат Почта", "Возвратов почты: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Возврат")
                    RKS_Selector("Возврат", "Возвратов: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Прибыл")
                    RKS_Selector("Прибыл", "Прибыло: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Доставлен")
                    RKS_Selector("Доставлен", "Доставлено: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Отправлен")
                    RKS_Selector("Отправлен", "Отправлено: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Выкуплен")
                    RKS_Selector("Выкуплен", "Выкуплено: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Брак")
                    RKS_Selector("Брак", "Брак: ", textBox_filter.Text);

                if (comboBox_stat.SelectedItem.ToString() == "Отказ MGR")
                    RKS_Selector("Отказ MGR", "Отказ MGR: ", textBox_filter.Text);
            }

            dataGridView1.Focus();
            HideTel();
        }

        private void SetStatusBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
                string username = dataGridView1.CurrentRow.Cells["UserName"].Value.ToString();
                string old_status = dataGridView1.CurrentRow.Cells["StatusName"].Value.ToString();

                string ClientName = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
                string CommentsKs = dataGridView1.CurrentRow.Cells["CommentsKs"].Value.ToString();

                string StatusId = "0";
                string StatusName = comboBox_status.SelectedItem.ToString();

                string tid1 = dataGridView1.CurrentRow.Cells["TID1"].Value.ToString();
                string tid2 = dataGridView1.CurrentRow.Cells["TID2"].Value.ToString();
                string tid3 = dataGridView1.CurrentRow.Cells["TID3"].Value.ToString();
                string tid4 = dataGridView1.CurrentRow.Cells["TID4"].Value.ToString();
                string tid5 = dataGridView1.CurrentRow.Cells["TID5"].Value.ToString();

                if (StatusName == "Отказ MGR")
                {
                    StatusId = "5";
                    
                    if (tid1!="0")
                        db_idp.VozvratTovar(rksname, tid1);
                    if (tid2 != "0")
                        db_idp.VozvratTovar(rksname, tid2);
                    if (tid3 != "0")
                        db_idp.VozvratTovar(rksname, tid3);
                    if (tid4 != "0")
                        db_idp.VozvratTovar(rksname, tid4);
                    if (tid5 != "0")
                        db_idp.VozvratTovar(rksname, tid5);
                }

                if (StatusName == "Одобрен")
                {
                    StatusId = "6";

                    //if (old_status != "Одобрен")
                    //    db_idp.SpisatSoSklada(RegionId, WorkId);
                }

                if (StatusName == "Доставлен")
                    StatusId = "7";
                if (StatusName == "Возврат")
                    StatusId = "16";
                if (StatusName == "Отправлен")
                {
                    string Phone = "";

                    try
                    {
                        Phone = db_rks.SqlQueryWithResult("SELECT Phone from RKS WHERE WorkId='" + WorkId + "'");
                        //MessageBox.Show(Phone);
                        //Phone = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                    }
                    catch
                    {

                    }

                    StatusId = "18";
                    string qq = "UPDATE `RKS` SET DLVPice = 75 WHERE WorkId = " + WorkId;
                    db_rks.SqlQuery(qq, ""); //Проставлена цена доставки
                    db_rks.SendToLiveinform(db_rks.api_id2, WorkId, Phone, ClientName, CommentsKs);
                }

                if (StatusName == "Прибыл")
                    StatusId = "19";
                if (StatusName == "Выкуплен")
                    StatusId = "20";
                if (StatusName == "Возврат Почта")
                    StatusId = "21";
                if (StatusName == "Брак")
                    StatusId = "30";

                db_rks.SetStatus(WorkId, StatusId, StatusName);
                db_idp.SetStatus(WorkId, StatusId, StatusName);

                if (StatusName == "Возврат")
                {
                    string qq = @"DELETE from accruals WHERE workid='" + WorkId + "'";
                    db_idp.SqlQuery(qq, "Удалено из accruals");
                }

                if (StatusName == "Брак")
                {
                    string qq = @"DELETE from accruals WHERE workid='" + WorkId + "'";
                    db_idp.SqlQuery(qq, "Удалено из accruals");
                }

                if (StatusName == "Доставлен")
                {
                    SetDeliveredStatus(WorkId, username);
                    dataGridView1.CurrentRow.Cells["StatusName"].Value = "Доставлен";
                }
            }

            if (last_button == "1")
                FilterDayBtn_Click(null, null);
            if (last_button == "2")
                FilterTextBtn_Click(null, null);
        }

        private void DayCostsBtn_Click(object sender, EventArgs e)
        {
            FormDayCosts f = new FormDayCosts();
            f.RegionId = RegionId;
            f.ShowDialog();
        }

        private void CostsBtn_Click(object sender, EventArgs e)
        {

        }

        private void DayStatBtn_Click(object sender, EventArgs e)
        {
            FormDayStat f = new FormDayStat();
            f.ShowDialog();
        }


        private void KillWordsBtn_Click(object sender, EventArgs e)
        {
            List<string> name = new List<string> { "winword" };//процесс, который нужно убить
            System.Diagnostics.Process[] etc = System.Diagnostics.Process.GetProcesses();//получим процессы
            foreach (System.Diagnostics.Process anti in etc)//обойдем каждый процесс
            {
                foreach (string s in name)
                {
                    if (anti.ProcessName.ToLower().Contains(s.ToLower())) //найдем нужный и убьем
                    {
                        anti.Kill();
                        name.Remove(s);
                    }
                }
            }
        }

        private void Mass_Status_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                SetStatusBtn_Click(null, null);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("1");
        }

        private void RksBonusBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void StartPrintBtn_Click(object sender, EventArgs e)
        {
            if (checkBox_print.Checked)
                ipos = Convert.ToInt32(textBox_print1.Text);
            end_pos = dataGridView1.Rows.Count;
            timer1.Interval = Convert.ToInt32(textBox2.Text) * 1000;
            timer1.Enabled = true;
        }

        private void StopPrintBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        public class MyDataGridView : System.Windows.Forms.DataGridView // Создаём класс наследуемый от Forms.DataGridView
        {
            public int i = 0;           // переменная для примера
            protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) // И переопределяем метод обработки нажатия управляющих клавиш, унаследованный от Control
            {
                this.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

                if ((keyData == (Keys.C | Keys.Control)))  // Если нажаты CTRL+C
                {
                    Clipboard.SetText(this.CurrentCell.Value.ToString());
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FilterTextBtn_Click(sender, e);
            }
        }

        private void dataGridView1_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //MessageBox.Show("dataGridView1_ColumnHeaderMouseClick");
            FormatColumns();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormZP f = new FormZP();
            f.ShowDialog();
        }

        private void tabpage3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormOstatki f = new FormOstatki();
            f.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BeginEdit(false);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormQueueStat f = new FormQueueStat();
            f.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void RksBonusBtn_Click(object sender, EventArgs e)
        {
            FormRksBonus f = new FormRksBonus();
            f.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FormChart f = new FormChart();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                dataGridView1.CurrentRow.Selected = true;
                //SetStatusBtn_Click(null, null);
                string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
                string Phone = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                string ClientName = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
                string CommentsKs = dataGridView1.CurrentRow.Cells["CommentsKs"].Value.ToString();
                db_rks.SendToLiveinform(db_rks.api_id2, WorkId, Phone, ClientName, CommentsKs);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            FormStata f = new FormStata();
            f.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {


        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://id-points.ru/newstat.php");
        }


        private void button10_Click_2(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://id-points.ru/newstat.php?p1=1");
        }
        
        void GetLiveInform()
        {
            string dt = DateTime.Now.Year.ToString() + "" + DateTime.Now.Month.ToString() + "" + DateTime.Now.Day.ToString();
            string tm = DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                dataGridView1.CurrentRow.Selected = true;

                string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
                string Phone = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                string Fio = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
                string CommentsKs = dataGridView1.CurrentRow.Cells["CommentsKs"].Value.ToString();

                if (CommentsKs.Length > 13)
                {
                    CommentsKs = CommentsKs.Substring(0, 14);
                    //db_rks.SendToLiveinform(WorkId, Phone, Fio, CommentsKs);

                    string s = "";

                    //try
                    //{
                        s=db_rks.GetFromLiveinform(WorkId, Phone, Fio, CommentsKs);
                    //}
                    //catch(Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}

                    if (s != "")
                    {
                        string currentDate = DateTime.Now.Date.ToString();
                        string currentTime = DateTime.Now.TimeOfDay.ToString();
                        //DateTime.Now.Date.
                        richTextBox1.Text += currentDate + " " + currentTime + " " + s + Environment.NewLine;
                    }
                }
                Application.DoEvents();

                if (i % 20 == 0)
                {
                    //StreamWriter wr = new StreamWriter(Application.StartupPath + "\\log" + dt + "_" + tm + ".txt");
                    //wr.Write(richTextBox1.Text);

                    richTextBox1.SaveFile(Application.StartupPath + "\\log" + dt + "_" + tm + ".txt", RichTextBoxStreamType.PlainText);
                    //wr.Close();
                }
            }
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            vhojdenienum = 0;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            FormUsers f = new FormUsers();
            f.ShowDialog();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            string t1 = "100\n{\"status\":\"1\",\"weight\":\"0.395\",\"value\":\"3740\",\"price\":\"3740\",\"additional1\":\"\",\"additional2\":\"\",\"additional3\":\"\",\"delivery\":\"RU\",\"0\":{\"date\":\"06.03.2017 09:49\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u0440\\u0438\\u0431\\u044b\\u043b\\u043e \\u0432 \\u043c\\u0435\\u0441\\u0442\\u043e \\u0432\\u0440\\u0443\\u0447\\u0435\\u043d\\u0438\\u044f\",\"geo\":\"\\u041d\\u043e\\u0432\\u0430\\u044f \\u041c\\u0430\\u0439\\u043d\\u0430 1\",\"index\":\"433556\"},\"1\":{\"date\":\"06.03.2017 08:56\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u043e\\u043a\\u0438\\u043d\\u0443\\u043b\\u043e \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u0414\\u0438\\u043c\\u0438\\u0442\\u0440\\u043e\\u0432\\u0433\\u0440\\u0430\\u0434 \\u0423\\u041e\\u041e\\u041f\",\"index\":\"433519\"},\"2\":{\"date\":\"04.03.2017 05:01\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u043e\\u043a\\u0438\\u043d\\u0443\\u043b\\u043e \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u0423\\u043b\\u044c\\u044f\\u043d\\u043e\\u0432\\u0441\\u043a \\u0423\\u041e\\u041e\\u041f\",\"index\":\"432098\"},\"3\":{\"date\":\"02.03.2017 19:58\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u043e\\u043a\\u0438\\u043d\\u0443\\u043b\\u043e \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u0421\\u0430\\u043c\\u0430\\u0440\\u0430 \\u041c\\u0421\\u0426\",\"index\":\"443960\"},\"4\":{\"date\":\"01.03.2017 08:49\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u0440\\u0438\\u0431\\u044b\\u043b\\u043e \\u0432 \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u0421\\u0430\\u043c\\u0430\\u0440\\u0430 \\u041c\\u0421\\u0426\",\"index\":\"443960\"},\"5\":{\"date\":\"27.02.2017 14:14\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u043e\\u043a\\u0438\\u043d\\u0443\\u043b\\u043e \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u041d\\u043e\\u0432\\u043e\\u0441\\u0438\\u0431\\u0438\\u0440\\u0441\\u043a \\u041c\\u0421\\u0426\",\"index\":\"630960\"},\"6\":{\"date\":\"26.02.2017 11:00\",\"operation\":\"\\u041e\\u0431\\u0440\\u0430\\u0431\\u043e\\u0442\\u043a\\u0430\",\"text\":\"\\u041f\\u0440\\u0438\\u0431\\u044b\\u043b\\u043e \\u0432 \\u0441\\u043e\\u0440\\u0442\\u0438\\u0440\\u043e\\u0432\\u043e\\u0447\\u043d\\u044b\\u0439 \\u0446\\u0435\\u043d\\u0442\\u0440\",\"geo\":\"\\u041d\\u043e\\u0432\\u043e\\u0441\\u0438\\u0431\\u0438\\u0440\\u0441\\u043a \\u041c\\u0421\\u0426\",\"index\":\"630960\"},\"7\":{\"date\":\"24.02.2017 14:14\",\"operation\":\"\\u041f\\u0440\\u0438\\u0435\\u043c\",\"text\":\"\\u0415\\u0434\\u0438\\u043d\\u0438\\u0447\\u043d\\u044b\\u0439\",\"geo\":\"\\u041d\\u043e\\u0432\\u043e\\u0441\\u0438\\u0431\\u0438\\u0440\\u0441\\u043a 49\",\"index\":\"630049\"}}";
            string geo=t1.Substring(t1.IndexOf("geo") + 4, t1.Length - (t1.IndexOf("geo") + 4));
            string index = t1.Substring(t1.IndexOf("index") + 6, t1.Length - (t1.IndexOf("index") + 6));
            geo = geo.Substring(0, geo.IndexOf(","));
            index = index.Substring(0, index.IndexOf(","));
            string temp_geo = System.Text.RegularExpressions.Regex.Unescape(geo);
            MessageBox.Show(temp_geo + " " + index);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);


        private void timer2_Tick(object sender, EventArgs e)
        {
            //IntPtr HWND = FindWindow(null, "EyeBeam");
            //ShowWindow(HWND, 0);
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            db_idp.HangUp();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //e.ColumnIndex
        }


        private void GetStatus_LiveInform1_Click(object sender, EventArgs e)
        {
            GetLiveInform();
        }
        private void GetStatus_LiveInform2_Click(object sender, EventArgs e)
        {
            GetLiveInform();
        }

        private void CostsBtn_Click_1(object sender, EventArgs e)
        {
            if (rksname == "Denis" || rksname == "NatRKS" || rksname == "OlyaRKS")
            {
                FormCosts f = new FormCosts();

                if (rksname == "Denis" || rksname == "OlyaRKS")
                    f.current_id = "9";

                if (rksname == "NatRKS")
                    f.current_id = "13";

                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет доступа");
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1[0, 0];
            GetFieldsFromDataGrid();
            MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
        }
    }
}
