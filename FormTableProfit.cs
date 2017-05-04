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
    public partial class FormTableProfit : Form
    {
        public FormTableProfit()
        {
            InitializeComponent();
        }

        DB_RKS db_rks = new DB_RKS();


        

        private void button1_Click(object sender, EventArgs e)
        { 
            //textBox1.Text = db_rks.GetFullStatTable("11");
        }

        private void FormTableProfit_Load(object sender, EventArgs e)
        {
        }
    }
}
