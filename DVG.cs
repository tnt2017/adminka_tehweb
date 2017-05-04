using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace adminka
{
    class DVG
    {
        public void SetDvgStyle(TabControl tabControl1)
        {
            foreach (Control tabPage in tabControl1.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    //Перебираем все контролы на вкладке
                    if (control is DataGridView)
                    {
                        DataGridView dgv = (DataGridView)control;
                        dgv.AllowUserToAddRows = false;
                        dgv.MultiSelect = false;
                    }
                }
            }
        }

        public void SetDvgStyle(Form Form1)
        {
            foreach (Control control in Form1.Controls)
            {
                //Перебираем все контролы на вкладке
                if (control is DataGridView)
                {
                    DataGridView dgv = (DataGridView)control;
                    dgv.AllowUserToAddRows = false;
                    dgv.MultiSelect = false;
                }
            }
        }


        public void AddImgColumn(DataGridView dgv, Image image, string HeaderText)
        {
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.HeaderText = HeaderText;
            img.Image = image;
            dgv.Columns.Add(img);
        }

        public void DeleteImageColumns(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                if ((dgv.Columns[i] is DataGridViewImageColumn))
                {
                    dgv.Columns.Remove(dgv.Columns[i]);
                    i--;
                }
            }
        }
    }
}
