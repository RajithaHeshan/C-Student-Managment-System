﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_project_1
{
    public partial class Add_New_Student : UserControl
    {
        public Add_New_Student()
        {
            InitializeComponent();
        }

        private void Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "SELECT Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif ";

            if(opf.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Student_Class student = new Student_Class();
            string fname = textfirstname.Text;
            string lname = textLastName.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textPhoneno.Text;
            string address = textAddres.Text;
            string gender = "Male";

            if(radioFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            if(((this_year-born_year ) <10 || ((this_year-born_year)>100 ))) // the student age must be between 10-100
            {
                MessageBox.Show("The student age Must be Between 10-100","Invalid Birth Date",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(verif())
            {
                pictureBox1.Image.Save(pic, pictureBox1.Image.RawFormat);

                if(student.InsertStudent(fname,lname,bdate,phone,gender,address,pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Faild", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
        //verify data
        bool verif()
        {
            if((textfirstname.Text.Trim()== "") ||
                (textLastName.Text.Trim()== "") ||
                (textPhoneno.Text.Trim()== "")||
                (textAddres.Text.Trim()== "") ||
                (pictureBox1.Image==null ))
            {
                return false; 
            }
            else
            {
                return true;
            }
        }

        private void Add_New_Student_Load(object sender, EventArgs e)
        {

        }
    }
}
