﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_project_1
{
    public partial class EditCourseForm : UserControl
    {
        public EditCourseForm()
        {
            InitializeComponent();
        }
        COURSE course = new COURSE();
        private void buttonEditcourse_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxlabel.Text;
                int hrs = (int)numericUpDown1.Value;
                string descr = textBoxDescription.Text;
                int id = (int)comboBox1.SelectedValue;

                if(name.Trim() !="")

                {
                    if (!course.checkCourseName(name, id))
                    {
                        MessageBox.Show("This Course Name Already Exists", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }

                    else if (course.updateCourse(id, name, hrs, descr))
                    {
                        MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillcombo(comboBox1.SelectedIndex);
                    }
                    else
                    {
                        MessageBox.Show("Course Not Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

                else
                {
                    MessageBox.Show("No Course Selected", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }






            }
            catch (Exception ex)
            {
                MessageBox.Show("No Course Selected", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
            



            

            

        private void EditCourseForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = course.getAllCourses();
            comboBox1.DisplayMember = "label";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedItem = null;
        }

        public void fillcombo(int index)
        {
            comboBox1.DataSource = course.getAllCourses();
            comboBox1.DisplayMember = "label";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = index;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(comboBox1.SelectedValue);
                DataTable table = new DataTable();
                table = course.getCoursesById(id);
                textBoxlabel.Text = table.Rows[0][1].ToString();
                numericUpDown1.Value = Int32.Parse(table.Rows[0][2].ToString());
                textBoxDescription.Text = table.Rows[0][3].ToString();

            }
            catch(Exception ex)
            {

            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
          //  comboBox1.SelectedValue = nul;
            textBoxlabel.Text = "";
            numericUpDown1.Value = 0;
            textBoxDescription.Text = "";

        }
    }
}
