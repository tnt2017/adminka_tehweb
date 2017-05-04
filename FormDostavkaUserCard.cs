using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace adminka
{
    public partial class FormDostavkaUserCard : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();
        public FormDostavkaUserCard()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }
        public void SendSMS(int n)
        {
            string CommentsKS = textbox_CommentsKS.Text;
            CommentsKS = CommentsKS.Substring(0, 14);
            string smstext = db_rks.tab_sms.Rows[n][1].ToString();
            smstext = smstext.Replace("%N%", CommentsKS);
            string dt = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy");
            smstext = smstext.Replace("%DATE+3%", dt);
            textBox_smstext.Text = smstext;
        }
        
        private void textbox_CommentsKS_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SendSMS(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendSMS(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SendSMS(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendSMS(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SendSMS(4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendSMS(5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           db_rks.SendSMS(db_rks.sms_sender, textBox_phone.Text, textBox_smstext.Text);
           db_rks.InsertRKSHistory(textBox_ksid.Text, textBox_smstext.Text);
           button_gethistory_Click(null, null);
        }

        private void FormDostavkaUserCard_Load(object sender, EventArgs e)
        {
            //dateTimePicker1.Format = CustomTypeDescriptor
            button_gethistory_Click(sender,e);
        }

        private void button_gethistory_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db_rks.GetRKSHistory(textBox_ksid.Text);

            try
            {
                dataGridView1.Columns["Date"].Width = 150;
                dataGridView1.Columns["text"].Width = 400;
                dataGridView1.Columns["act"].Width = 40;
                dataGridView1.Columns["UserId"].Width = 40;
            }
            catch
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string now = dateTimePicker1.Value.Year.ToString() + "-" +
            dateTimePicker1.Value.Month.ToString() + "-" +
            dateTimePicker1.Value.Day.ToString() + " " + dateTimePicker1.Value.Hour.ToString() + ":" + dateTimePicker1.Value.Minute.ToString();
            string q= "INSERT into Notify VALUES (NULL, " + textBox_ksid.Text + ", '" + now + "', '" + richTextBox1.Text + "')";
            //MessageBox.Show(q);
            db_rks.SqlQuery(q, "Добавили заметку");
            Close();
        }
    }
}
