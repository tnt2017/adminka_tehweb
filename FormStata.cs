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
    public partial class FormStata : Form
    {
        DB_RKS db_rks = new DB_RKS();
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();
        public FormStata()
        {
            InitializeComponent();
            dvg.SetDvgStyle(this);
        }


        string MakeQuery(string dt,int i)
        {
            //(SELECT DateDelivery from RKS WHERE UserName = 'dashau2' AND DateDelivery = '" + dt + "') as 'd'

            string q = "SELECT DateDelivery, " +
            "(SELECT COUNT(*)   from RKS WHERE UserName = 'dashau2' AND DateDelivery = '" + dt + "' ) as 'x'," +
            "(SELECT COUNT(*)  from RKS WHERE UserName = 'dashau2' AND DateDelivery = '" + dt + "' AND StatusName = 'Доставлен')  as 'y', " +
            "cast((SELECT COUNT(*)  from RKS WHERE UserName='dashau2' AND DateDelivery='" + dt + "' AND StatusName='Доставлен') as int)*100 / cast((SELECT COUNT(*)   from RKS WHERE UserName='dashau2' AND DateDelivery='" + dt + "' ) as int) as 'w', " +
            "SUM(TotalSumm - GoodsPrice - Bonus - Overheads - DLVPice - 75) as 'z', " +
            "(SELECT SUM(TotalSumm - GoodsPrice - Bonus - Overheads - DLVPice - 75)  from RKS WHERE UserName = 'dashau2' AND DateDelivery = '" + dt + "' AND StatusName = 'Доставлен') as 'rr' " +
            " from RKS WHERE UserName = 'dashau2' AND DateDelivery = '" + dt + "' AND StatusName='Доставлен'"; //  
            return q;
        }
        //            
        // + //  AND StatusName = 'Доставлен')"
        //

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormStata_Load(object sender, EventArgs e)
        {
            dataGridView_users.DataSource=db_idp.Get_DataTable("SELECT * from Users WHERE Name LIKE '%U2' ORDER by Name");
            //listBox1.Items.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt_full = new DataTable();
            DateTime d1 = DateTime.Parse(dateTimePicker1.Text);
            string q = "";

            for (int dd = 1; dd < 10; dd++)
            {
                DateTime d2 = d1.AddDays(-dd);
                string dt = d2.ToString("yyyy-MM-dd");

                q = "SELECT ";
                for (int i = 0; i < 35; i++) //dataGridView_users.RowCount
                {
                    string user = dataGridView_users.Rows[i].Cells[1].Value.ToString();
                    //MessageBox.Show(user);

                    if (i < 34)
                    {
                        q += "(SELECT COUNT(*)   from RKS WHERE UserName = '" + user + "' AND DateDelivery = '" + dt + "' ) as '_x'," + Environment.NewLine + //" + user + "
                             "(SELECT COUNT(*)  from RKS WHERE UserName = '" + user + "' AND DateDelivery = '" + dt + "' AND StatusName = 'Доставлен')  as '_y', " + Environment.NewLine + //" + user + "
                             "cast((SELECT COUNT(*)  from RKS WHERE UserName='" + user + "' AND DateDelivery='" + dt + "' AND StatusName='Доставлен') as int)*100 / cast((SELECT COUNT(*)   from RKS WHERE UserName='" + user + "' AND DateDelivery='" + dt + "' ) as int) as '" + user + "_z', " +
                             "SUM(TotalSumm) ," + Environment.NewLine;
                    }
                    else
                    {
                        q += "(SELECT COUNT(*)   from RKS WHERE UserName = '" + user + "' AND DateDelivery = '" + dt + "' ) as '_x'," + Environment.NewLine + //" + user + "
                             "(SELECT COUNT(*)  from RKS WHERE UserName = '" + user + "' AND DateDelivery = '" + dt + "' AND StatusName = 'Доставлен')  as '_y',  " + Environment.NewLine + //" + user + "
                             "cast((SELECT COUNT(*)  from RKS WHERE UserName='" + user + "' AND DateDelivery='" + dt + "' AND StatusName='Доставлен') as int)*100 / cast((SELECT COUNT(*)   from RKS WHERE UserName='" + user + "' AND DateDelivery='" + dt + "' ) as int) as '" + user + "_z', " +
                             "SUM(TotalSumm)" + Environment.NewLine +
                             "from RKS WHERE UserName = '" + user + "' AND DateDelivery = '" + dt + "' AND StatusName = 'Доставлен'" + Environment.NewLine;
                    }
                }


                DataTable dtbl = new DataTable();
                dtbl = db_rks.Get_DataTable(q);
                
                if (dtbl!=null)
                {
                    dt_full.Merge(dtbl);
                    dataGridView1.DataSource = dt_full;
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].Width = 70;
                    }
                    Application.DoEvents();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Columns[j].ToString().Contains("_x"))
                            {
                                dataGridView1.Columns[j].Width = 20;
                            }
                            if (dataGridView1.Columns[j].ToString().Contains("_y"))
                            {
                                dataGridView1.Columns[j].Width = 20;
                            }


                            if (dataGridView1.Columns[j].ToString().Contains("_z"))
                            {
                                string s = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                //MessageBox.Show(s);

                                if (s != "")
                                {
                                    double proc = Convert.ToDouble(s);
                                    if (proc < 50)
                                    {
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                    }
                                    if (proc > 50 && proc < 60)
                                    {
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                                    }
                                    if (proc > 60 && proc < 70)
                                    {
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Green;
                                    }
                                    if (proc >= 70)
                                    {
                                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                                    }
                                }

                                dataGridView1.Update();

                            }

                        }
                    }


                }

                //if (dd!=2)
                //q+= "UNION" + Environment.NewLine;
            }




            /* string q = @"SELECT DateDelivery, (SELECT COUNT(*)   from RKS WHERE UserName = 'dashau2' AND DateDelivery = '2017-02-14' ) as 'dashau2_x',
                     (SELECT COUNT(*)  from RKS WHERE UserName = 'dashau2' AND DateDelivery = '2017-02-14' AND StatusName = 'Доставлен')  as 'dashau2_y',  
                     SUM(TotalSumm) ,

                     (SELECT COUNT(*)   from RKS WHERE UserName = 'diulinu2' AND DateDelivery = '2017-02-14' ) as 'diulinu2_x',
                     (SELECT COUNT(*)  from RKS WHERE UserName = 'diulinu2' AND DateDelivery = '2017-02-14' AND StatusName = 'Доставлен')  as 'diulinu2_y',  
                     SUM(TotalSumm) ,                      

                     (SELECT COUNT(*)   from RKS WHERE UserName = 'dorozhkinu2' AND DateDelivery = '2017-02-14' ) as 'dorozhkinu2_x',
                     (SELECT COUNT(*)  from RKS WHERE UserName = 'dorozhkinu2' AND DateDelivery = '2017-02-14' AND StatusName = 'Доставлен')  as 'dorozhkinu2_y',  
                     SUM(TotalSumm)

                    from RKS WHERE UserName = 'dashau2' AND DateDelivery = '2017-02-14' AND StatusName = 'Доставлен'";*/


            textBox1.Text = q;
            //MessageBox.Show(q);
            //dataGridView1.DataSource = db_rks.Get_DataTable(q);

        }

    }
}
