using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adminka
{
    public partial class FormRksStatus : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        public string RegionId;
        public string rksname;

        public string tid1, tid2, tid3, tid4, tid5;

        public FormRksStatus()
        {
            InitializeComponent();
        }

        private void FormRksStatus_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            db_idp.CallPhone(textBox_phone.Text);
        }

        private void UpdateOrder(string StatusId, string StatusName)
        {
            string q = "UPDATE RKS SET ";
            if (StatusId != "" && StatusName != "")
                q += " StatusId = '" + StatusId + "', StatusName = '" + StatusName + "', ";

            q += " ClientName='" + textBox_fio.Text + "', Adress='" + textBox2.Text + "', CommentsIM='" + textBox3.Text + "', Phone='" + textBox_phone.Text + "', CommentsKs='" + textBox_tracking.Text + "' WHERE WorkId=" + textBox_workid.Text;
            //MessageBox.Show(q);
            db_rks.SqlQuery(q, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateOrder("6","Одобрен");
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateOrder("5", "Отказ MGR");

            if (tid1 != "0")
                db_idp.VozvratTovar(rksname, tid1);
            if (tid2 != "0")
                db_idp.VozvratTovar(rksname, tid2);
            if (tid3 != "0")
                db_idp.VozvratTovar(rksname, tid3);
            if (tid4 != "0")
                db_idp.VozvratTovar(rksname, tid4);
            if (tid5 != "0")
                db_idp.VozvratTovar(rksname, tid5);
            Close();
        } 

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateOrder("19", "Прибыл");
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateOrder("20", "Выкуплен");
            Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string id=db_rks.SqlQueryWithResult("SELECT Id from RKS WHERE CommentsKs=" + textBox_tracking.Text + " AND WorkId!=" + textBox_workid.Text);

            if (id != "" && id!="0")
            {
                MessageBox.Show("ШПИ уже присвоен другому заказу. Номер заказа в РКС=" + id);
                return;
            }

            if (textBox_tracking.Text.Length == 14)
            {            
                UpdateOrder("18", "Отправлен");
                db_rks.SendToLiveinform(db_rks.api_id2, textBox_workid.Text, textBox_phone.Text, textBox_fio.Text, textBox_tracking.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Неверный ШПИ");
                textBox_tracking.Focus();
            }
        }

        private void FormRksStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///e.Cancel = true;

            //DialogResult dialogResult = MessageBox.Show("Сохранить изменения?", "", MessageBoxButtons.YesNo);
            //if (dialogResult == DialogResult.Yes)
            //{
               // UpdateOrder("","");
            //}
            //else
            //{
                //Close();
            //}
        }
    }
}
