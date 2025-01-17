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
    public partial class ManageCourseForm : UserControl
    {
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();
        int pos;
        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        public void reloadListBoxData()
        {
            listBoxCourses.DataSource = course.getAllCourses();
            listBoxCourses.ValueMember = "id";
            listBoxCourses.DisplayMember = "label";

            listBoxCourses.SelectedItem = null;

            labeltotalCourse.Text = "Total Courses : " + course.totalCourse();
        }

        public void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];

            textBoxid.Text = dr.ItemArray[0].ToString();
            textBoxlabel.Text = dr.ItemArray[1].ToString();
            numericUpDown1.Value = int.Parse(dr.ItemArray[2].ToString());
            textBoxDescription.Text = dr.ItemArray[3].ToString();


        }
        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            pos = listBoxCourses.SelectedIndex;
            showData(pos);
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxlabel.Text;
                int hrs = (int)numericUpDown1.Value;
                string descr = textBoxDescription.Text;
                int id = Convert.ToInt32(textBoxid.Text);
                

                if (name.Trim() != "")

                {
                    if (!course.checkCourseName(name, id))
                    {
                        MessageBox.Show("This Course Name Already Exists", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }

                    else if (course.updateCourse(id, name, hrs, descr))
                    {
                        MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBoxData();
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

        private void buttonAdd_Click(object sender, EventArgs e)
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
                        reloadListBoxData();
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
            catch (Exception ex)
            {

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxid.Text);



                if (MessageBox.Show("Are You Sure You Want To Remove This Course", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBoxData();

                        textBoxid.Text = "";
                        numericUpDown1.Value = 0;
                        textBoxlabel.Text = "";
                        textBoxDescription.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Course Not Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }


            }

            catch
            {
                MessageBox.Show("Enter A Valid Numeric ID ", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ///textBoxCourseid.Text = "";
            }
        }
    }
    }


