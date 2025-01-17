﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace My_project_1
{
    public partial class Add_CourseForm : UserControl
    {
        public Add_CourseForm()
        {
            InitializeComponent();
        }
        COURSE course = new COURSE();
        private void buttonaddcourse_Click(object sender, EventArgs e)
        {
            try
            {
                string courseLabel = textBoxlabel.Text;
                int hours = (int)numericUpDown1.Value;
                string description = textBoxDescription.Text;



                if (courseLabel.Trim() == "")
                {
                    MessageBox.Show("Add a Course Name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (course.checkCourseName(courseLabel))
                {
                    if (course.insertCourse(courseLabel, hours, description))
                    {
                        MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                
            }
            catch(Exception ex)
            {

            }
           

           
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxlabel.Text = "";
            textBoxDescription.Text = "";
            numericUpDown1.Value = 0;



        }
    }
    }

