using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;



namespace adminka
{
 

    public partial class FormDostavka : Form
    {
        DB_IDP db_idp = new DB_IDP();
        DB_RKS db_rks = new DB_RKS();

        DVG dvg = new DVG();
        int vhojdenienum = 0;

        string current_tab = "";
        int scroll_counter = 0;
        bool enable_scroll=true;
        string temp_str;
 

        Dictionary <string, string> column_width=new Dictionary<string, string>();

        public FormDostavka()
        {
            InitializeComponent();
            dataGridView_users.AllowUserToAddRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView_users.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }


        public static void SetDoubleBuffered(Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            /*typeof(Control).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, control, new object[] { true });*/
        }

        void DataGridView1_MouseWheel(object sender, MouseEventArgs e)
        {
 
        }

        private void FormDostavka_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            DataTable dt = db_rks.GetDostavkaGroupId(textBox_uname.Text);
            string GroupId = dt.Rows[0][0].ToString();
            textBox_groupid.Text = GroupId;
            dataGridView_users.DataSource = db_rks.GetDostavkaUsers(GroupId);

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Все операторы");
            for (int i = 0; i < dataGridView_users.RowCount; i++)
            {
                comboBox1.Items.Add(dataGridView_users[0, i].Value.ToString());
            }
            current_tab = "Прибыл";
            comboBox1.SelectedIndex = 0;
            SetDoubleBuffered(dataGridView1);

            dataGridView1.DoubleBuffered(true);

            dataGridView1.MouseWheel += new MouseEventHandler(DataGridView1_MouseWheel);
            //    //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        DataTable GetOrders(string status, int pribil_plus)
        {
            DataTable dt_full = new DataTable();

            for (int i = 0; i < dataGridView_users.Rows.Count; i++)
            {
                string user = dataGridView_users[0, i].Value.ToString();
                //dataGridView_users.CurrentRow.Selected = true;

                try
                {
                    DataTable dt = db_rks.GetDostavka(textBox1.Text, user, status, pribil_plus);

                    if (dt != null)
                    {
                        dt_full.Merge(dt);
                        Application.DoEvents();
                    }
                }
                catch(Exception ex)
                {
                  //  MessageBox.Show(user + " :: " + ex.Message); // убрал 06-04-2017
                }
            }
            return dt_full;
        }


        DataTable GetOrdersOtpravlen(string status)
        {
            DataTable dt_full = new DataTable();

            for (int i = 0; i < dataGridView_users.Rows.Count; i++)
            {
                string user = dataGridView_users[0, i].Value.ToString();
                //dataGridView_users.CurrentRow.Selected = true;
                DataTable dt = db_rks.GetDostavkaOtpravlen(textBox1.Text, user, status);

                if (dt != null)
                {
                    dt_full.Merge(dt);
                    Application.DoEvents();
                }
            }
            return dt_full;
        }

        public DataTable GetNotify()
        {
            DataTable dt_full = new DataTable();

            for (int i = 0; i < dataGridView_users.Rows.Count; i++)
            {
                string user = dataGridView_users[0, i].Value.ToString();
                //dataGridView_users.CurrentRow.Selected = true;
                DataTable dt = db_rks.GetNotify(user);

                //MessageBox.Show(user);
                if (dt != null)
                {
                    try
                    {
                        dt_full.Merge(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(" +++" + ex);
                    }
                    Application.DoEvents();
                }
            }
            return dt_full;
        }


        private void dataGridView_users_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*string user = dataGridView_users.CurrentRow.Cells[0].Value.ToString();

            if (current_tab == "Прибыл")
            {
                dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Прибыл", 1);
            }
            if (current_tab == "Прибыл+")
            {
                dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Прибыл", 2);
            }
            if (current_tab == "Выкуплен")
            {
                dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Выкуплен", 1);
            }
            if (current_tab == "Заметки")
            {
                dataGridView1.DataSource = db_rks.GetNotify(user);
            }*/
        }

