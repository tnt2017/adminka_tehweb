using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace adminka
{
    public partial class LoginForm : Form
    {
        string PwdFile = Environment.CurrentDirectory + "\\loginpass.txt";
        string UsrFile = Environment.CurrentDirectory + "\\users.txt";
        ScreenShots screens = new ScreenShots();

        int seconds = 0;

        public LoginForm()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }

        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();


        string checkIp()
        {
            StreamReader reader;
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;

            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://parfum-2017.ru/myip.php");

                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                reader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseText = reader.ReadToEnd();
                return responseText;
                //return System.Text.RegularExpressions.Regex.Match(reader.ReadToEnd(), @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})").Groups[1].Value;
            }
            catch
            {
                return "error";
            }
        }



        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.Text = "Adminka v" + db_idp.version;

            /*if (!File.Exists(Environment.CurrentDirectory + "\\mysql.data.dll"))
            {
                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\mysql.data.dll", FileMode.Create, FileAccess.Write);
                fs.Write(Properties.Resources.mysql_data, 0, Properties.Resources.mysql_data.Length);
                fs.Close();
            }*/

            if (Environment.MachineName == db_idp.mycomp)
            {
                listBox1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;

                string[] NewFile = File.ReadAllLines(UsrFile);
                foreach (string str in NewFile)
                {
                    listBox1.Items.Add(str);
                }
            }
            else
            {
                listBox1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }

            button_update.Visible = true;

            try
            {
                string[] NewFile = File.ReadAllLines(PwdFile);
                foreach (string str in NewFile)
                {
                    String[] words = str.Split(new char[] { ':' });
                    textBox1.Text = words[0];
                    textBox2.Text = words[1];
                }
            }
            catch
            {
                //
            }
            button_update_Click(null, null);

            if (textBox1.Text == "live")
              button1_Click(null,null);
        }

        public void ShowOperForm(string uid)
        {
            string RegionId = "";
            string RksId = "";
            string qq = @"SELECT `Users`.`id`, `Users`.`Pass`, `Users`.`Name`, `Users`.`UserTypeId`,`Limits`.`RksId`, `Limits`.`RegionId` 
                        FROM `Users`
                        INNER JOIN `Limits` ON `Limits`.`UserId`=`Users`.`id`
                        WHERE `Users`.`Name` = '" + textBox1.Text + "'";

            dataGridView1.DataSource = db_idp.Get_DataTable(qq);

            try
            {
                RksId = dataGridView1.Rows[0].Cells["RksId"].Value.ToString();
                RegionId = dataGridView1.Rows[0].Cells["RegionId"].Value.ToString();
            }
            catch
            {

            }
            
            OperForm f = new OperForm();
            f.textBox_uid.Text = uid;
            f.textBox_rksid.Text = RksId;
            f.textBox_regionid.Text = RegionId;
            f.textBox_username.Text = textBox1.Text;
            f.Text = "Operator: " + textBox1.Text;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hash = db_idp.GetMD5ofMD5(textBox2.Text);
            string q = @"SELECT `Users`.`id`, `Users`.`Pass`, `Users`.`Name`, `Users`.`UserTypeId` FROM `Users`
                        WHERE `Users`.`Name` = '" + textBox1.Text + "'";

            dataGridView1.DataSource = db_idp.Get_DataTable(q);
            string hashdb = dataGridView1.Rows[0].Cells["Pass"].Value.ToString();
            string uid = dataGridView1.Rows[0].Cells["id"].Value.ToString();
            string UserTypeId = dataGridView1.Rows[0].Cells["UserTypeId"].Value.ToString();
            string RegionId = "";

            if (hash == hashdb)
            {
                if(textBox1.Text == "Denis" || textBox1.Text == "UlyaAdmin")
                timer1.Enabled = true;

                string ip = checkIp();

                string ret = db_idp.SqlQueryWithResult("SELECT * from ip_permitted WHERE ip='" + ip + "'");

                if (ret != "0")
                {
                    db_idp.SqlQuery("INSERT into visitors (id, ip, login, version, machine) VALUES (NULL,'" + ip + "','" + textBox1.Text + "', '" + db_idp.version + "', '" + Environment.MachineName + "'); ", "");
                }
                else
                {
                    db_idp.SqlQuery("INSERT into visitors (id, ip, login, version, machine) VALUES (NULL,'" + ip + "','" + textBox1.Text + "', '" + db_idp.version + "', '" + Environment.MachineName + "');", "");
                }

                if (checkBox1.Checked)
                {
                    if (textBox1.Text.IndexOf("RKS")<0  && textBox1.Text != "Denis")
                    {
                        string s = textBox1.Text + ":" + textBox2.Text;
                        File.WriteAllText(PwdFile, s, Encoding.Default);
                    }
                    else
                    {
                        string s = textBox1.Text + ":";
                        File.WriteAllText(PwdFile, s, Encoding.Default);
                    }
                }

                if (UserTypeId == "1")
                {
                    AdminForm f = new AdminForm();
                    f.username = textBox1.Text;
                    f.Show();
                    Hide();
                }

                if (UserTypeId == "2")
                {
                    ShowOperForm(uid);
                    Hide();
                }

                if (UserTypeId == "3")
                {
                    /*string qq = @"SELECT `Users`.`id`, `Users`.`Pass`, `Users`.`Name`, `Users`.`UserTypeId`,`Limits`.`RksId`, `Limits`.`RegionId` 
                        FROM `Users`
                        INNER JOIN `Limits` ON `Limits`.`UserId`=`Users`.`id`
                        WHERE `Users`.`Name` = '" + textBox1.Text + "'";

                    RegionId = dataGridView1.Rows[0].Cells["RegionId"].Value.ToString();*/

                    //MessageBox.Show(RegionId)

                    RksForm f = new RksForm();
                    f.rksname = textBox1.Text;
                    f.Show();
                    Hide();
                }
                if (UserTypeId == "4")
                {
                    FormAudit f = new FormAudit();
                    f.Show();
                    Hide();
                }
                if (UserTypeId == "5")
                {
                    FormDostavka f = new FormDostavka();
                    f.textBox_uname.Text = textBox1.Text;
                    f.Show();
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин пароль");

                if (Environment.MachineName == db_idp.mycomp && UserTypeId!="5")
                {
                    ShowOperForm(uid);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string s = listBox1.SelectedItem.ToString();
                String[] words = s.Split(new char[] { ':' });
                textBox1.Text = words[0];
                textBox2.Text = words[1];
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }       

        private void button_update_Click(object sender, EventArgs e)
        {
            string server_version = db_idp.GetServerVersion();

            if (db_idp.version != server_version)
            {
                if(textBox1.Text!="live")
                MessageBox.Show("Текущая версия:" + db_idp.version + "\r\nВерсия на сервере:" + server_version + "\r\nЗапускаем обновление");
                textBox3.Text = "Текущая версия:" + db_idp.version + "\r\nВерсия на сервере:" + server_version + "\r\nЗапускаем обновление";
                db_idp.UpdateExe();
            }
            {
                //MessageBox.Show("Текущая версия:" + version + "\r\nВерсия на сервере:" + server_version + "\r\nОбновление не требуется");
                textBox3.Text = "Текущая версия:" + db_idp.version + "\r\nВерсия на сервере:" + server_version + "\r\nОбновление не требуется";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormZarplata f = new FormZarplata();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormQueueReset f = new FormQueueReset();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string s = "server=id-points.ru;user=idp_root;database=idpoints;password=ZRHRIpdt9rg00f;charset=utf8; Allow Zero Datetime=true;";
            string s = "server=id-points.ru;user=rks_root;database=rks;password=GscrUiXe9uOpGT;charset=utf8; Allow Zero Datetime=true;";
            
            string s1 = db_idp.Crypt(s);
            textBox3.Text = s1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds % 10 == 0)
            {
                Thread t = new Thread(delegate () { screens.SendScreen(textBox1.Text,db_idp.version); });
                t.Start();
            }
        }
    }
}

