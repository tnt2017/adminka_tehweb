using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace adminka
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        DB db = new DB();

        static string GetMd5Hash(MD5 md5Hash, string input)
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

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
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


        private void LoginForm_Load(object sender, EventArgs e)
        { 
            string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, textBox2.Text);
            hash = GetMd5Hash(md5Hash, hash);
            ///MessageBox.Show(hash);

            dataGridView1.DataSource=db.Get_DataTable("SELECT * from Users WHERE Name='" + textBox1.Text + "' ", "idpoints");
            string hashdb=dataGridView1.Rows[0].Cells["Pass"].Value.ToString();
            string uid = dataGridView1.Rows[0].Cells["id"].Value.ToString();
            string UserTypeId = dataGridView1.Rows[0].Cells["UserTypeId"].Value.ToString();

            //MessageBox.Show(hashdb);

            if (hash == hashdb)
            {
                MessageBox.Show("Залогинились");

                if (UserTypeId == "1")
                {
                    AdminForm f = new AdminForm();
                    //f.textBox_uid.Text = uid;
                    f.Show();
                    Hide();
                }

                if (UserTypeId == "2")
                {
                    OperForm f = new OperForm();
                    f.textBox_uid.Text = uid;
                    f.Show();
                    Hide();
                }

                if (UserTypeId == "3")
                {
                    MessageBox.Show("RKS");
                    RksForm f = new RksForm();
                    f.Show();
                    Hide();
                }
            }
            else
            {
                MessageBox.Show("Неверный логин пароль");
            }
        }
    }
}

