using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace adminka
{ 
    public partial class RksForm : Form
    {
        public RksForm()
        {
            InitializeComponent();
        }

        DB db = new DB();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(2016, 11, 1);
            dateTimePicker2.Value = new DateTime(2016, 1, 1);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 0;
            comboBox_shab_word.SelectedIndex = 0;
            comboBox_shab_excel.SelectedIndex = 0;
        }

        void MakeWordFile(string shab_fname)
        {
            Word._Application oWord = new Word.Application();

            string dir = comboBox1.SelectedItem.ToString() + "_" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            DirectoryInfo directoryinfo = Directory.CreateDirectory(Environment.CurrentDirectory + "\\" + dir);

            Word._Document oDoc=oWord.Documents.Add(Environment.CurrentDirectory + "\\" + shab_fname);
            string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
            string fname = Environment.CurrentDirectory + "\\" + dir + "\\" + WorkId + "_112ЭК.doc";

            try
            {
                SetTemplate(oDoc);
                oDoc.SaveAs(FileName: fname);
            }
            catch
            {
                MessageBox.Show("err");
                return;
            }

            if (checkBox1.Checked)
                oDoc.PrintOut();

            oDoc.Close();

            if (checkBox2.Checked)
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.FileName = fname;
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetTemplate(Word._Document oDoc)
        {
            PriceInWords PriceInWords = new PriceInWords();

            oDoc.Bookmarks["Summa"].Range.Text = textBox_summa.Text;

            if (comboBox1.SelectedItem.ToString() == "Казахстан")
                oDoc.Bookmarks["SummaPr"].Range.Text = PriceInWords.SummaPropis(textBox_summa.Text, "тенге");
            else
                oDoc.Bookmarks["SummaPr"].Range.Text = PriceInWords.SummaPropis(textBox_summa.Text, "рублей 00 копеек");

            oDoc.Bookmarks["Komu"].Range.Text = textBox_komu.Text;
            oDoc.Bookmarks["Kuda"].Range.Text = textBox_kuda.Text;
            oDoc.Bookmarks["Otkogo"].Range.Text = textBox_otkogo.Text;

            string s = textBox_adrotravitelya.Text;
            String[] words = s.Split(new char[] { ',' });

            // MessageBox.Show(words.Length.ToString());

            string Oblast = words[1];

            if (!Oblast.Trim().ToLower().Contains(" "))
                Oblast = Oblast + " обл ";

            string Raion = words[2];
            string NasPunkt = words[3];
            string Ulica = words[4];

            if (!words[4].Contains("ул"))
                words[4] = "ул. " + words[4] + ", ";


            if (words.Length >= 7)
                oDoc.Bookmarks["Gorod"].Range.Text = words[4] + " д. " + words[5] + ", " + " кв. " + words[6];
            else
                oDoc.Bookmarks["Gorod"].Range.Text = words[4] + " д. " + words[5];

            if (Raion.Length > 0)
                oDoc.Bookmarks["Ulica"].Range.Text = Oblast + ", " + Raion + ", ";
            else
                oDoc.Bookmarks["Ulica"].Range.Text = Oblast + ", ";


            if (!NasPunkt.Contains("г.") && !NasPunkt.Contains("с ") && !NasPunkt.Contains("пгт ") && !NasPunkt.Contains("п ") && !NasPunkt.Contains("рп ") && !NasPunkt.Contains("д "))
                NasPunkt = ", г. " + NasPunkt;
            
            oDoc.Bookmarks["Kvart"].Range.Text = NasPunkt;

            string ZIP = words[0];
            oDoc.Bookmarks["I1"].Range.Text = ZIP[0].ToString();
            oDoc.Bookmarks["I2"].Range.Text = ZIP[1].ToString();
            oDoc.Bookmarks["I3"].Range.Text = ZIP[2].ToString();
            oDoc.Bookmarks["I4"].Range.Text = ZIP[3].ToString();
            oDoc.Bookmarks["I5"].Range.Text = ZIP[4].ToString();
            oDoc.Bookmarks["I6"].Range.Text = ZIP[5].ToString();
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
            textBox_komu.Text = "";
            textBox_kuda.Text = "";
            textBox_index.Text = "";
            textBox_otkogo.Text = "";
            textBox_summa.Text = "";
            textBox_adrotravitelya.Text = "";

            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                GetFieldsFromDataGrid();
            }
        }

        void GetFieldsFromDataGrid()
        {
            dataGridView2.DataSource = db.GetPASP(dataGridView1.CurrentRow.Cells["RksId"].Value.ToString());

            if (dataGridView2.CurrentRow != null)
            {
                textBox_otkogo.Text = dataGridView1.CurrentRow.Cells["ClientName"].Value.ToString();
                textBox_summa.Text = dataGridView1.CurrentRow.Cells["TotalSumm"].Value.ToString();
                textBox_adrotravitelya.Text = dataGridView1.CurrentRow.Cells["Adress"].Value.ToString();

                textBox_komu.Text = dataGridView2.CurrentRow.Cells["Name"].Value.ToString();
                textBox_kuda.Text = dataGridView2.CurrentRow.Cells["Adres"].Value.ToString();
                textBox_index.Text = dataGridView2.CurrentRow.Cells["Zip"].Value.ToString();    
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow != null)
            //{
            //    int index = dataGridView1.CurrentRow.Index;
            //    if (index != -1)
            //    {
            progressBar1.Maximum = dataGridView1.Rows.Count;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                progressBar1.Value = i;
                dataGridView1.Rows[i].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, i];
                GetFieldsFromDataGrid();
                MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
                Application.DoEvents();
            }
            //   }
            // }
            progressBar1.Value = 0;
        }

        private void textBox_adrotravitelya_Click(object sender, EventArgs e)
        {

        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_find_Click(sender, e);
            }
        }

        private void button_find_Click(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["Workid"].Value.ToString() == textBox1.Text)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                if (dataGridView1.Rows[i].Cells["ClientName"].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                if (dataGridView1.Rows[i].Cells["CommentsIM"].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                    dataGridView1.Rows[i].Selected = true;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
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

        public void FormatColumns()
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Columns["WorkId"].Width = 50;
                dataGridView1.Columns["RksId"].Width = 30;
                dataGridView1.Columns["DateDelivery"].Width = 70;
                dataGridView1.Columns["UserName"].Width = 70;
                dataGridView1.Columns["Phone"].Width = 80;
                dataGridView1.Columns["ClientName"].Width = 200;
                dataGridView1.Columns["Adress"].Width = 350;
                dataGridView1.Columns["PriceName"].Width = 350;
                dataGridView1.Columns["TotalSumm"].Width = 70;
                dataGridView1.Columns["CommentsIM"].Width = 200;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s1 = dateTimePicker1.Value.Year.ToString() + "-" +
            dateTimePicker1.Value.Month.ToString() + "-" +
            (dateTimePicker1.Value.Day - 1).ToString();

            string s2 = dateTimePicker1.Value.Year.ToString() + "-" +
            dateTimePicker1.Value.Month.ToString() + "-" +
            (dateTimePicker1.Value.Day + 1).ToString();

            string s3 = GetRegionId();

            if (checkBox3.Checked)
                dataGridView1.DataSource = db.GetRKS(richTextBox1, s1, s2, s3, "Одобрен");
            else
                dataGridView1.DataSource = db.GetRKS(richTextBox1, s1, s2, s3, "");

            FormatColumns();
            label1.Text = "Одобренных заказов: " + (dataGridView1.RowCount).ToString();
        }
        
        private void RKS_Selector(string s1, string s2)
        {            
            string date1 = dateTimePicker2.Value.Year.ToString() + "-" + dateTimePicker2.Value.Month.ToString() + "-" + (dateTimePicker2.Value.Day - 1).ToString();
            string date2 = dateTimePicker3.Value.AddDays(1).Year.ToString() + "-"  +dateTimePicker3.Value.AddDays(1).Month.ToString() + "-" + dateTimePicker3.Value.AddDays(1).Day.ToString();
            string RegionId = GetRegionId();

            dataGridView1.DataSource = db.GetRKS(richTextBox1, date1, date2, RegionId, s1);
            FormatColumns();
            label1.Text = s2 + (dataGridView1.RowCount).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount>0)
                MakeWordFile(comboBox_shab_word.SelectedItem.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                MakeExcelFile(comboBox_shab_excel.SelectedItem.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            RKS_Selector("Одобрен", "Одобрено: ");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RKS_Selector("Выкуплен", "Выкуплено: ");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RKS_Selector("Прибыл", "Прибыло: ");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            RKS_Selector("Доставлен", "Доставлено: ");
        }
        
        private void button4_Click_1(object sender, EventArgs e)
        {
            RKS_Selector("Возврат Почта", "Возвратов почты: ");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            RKS_Selector("Возврат", "Возвратов: ");
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            RKS_Selector("Отправлен", "Отправлено: ");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
                string StatusId = "0";
                string StatusName = comboBox2.SelectedItem.ToString();

                if (StatusName == "Одобрен")
                    StatusId = "6";
                if (StatusName == "Доставлен")
                    StatusId = "7";
                if (StatusName == "Возврат")
                    StatusId = "16";
                if (StatusName == "Отправлен")
                    StatusId = "18";
                if (StatusName == "Прибыл")
                    StatusId = "19";
                if (StatusName == "Выкуплен")
                    StatusId = "20";
                if (StatusName == "Возврат почта")
                    StatusId = "21";
                if (StatusName == "Брак")
                    StatusId = "30";
                
                db.SetStatus(WorkId, StatusId, StatusName);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            RKS_Selector("Брак", "Брак: ");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Казахстан")
                comboBox_shab_word.SelectedIndex = 1;
            else
                comboBox_shab_word.SelectedIndex = 0;

        }
    }
}
