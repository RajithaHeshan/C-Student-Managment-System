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
    public partial class Update_delet_student_Form : UserControl
    {
        public Update_delet_student_Form()
        {
            InitializeComponent();
        }

        Student_Class student = new Student_Class();

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "SELECT Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif ";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }


        }


        bool verif()
        {
            if ((textfirstname.Text.Trim() == "") ||
                (textLastName.Text.Trim() == "") ||
                (textPhoneno.Text.Trim() == "") ||
                (textAddres.Text.Trim() == "") ||
                (pictureBox1.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Update_delet_student_Form_Load(object sender, EventArgs e)
        {

        }

        private void buttonedit_Click(object sender, EventArgs e)
        {
            
            try
            {
                int id = Convert.ToInt32(textBoxid.Text);
                string fname = textfirstname.Text;
                string lname = textLastName.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textPhoneno.Text;
                string address = textAddres.Text;
                string gender = "Male";

                if (radioFemale.Checked)
                {
                    gender = "Female";
                }

                MemoryStream pic = new MemoryStream();

                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10 || ((this_year - born_year) > 100))) // the student age must be between 10-100
                {
                    MessageBox.Show("The student age Must be Between 10-100", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif())
                {
                    pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);

                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student Infromation Updated", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Empty Faild", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Enter a Valid Student Id ", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxid.Text);
                if (MessageBox.Show("Are You Sure you Want To Delete this Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textBoxid.Text = "";
                        textfirstname.Text = "";
                        textLastName.Text = "";
                        textPhoneno.Text = "";
                        textAddres.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Student Not Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Enter a Valid Student Id ", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxid.Text);
                MySqlCommand cmd = new MySqlCommand("SELECT `id`, `First_Name`, `Last_Name`, `Birth_date`, `Gender`, `Phone`, `Address`, `Picture` FROM `add_student` WHERE `id`=" + id);

                DataTable table = student.getStudents(cmd);

                if (table.Rows.Count > 0)
                {
                    textfirstname.Text = table.Rows[0]["First_Name"].ToString();
                    textLastName.Text = table.Rows[0]["Last_Name"].ToString();
                    textPhoneno.Text = table.Rows[0]["Phone"].ToString();
                    textAddres.Text = table.Rows[0]["Address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["Birth_date"];

                    if (table.Rows[0]["Gender"].ToString() == "Female")
                    {
                        radioFemale.Checked = true;
                    }
                    else
                    {
                        radiomale.Checked = true;
                    }

                    byte[] pic = (byte[])table.Rows[0]["Picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBox1.Image = Image.FromStream(picture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a Valid Student Id ", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxid_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBoxid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
    
}
