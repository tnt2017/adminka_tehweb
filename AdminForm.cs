using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace adminka
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private DataTable Get_DataTable(string queryString, string db)
        {
            DataTable dt = new DataTable();

            MySqlConnectionStringBuilder mysqlCSB;
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "127.0.0.1";
            mysqlCSB.Database = db;
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "++++++";

            //string conStr = "server=127.0.0.1;user=root;" +
            //     "database=idpoints;password=++++++;charset=utf8";

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;

                MySqlCommand com = new MySqlCommand(queryString, con);

                try
                {
                    con.Open();

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
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }

        private DataTable Get_rks()
        {
            string queryString = @"SELECT ClientName,     
                                 UserName,
                                 Phone,
                                 Adress         
                          FROM   rks 
                          WHERE  RegionId = 1";

            return Get_DataTable(queryString, "rks");
        }

        private DataTable Get_idpoints_ZP()
        {
            // string queryString = @"SELECT Data,     
            //                      UserID,
            //                       Sum
            //               FROM   zp";

            string queryString = "SELECT * FROM Users LEFT JOIN(SELECT SUM(`Sum`) AS paid, `UserId` AS us FROM `Zp` GROUP BY `UserId`) z ON z.us = Id LEFT JOIN (SELECT SUM(`accr`) AS accr, `userid` AS usd FROM `accruals` GROUP BY `userid`) a ON a.usd = Id LEFT JOIN (SELECT COUNT(`Work`.`statusId`) AS dost, SUM(`Work`.`try_total`) AS summ, `Work`.`userId`, `Region`. `Name` AS rname,  `Valuta`.`abbr` FROM `Work` INNER JOIN  `Region` ON  `Region`.`Id` =  `Work`.`RegionId` INNER JOIN  `Valuta` ON  `Region`.`ValId` =  `Valuta`.`Id` WHERE `statusId`= 7 OR `statusId`= 11 GROUP BY `Work`.`userId`) s ON s.userId = Id ORDER BY `Name`";
            return Get_DataTable(queryString, "idpoints");
        }
        
        private DataTable Get_rks_Salary()
        {
            string queryString = @"SELECT Id,     
                                 RegionId,
                                 Date,
                                 Sum
                          FROM   salary";

            return Get_DataTable(queryString, "rks");
        }
        
        private DataTable GetTovars()
        {
            string queryString = @"SELECT Id,     
                                 Name,
                                 Price,
                                 Header,
                                 Descr
                          FROM   tovar";

            return Get_DataTable(queryString, "idpoints");
        }
        
        private DataTable GetUsers()
        {
            string queryString = @"SELECT Id,     
                                 Name
                          FROM   users";

            return Get_DataTable(queryString, "idpoints");
        }

        private DataTable GetRKS()
        {
            string queryString = @"SELECT Id,     
                                 Name
                          FROM   users WHERE UserTypeId=3";

            return Get_DataTable(queryString, "idpoints");
        }
        
        private DataTable GetLimits()///////////////////////////////////////////////////////////
        {
            /* string queryString = @"SELECT Id,     
                                  UserId,
                                  RksId, 
                                  RegionId, 
                                  Lim
                           FROM   limits";*/
                           
            string queryString=@"SELECT limits.Id, users.Name, limits.RksId, region.Name, limits.lim FROM limits
                                 LEFT OUTER JOIN users ON limits.UserId = users.Id 
                                 LEFT OUTER JOIN region ON limits.RegionId = region.id";

            return Get_DataTable(queryString, "idpoints");
        }
                
        private DataTable GetRegions()
        {
            string queryString = @"SELECT Id,     
                                 Name
                          FROM   region";

            return Get_DataTable(queryString, "idpoints");
        }

        private DataTable GetOstatki(int rid)
        {
            //string queryString="SELECT SUM(`Sklad`.`Income`) - SUM(`Sklad`.`Outcome`) AS inc,`sel` ,`rsel`, `Sklad`.`SkladTypeId`, `Tovar`.`Name`, `Tovar`.`Id` AS tid, `Sklad`.`SkladTypeId` AS sid, `tovarcomment`.`comment` AS cmt FROM `Sklad` INNER JOIN `Tovar` ON `Sklad`.`TovarId` = `Tovar`.`Id` LEFT JOIN `tovarcomment` ON (`Sklad`.`RegionId` = `tovarcomment`.`RegionId` AND `Sklad`.`TovarId` = `tovarcomment`.`TovarId`) LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS sel, `PriceLst`.`TovarId` AS stid, `Work`.`ByPost` AS ololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` WHERE `StatusId` IN (7) GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) ebana ON `ebana`.`stid` = `Tovar`.`Id` AND `ebana`.`ololo` +1 = `Sklad`.`SkladTypeId` LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS rsel, `PriceLst`.`TovarId` AS rstid, `Work`.`ByPost` AS rololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` WHERE `StatusId` IN (6) GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) rebana ON `rebana`.`rstid` = `Tovar`.`Id` AND `rebana`.`rololo` +1 = `Sklad`.`SkladTypeId`	WHERE 1 GROUP BY `Sklad`.`TovarId`,`Sklad`.`SkladTypeId`";

            string queryString = "SELECT SUM(`Sklad`.`Income`) - SUM(`Sklad`.`Outcome`) AS Vsego,`sel` ,`rsel`, `Sklad`.`SkladTypeId`, `Tovar`.`Name`, `Tovar`.`Id` AS tid, `Sklad`.`SkladTypeId` AS sid, `tovarcomment`.`comment` AS cmt FROM `Sklad` INNER JOIN `Tovar` ON `Sklad`.`TovarId` = `Tovar`.`Id` LEFT JOIN `tovarcomment` ON (`Sklad`.`RegionId` = `tovarcomment`.`RegionId` AND `Sklad`.`TovarId` = `tovarcomment`.`TovarId`) LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS sel, `PriceLst`.`TovarId` AS stid, `Work`.`ByPost` AS ololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` WHERE `StatusId` IN (7) AND `Work` .`RegionId` = " + rid + " GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) ebana ON `ebana`.`stid` = `Tovar`.`Id` AND `ebana`.`ololo` +1 = `Sklad`.`SkladTypeId` LEFT JOIN ( SELECT SUM(`PriceLst`.`Kol`) AS rsel, `PriceLst`.`TovarId` AS rstid, `Work`.`ByPost` AS rololo FROM `Work` INNER JOIN `WorkTovar` ON `Work`.`Id` = `WorkTovar`.`WorkId` INNER JOIN `PriceLst` ON `WorkTovar`.`PriceId` = `PriceLst`.`Id` WHERE `StatusId` IN (6) AND `Work` .`RegionId` = " + rid + " GROUP BY `PriceLst`.`TovarId`,`Work`.`ByPost` ) rebana ON `rebana`.`rstid` = `Tovar`.`Id` AND `rebana`.`rololo` +1 = `Sklad`.`SkladTypeId`	WHERE `Sklad`.`RegionId` = " + rid + " GROUP BY `Sklad`.`TovarId`,`Sklad`.`SkladTypeId`";

            return Get_DataTable(queryString, "idpoints");
        }

        private DataTable GetSpisaniya()
        {
            string queryString = "SELECT  `Users`.`Id` ,  `Users`.`Name`,  `Zp`.`Data` ,  `Zp`.`Sum`  FROM  `Users`  INNER JOIN  `Zp` ON  `Zp`.`UserId` =  `Users`.`Id` ORDER BY   `Zp`.`Data` DESC, `Users`.`Name`";
            return Get_DataTable(queryString, "idpoints");
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            button11_Click(sender, e);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView_zakaz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView_zakaz.DataSource = Get_rks();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = Get_idpoints_ZP();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox3.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView_tovars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridView_tovars.DataSource = GetTovars();
        }

        private void InsertData(string sql)
        {
            string conStr = "server=127.0.0.1;user=root;" +
                             "database=idpoints;password=++++++;charset=utf8";

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                try
                {
                    MessageBox.Show(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Данные добавлены!");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteData(string sql)
        {
            string conStr = "server=127.0.0.1;user=root;" +
                             "database=idpoints;password=++++++;charset=utf8";

            using (MySqlConnection con = new MySqlConnection(conStr))
            {
                try
                {
                    MessageBox.Show(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    con.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Данные удалены!");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void InsertTovar(string Name, string Header, string Price, string Descr)
        {
            string sql = "INSERT INTO tovar (Name, Header, Price, Descr, Image, Prim, Code, Category )" +
                             "VALUES ('" + Name + "', '" + Header + "', " + Price + ", '" + Descr + "', '../lib/img/tovar/Без имени-1.jpg', 0 , 0 , 0 )";
            InsertData(sql);
        }

        private void DeleteTovar(string id)
        {
            string sql = "DELETE from tovar where id='" + id + "' ";
            DeleteData(sql);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            InsertTovar(textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DeleteTovar(dataGridView_tovars.CurrentRow.Cells[0].Value.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_Enter(object sender, EventArgs e)
        {
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            button9_Click_1(sender, e);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            dataGridView_users.DataSource = GetUsers();
            dataGridView_limits.DataSource = GetLimits();

            for (int i = 0; i < dataGridView_users.Rows.Count - 1; i++)
            {
                comboBox4.Items.Add(dataGridView_users[1, i].Value.ToString());
            }

            comboBox4.SelectedIndex = 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            dataGridView4.ColumnCount = 10;

            Excel._Application excelapp = new Excel.Application();
            excelapp.Workbooks.Open(textBox3.Text);
            Excel.Worksheet activeSheet = excelapp.ActiveWorkbook.ActiveSheet;

            MessageBox.Show(activeSheet.Rows.Count.ToString());
            dataGridView4.Rows.Add(activeSheet.Rows.Count);

            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Excel.Range range = activeSheet.Cells[i + 1, j + 1];
                    dataGridView4[j, i].Value = range.Value;
                }

                dataGridView4[0, i].Value = i;
            }

            excelapp.Quit();
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            dataGridView_zp.DataSource = Get_rks_Salary();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView_regions.DataSource = GetRegions();

            for (int i = 0; i < dataGridView_regions.Rows.Count - 1; i++)
            {
                comboBox2.Items.Add(dataGridView_regions[1, i].Value.ToString());
                comboBox3.Items.Add(dataGridView_regions[1, i].Value.ToString());
                comboBox6.Items.Add(dataGridView_regions[1, i].Value.ToString());
            }


            dataGridView_rks.DataSource = GetRKS();
            for (int i = 0; i < dataGridView_rks.Rows.Count - 1; i++)
            {
                comboBox_rks.Items.Add(dataGridView_rks[1, i].Value.ToString());
            }
            comboBox_rks.SelectedIndex = 0;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //dataGridView_ostatki.SelectedRows
            // comboBox3.SelectedItem
            dataGridView_ostatki.DataSource = GetOstatki(1);
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_regions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            object index = dataGridView_regions.CurrentRow.Cells[0].Value;
            MessageBox.Show("region=" + index.ToString());
            //dataGridView_ostatki.DataSource = GetOstatki(index);
            //dataGridView_regions.
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            dataGridView_spisaniya.DataSource = GetSpisaniya();

        }

        private void tabPage8_Enter(object sender, EventArgs e)
        {
            button13_Click_1(sender, e);
        }

        private DataTable GetComments()
        {
            string queryString = "SELECT DISTINCT(`Sklad`.`TovarId`) AS tid, `Sklad`.`RegionId`, `Tovar`.`Name` AS tin, `Region`.`Name` AS rin,`comment` FROM `Sklad` INNER JOIN `Tovar` ON  `Sklad`.`TovarId` = `Tovar`.`Id` INNER JOIN `Region` ON  `Sklad`.`RegionId` = `Region`.`Id` LEFT JOIN `tovarcomment` ON(`Sklad`.`RegionId` = `tovarcomment`.`RegionId` AND `Sklad`.`TovarId` = `tovarcomment`.`TovarId`) WHERE `Sklad`.`RegionId` IN(SELECT DISTINCT(`Sklad`.`RegionId`) FROM `Sklad`) ORDER BY `Sklad`.`RegionId`";
            return Get_DataTable(queryString, "idpoints");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView_comments.DataSource = GetComments();
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void InsertUser(string Name, string Pass)
        {
            string sql = "INSERT INTO users VALUES ('0','name','1','pass',0,0,0,0,0,0,0,0,0,0)";
            InsertData(sql);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            InsertUser(textBox1.Text, textBox2.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }
    }
}
