using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Net;

namespace adminka
{
    public partial class FormAudit : Form
    {
        DB_IDP db_idp = new DB_IDP();

        public FormAudit()
        {
            InitializeComponent();
        }

        private void FormAudit_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db_idp.GetAudit(textBox_wid.Text, textBox_rid.Text, textBox_username.Text, textBox_status.Text, textBox_tel.Text, textBox_fio.Text);
            dataGridView1.Columns["WorkId"].Width = 50;
            dataGridView1.Columns["RegionId"].Width = 30;
            dataGridView1.Columns["Fio"].Width = 200;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            button1_Click_1(sender,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = db_idp.GetCalls(textBox_nomer.Text, textBox_nabranni.Text, textBox_cstatus.Text, textBox_flov.Text, textBox_data.Text);
            dataGridView2.Columns["flow"].Width = 30;
            dataGridView2.Columns["date"].Width = 130;
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            button2_Click(sender,e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string s = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            string url = "ftp://FOC122344:cJw1Cdr5dcs2q2d9Azx@ftp.telphin.ru/" + s + ".WAV";
            //MessageBox.Show(url);
            System.Diagnostics.Process.Start(url);

            /*WebClient client = new WebClient();
            client.DownloadFile(url, "temp.wav");
            SoundPlayer simpleSound = new SoundPlayer("temp.wav");
            simpleSound.Play();*/
        }

        private void FormAudit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
