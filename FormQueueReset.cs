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
    public partial class FormQueueReset : Form
    {
        DB_IDP db_idp = new DB_IDP();

        public FormQueueReset()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                listBox1.SelectedIndex = i;
                Application.DoEvents();
                //button2_Click(null,null);
                Application.DoEvents();
            }
        }

        private void FormQueueReset_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = listBox1.SelectedItem.ToString();
            string[] separator = { "\t" };
            String[] words = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string userid = words[0];
            string neobr_count=db_idp.SqlQueryWithResult("SELECT COUNT(*) from Work WHERE Work.StatusId = 1 AND Work.RegionId = 20 AND UserId = " + userid);

            string q = @"SELECT COUNT(*) FROM `Work` INNER JOIN `Clients` ON `Clients`.`Id` = `Work`.`ClientId` 
                         WHERE `UserId` = " + userid + "  AND (`Work`.`StatusId`= '1') AND SUBSTRING(`Clients`.`Adr`, 1, 1) = '6' AND SUBSTRING(`Clients`.`Adr`, 2, 1) > '2'";
            
            string neobr_count_utro = db_idp.SqlQueryWithResult(q);
            
            textBox1.Text += s + "\t" + neobr_count + "\t" + neobr_count_utro + Environment.NewLine;
            
            //MessageBox.Show(neobr_count);
            //db_idp.GetRawOrders(userid, "20");
            //dataGridView1.DataSource = db_idp.GetCurrentOrder(userid, 0, "20");
            //MessageBox.Show(dataGridView1.RowCount.ToString()); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = listBox1.SelectedItem.ToString();
            string[] separator = { "\t" };
            String[] words = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            string userid = words[0];
            db_idp.GetRawOrders(userid, "20");
        }
    }
}
