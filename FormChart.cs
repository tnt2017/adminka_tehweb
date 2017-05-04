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
    public partial class FormChart : Form
    {
        DB_RKS db_rks = new DB_RKS();

        public FormChart()
        {
            InitializeComponent();
        }

        int[] masX = new int[32];
        int[] masY = new int[32];

        private void FormChart_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 32; i++)
            {
                string dt = "2017-01-" + i.ToString();
                string s = db_rks.SqlQueryWithResult("SELECT COUNT(*) from RKS WHERE DateDelivery='" + dt + "'");
                masX[i] = i;
                masY[i] = Convert.ToInt32(s);
                textBox1.Text += dt + ":" + s + Environment.NewLine;
                chart1.Series["Series1"].Points.DataBindXY(masX, masY);
                Application.DoEvents();
            }
        }
    }
}
