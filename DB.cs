using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;


namespace adminka
{
    public class DB
    {
        public string sitename = "rintop.ru"; //"xxlcentury.ru";
        public string sms_sender = "CLIENTINFO"; //"xxlcentury.ru";
       // public string sms_login = "idbonuskz";
       // public string sms_pass = "FZ9Zabk8Z"; //"15061506z";

        public string ConnectionString;
        public MySqlConnection con;

        private void WriteLog(string s)
        {
           string fname=Environment.CurrentDirectory + "\\debug.txt";
           string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
           //File.AppendAllText(fname, dt + " - " + s + "\r\n\r\n", Encoding.UTF8);
        }


        public string Crypt(string s)
        {
            string s2 = "";

            for (int i = 0; i < s.Length; i++)
            {
                s2 += (s[i] + 0).ToString() + " ";
            }
            return s2;
        }

        public string Decrypt(string s2)
        {
            string[] split = s2.Split(new Char[] { ' ' });
            char[] chars = new char[300];

            for (int i = 0; i < split.Count() - 1; i++)
            {
                int c = Convert.ToInt32(split[i].ToString());
                chars[i] = (char)c;
            }

            string s3 = new string(chars);
            return s3;
        }

        public DataTable Get_DataTable(string queryString)
        {
            WriteLog(queryString);
            DataTable dt = new DataTable();

            if (con.State != ConnectionState.Open)
            {
                MessageBox.Show("Сейчас пропадают цены. Может инет отвалился. Пробуем переподключиться");
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                MySqlCommand com = new MySqlCommand(queryString, con);

                using (MySqlDataReader dr = com.ExecuteReader())
                {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                }
            }

            catch (Exception ex)
            {
                    WriteLog(ex.Message);
                    return null;
            }
            return dt;
        }


        public DataTable GetDataTable2(string q)
        {
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection con1 = new MySqlConnection(ConnectionString))
                {
                    MySqlCommand com = new MySqlCommand(q, con1);

                    con1.Open();

                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                return null;
            }


            return dt;
        }
        public void SqlQuery(string sql, string message)
        {
            WriteLog(sql);
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    //MessageBox.Show(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (message!="")
                    MessageBox.Show(message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void mysql_query(string s)
        {
            SqlQuery(s, ""); 
        }
        
        public string SqlQueryWithResult(string sql)
        {
            DataTable dt = Get_DataTable(sql);
            if (dt == null)
                return "0";
            else
            {
                if (dt.Rows.Count != 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "0";
            }
        }


        public DataRowCollection SqlQueryWithResultRow(string sql)
        {
            DataTable dt = Get_DataTable(sql);
            if (dt == null)
                return null;
            else
            {
                if (dt.Rows.Count != 0)
                    return dt.Rows;
                else
                    return null;
            }
        }


        public string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetMD5ofMD5(string s)
        {
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, s);
            hash = GetMd5Hash(md5Hash, hash);
            return hash;
        }

        public string LongToIPAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
        }       
        
        public void SendSMS(string sender, string phone, string message)
        {
            SMSC smsc = new SMSC();
            string[] r = smsc.send_sms(phone, message, 0);
        }


        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);


        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public void CallPhone(string s)
        {
            if (s.Length > 1)
            {

                if(s.Length==10)
                    s = "8" + s;
                else
                    s = "8" + s.Remove(0, 1);

                //MessageBox.Show(s);
                //WebBrowser webBrowser1 = new WebBrowser();
                //webBrowser1.Navigate("sip:" + s);


                System.Diagnostics.ProcessStartInfo start =
                new System.Diagnostics.ProcessStartInfo();
                start.FileName = "sip:" + s; //"sip:" + s;

                start.CreateNoWindow = true;
                start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process.Start(start);                               

            }
            else
            {
                MessageBox.Show("Неверный телефон");
            }

        }

        public void HangUp()
        {
            //IntPtr HWND1 = FindWindow(null, "EyeBeam");
            //SendMessage(HWND1, WM_CLOSE, 0, 0);

            List<string> name = new List<string> { "eyebeam", "Eyebeam" };//процесс, который нужно убить
            System.Diagnostics.Process[] etc = System.Diagnostics.Process.GetProcesses();//получим процессы
            foreach (System.Diagnostics.Process anti in etc)//обойдем каждый процесс
            {
                try
                {
                    foreach (string s in name)
                    {
                        if (anti.ProcessName.ToLower().Contains(s.ToLower())) //найдем нужный и убьем
                        {
                            //anti.CloseMainWindow();
                            //anti.WaitForExit();
                            //anti.Close();
                            anti.Kill();
                            name.Remove(s);
                        }
                    }
                }
                catch
                {

                }
            }
        }



    }
}