        private void Call_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string phone = dataGridView1.CurrentRow.Cells["Телефон"].Value.ToString();
                db_rks.CallPhone(phone);
            }
        }

        private void AddImgColumns1()
        {
            //MessageBox.Show("AddImgColumns1");
            try
            {
                dvg.AddImgColumn(dataGridView1, Properties.Resources.mail_yellow, "SMS");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка добавления столбца" + ex);
            }
            //dvg.AddImgColumn(dataGridView1, Properties.Resources.clock_edit, "Заметки");
        }


        public void FormatColumns1(string tabname)
        {
            try
            {
                column_width = Read("dictionary_" + current_tab + ".bin");
            }

            catch (Exception ex)
            {
               // MessageBox.Show("вот тут" + ex.Message);
            }


            foreach (var pair in column_width)
            {
                try
                {
                    if(dataGridView1.Columns[pair.Key]!=null)
                    dataGridView1.Columns[pair.Key].Width = Convert.ToInt32(pair.Value);
                }
                catch (Exception ex)
                {
                     MessageBox.Show("вот тут" + ex.Message);
                }
            }

                //dataGridView1.Sort(dataGridView1.Columns["Дата статуса"], ListSortDirection.Descending);


            AddImgColumns1();
            label1.Text = dataGridView1.Rows.Count.ToString();

            if (current_tab == "Отправлен")
            {
                try { dataGridView1.Sort(dataGridView1.Columns["Дата оформ."], ListSortDirection.Ascending); }
                catch(Exception ex) { }
            }


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //for (int j = 0; j < 11; j++)
                //    dataGridView1.Rows[i].Cells[j].ReadOnly = true;

                //MessageBox.Show(dataGridView1.Rows[i].Cells[11].Value.ToString());

                string s = dataGridView1.Rows[i].Cells[11].Value.ToString();
                //s = s.Replace("\n", "\r\n");
                dataGridView1.Rows[i].Cells[11].Value = s;

                string s2 = dataGridView1.Rows[i].Cells[10].Value.ToString();
                s2 = s2.Replace("<br>", "\r\n");
                dataGridView1.Rows[i].Cells[10].Value = s2;
            }
        }

        private void FormDostavka_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void ShowUserCard(int zametki)
        {
            if (dataGridView1.CurrentRow != null)
            {
                FormDostavkaUserCard f = new FormDostavkaUserCard();
                string ksid = "";

                if (zametki == 1)
                    ksid = dataGridView1.CurrentRow.Cells["ksid"].Value.ToString();

                f.textBox_ksid.Text = ksid;
                f.textBox_fio.Text = dataGridView1.CurrentRow.Cells["ФИО заказчика"].Value.ToString();
                f.textBox_adress.Text = dataGridView1.CurrentRow.Cells["Адрес"].Value.ToString();
                f.textBox_phone.Text = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                f.textbox_CommentsKS.Text = dataGridView1.CurrentRow.Cells["Комментарий КС"].Value.ToString();
                f.ShowDialog();
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                {
                    //MessageBox.Show(senderGrid.Columns[e.ColumnIndex].HeaderText.ToString());

                    if (senderGrid.Columns[e.ColumnIndex].HeaderText.ToString() == "SMS")
                        ShowUserCard(1);

                    //if (senderGrid.Columns[e.ColumnIndex].HeaderText.ToString() == "Заметки")
                    ///    ShowUserCard(0);
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            object headValue = ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value;

            if (headValue == null || !headValue.Equals((e.RowIndex + 1).ToString()))
            {
                ((DataGridView)sender).Rows[e.RowIndex].HeaderCell.Value = ((e.RowIndex + 1).ToString());
                ((DataGridView)sender).RowHeadersWidth = 55;
            }
        }

        private void button_pribil_Click(object sender, EventArgs e)
        {
            current_tab = "Прибыл";
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button_arrived_plus_Click(object sender, EventArgs e)
        {
            current_tab = "Прибыл+";
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button_bought_Click(object sender, EventArgs e)
        {
            current_tab = "Выкуплен";
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button_notify_Click(object sender, EventArgs e)
        {
            current_tab = "Заметки";
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            current_tab = "Отправлен";
            comboBox1_SelectedIndexChanged(null, null);
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string column_name = "";

            try
            {
                column_name=dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name.ToString();
            }
            catch (Exception ex)
            {


            }

            if (column_name == "Телефон")
            {
                string s = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                db_idp.CallPhone(s);
            }
        }


        void HideTel()
        {
            try
            {
                dataGridView1.Columns["Tel"].Visible = false;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                    dataGridView1.Rows[i].Cells["Телефон"].Value = "*************";
            }
            catch
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = comboBox1.SelectedItem.ToString();
            this.Text = "FormDostavka :: " + textBox_uname.Text + " :: " + current_tab + " :: " + user;

            dataGridView1.DataSource = null;
            dvg.DeleteImageColumns(dataGridView1);
            dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EEE9E9");

            if (user == "Все операторы")
            {
                if (current_tab == "Прибыл")
                {
                    dataGridView1.DataSource = GetOrders("Прибыл", 1);
                    dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EEE9E9");
                }

                if (current_tab == "Прибыл+")
                {
                    dataGridView1.DataSource = GetOrders("Прибыл", 2);
                    dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EEE9E9");
                }

                if (current_tab == "Выкуплен")
                {
                    dataGridView1.DataSource = GetOrders("Выкуплен", 1);
                    dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00BFFF");
                }

                if (current_tab == "Заметки")
                {
                    dataGridView1.DataSource = GetNotify();
                    dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EEE9E9");
                }

                if (current_tab == "Отправлен")
                {
                    dataGridView1.DataSource = GetOrdersOtpravlen("Отправлен");
                    dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#EEE9E9");
                }
            }
            else
            {
                if (current_tab == "Прибыл")
                    dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Прибыл", 1);

                if (current_tab == "Прибыл+")
                    dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Прибыл", 2);

                if (current_tab == "Выкуплен")
                    dataGridView1.DataSource = db_rks.GetDostavka(textBox1.Text, user, "Выкуплен", 1);

                if (current_tab == "Отправлен")
                    dataGridView1.DataSource = db_rks.GetDostavkaOtpravlen(textBox1.Text, user, "Отправлен");

                if (current_tab == "Заметки")
                {
                    dataGridView1.DataSource = null;
                    dvg.DeleteImageColumns(dataGridView1);
                    dataGridView1.DataSource = db_rks.GetNotify(user);
                }
            }

           // HideTel();

            FormatColumns1(current_tab);
        }

        private void FormDostavka_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                //MessageBox.Show("f");
                textBox2.Focus();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                //MessageBox.Show("f");
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
              //  MessageBox.Show("Down was pressed");
            }
            
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                //MessageBox.Show("Enter was pressed");
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            temp_str = dataGridView1.CurrentCell.Value.ToString();
            /*s=s.Replace("\n", "\r\n");
            dataGridView1.CurrentCell.Value = s;
            MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());*/
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            string WorkId = dataGridView1.CurrentRow.Cells["WorkId"].Value.ToString();
            string ksid = dataGridView1.CurrentRow.Cells["ksid"].Value.ToString();

            s = s.Replace("\r\r", "");
            string column_name = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name.ToString();

            if (s!= temp_str && column_name == "Комментарий ОД")
            {
                string q = "UPDATE RKS SET CommentsOD='" + s + "' WHERE WorkId=" + WorkId;
                db_rks.SqlQuery(q, ""); //Обновили комент
                
                string[] separator = { "\n" };
                String[] words = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                //MessageBox.Show(words.Last());

                db_rks.InsertRKSHistory(ksid, words.Last());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = textBox2.Text.ToLower();

            listBox1.Items.Clear();
            for (int i = 0; i < dataGridView1.RowCount; i++)//dataGridView1.FirstDisplayedScrollingRowIndex+1
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
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

            label2.Text = (vhojdenienum+1).ToString() + " из " + listBox1.Items.Count.ToString();

            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = vhojdenienum;
                int rnum= Convert.ToInt32(listBox1.SelectedItem.ToString());

                enable_scroll = false;
                dataGridView1.FirstDisplayedScrollingRowIndex = rnum;
                enable_scroll = true;
                dataGridView1.Rows[rnum].Selected = true;
                vhojdenienum++;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            vhojdenienum = 0;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(null, null);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
            dataGridView1.BeginEdit(false);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (enable_scroll)
            {
                scroll_counter++;

                if (scroll_counter % 3 == 0)
                {
                    if (e.NewValue > e.OldValue)
                        e.NewValue = e.OldValue + 1;
                    else
                        e.NewValue = e.OldValue - 1;
                }
                else
                {
                    e.NewValue = e.OldValue;
                }
            }
        }
        

        static void Write(Dictionary<string, string> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value);
                }
            }
        }

        static Dictionary<string, string> Read(string file)
        {
            var result = new Dictionary<string, string>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Get count.
                int count = reader.ReadInt32();
                // Read in all pairs.
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    result[key] = value;
                }
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                column_width[dataGridView1.Columns[i].HeaderText] = dataGridView1.Columns[i].Width.ToString();
            }

            Write(column_width, "dictionary_" + current_tab + ".bin");
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //column_width[e.Column.HeaderText.ToString()] = e.Column.Width.ToString();
            //MessageBox.Show(e.Column.HeaderText.ToString() + " " + e.Column.Width.ToString());
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Selected)
            {
                using (Pen pen = new Pen(Color.Red))
                {
                    int penWidth = 2;
                    pen.Width = penWidth;
                    int x = e.RowBounds.Left + (penWidth / 2);
                    int y = e.RowBounds.Top + (penWidth / 2);
                    int width = e.RowBounds.Width - penWidth;
                    int height = e.RowBounds.Height - penWidth;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EEE9E9");
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //dataGridView1.ClearSelection();
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           /* if (e.KeyCode == Keys.Enter)
            {
                // клавиша обработана
                // e. = true;
                MessageBox.Show("1");
            }*/
        }


        private void dgr_KeyPress_NumericTester(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) e.Handled = true;
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //e.Control.KeyPress += new KeyPressEventHandler(dgr_KeyPress_NumericTester);
            //MessageBox.Show("dataGridView1_EditingControlShowing");
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);



        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr HWND = FindWindow(null, "EyeBeam");
            ShowWindow(HWND, 0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            db_idp.HangUp();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox,.s
            if (dataGridView1.CurrentRow != null)
            {
                string phone = dataGridView1.CurrentRow.Cells["Tel"].Value.ToString();
                db_rks.CallPhone(phone);
            }
        }
    }

    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }

}

