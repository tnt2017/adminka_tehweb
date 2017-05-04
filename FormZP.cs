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
    public partial class FormZP : Form
    {
        DB_IDP db_idp = new DB_IDP();
        DVG dvg = new DVG();

        public FormZP()
        {
            InitializeComponent();
        }

        private void FormZP_Load(object sender, EventArgs e)
        {
            comboBox_region.SelectedIndex = 4;
            dvg.SetDvgStyle(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView_zp1.DataSource = db_idp.Get_idpoints_ZP(comboBox_region.SelectedItem.ToString(), checkBox_ShowBenuk.Checked);

            /*dataGridView_zp1.Columns[0].HeaderText = "ID";
            dataGridView_zp1.Columns[1].HeaderText = "Пользователь";
            dataGridView_zp1.Columns[2].HeaderText = "Дост";
            dataGridView_zp1.Columns[3].HeaderText = "На сумму";
            dataGridView_zp1.Columns[4].HeaderText = "Ср. чек";
            dataGridView_zp1.Columns[5].HeaderText = "Начислено";
            dataGridView_zp1.Columns[6].HeaderText = "Оплачено";
            dataGridView_zp1.Columns[7].HeaderText = "Остаток";
            dataGridView_zp1.Columns[8].HeaderText = "*";*/

            dataGridView_zp1.Columns["ID"].Width = 40;
            dataGridView_zp1.Columns["Дост."].Width = 50;
            dataGridView_zp1.Columns["valuta"].Width = 40;
            dataGridView_zp1.Columns["Ср. чек"].Width = 80;

            dataGridView_zp1.Sort(dataGridView_zp1.Columns[1], ListSortDirection.Ascending);

            double summ = 0;
            for (int i = 0; i < dataGridView_zp1.RowCount; i++)
            {
                if (dataGridView_zp1.Rows[i].Cells["Остаток"].Value != null && dataGridView_zp1.Rows[i].Cells["Остаток"].Value != "")
                {
                    try
                    {
                        string s = dataGridView_zp1.Rows[i].Cells["Остаток"].Value.ToString();
                        summ += Convert.ToDouble(s);
                        //MessageBox.Show(s);
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }

            label1.Text = "Кол-во операторов: " + dataGridView_zp1.RowCount.ToString();
            label2.Text = "Итого к выплате: " + summ.ToString();
        }
    }
}
