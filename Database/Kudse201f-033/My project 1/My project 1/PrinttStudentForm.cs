﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace My_project_1
{
    public partial class PrinttStudentForm : UserControl
    {
        public PrinttStudentForm()
        {
            InitializeComponent();
        }
        Student_Class student = new Student_Class();
        private void PrinttStudentForm_Load(object sender, EventArgs e)
        {
            fillGrid(new MySqlCommand("SELECT * FROM `add_student`"));

            if(radioButtonno.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        public void fillGrid(MySqlCommand cmd)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(cmd);

            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;


        }

        private void radioButtonno_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonyes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            string query;

            if(radioButtonyes.Checked)
            {
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                
                if(radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `add_student` WHERE `Birth_date`BETWEEN '"+date1+"' AND '"+date2+"' AND Gender='Male'";
                }
                else if(radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `add_student` WHERE `Birth_date`BETWEEN '" + date1 + "' AND '" + date2 + "' AND Gender='Female'";
                }
                else
                {
                    query = "SELECT * FROM `add_student` WHERE `Birth_date`BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }
                cmd = new MySqlCommand(query);
                fillGrid(cmd);

            }
            else // display the data without a birthday range
            {
                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `add_student` WHERE Gender='Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `add_student` WHERE Gender='Female'";
                }
                else
                {
                    query = "SELECT * FROM `add_student` ";
                }

                cmd = new MySqlCommand(query);
                fillGrid(cmd);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Student_list.txt";

            using (var writer = new StreamWriter(path))
            {
                if(!File.Exists(path))
                {
                    File.Create(path);
                }
                DateTime bdate;

                for (int i=0;i<dataGridView1.Rows.Count; i++)
                {
                    for(int j=0;j<dataGridView1.Columns.Count-1; j++)
                    {
                        if(j==3)
                        {
                            bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            writer.Write("\t" +bdate.ToString("yyyy-MM-dd") + "\t" + "|");
                        }

                        else if(j==dataGridView1.Columns.Count -2)
                         {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        else
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                        }

                        writer.WriteLine(""); // Make New Line
                    }

                  
                }
                writer.Close();
                MessageBox.Show("Data Exported","Print",MessageBoxButtons.OK,MessageBoxIcon.None);
            }
        }
    }
}
