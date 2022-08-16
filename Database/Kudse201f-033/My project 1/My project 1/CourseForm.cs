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
    public partial class CourseForm : UserControl
    {
        public CourseForm()
        {
            InitializeComponent();
        }

        private void buttonaddcourse_Click(object sender, EventArgs e)
        {
            Add_CourseForm ac = new Add_CourseForm();
            showcontrol(ac);

        }

        public void showcontrol(Control control)
        {
            panel1.Controls.Clear();

            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();

            panel1.Controls.Add(control);
        }

        private void buttonEditCourse_Click(object sender, EventArgs e)
        {
            EditCourseForm ecf = new EditCourseForm();
            showcontrol(ecf);
        }

        private void buttonManageCourses_Click(object sender, EventArgs e)
        {
            ManageCourseForm Mcf = new ManageCourseForm();
            showcontrol(Mcf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Remove_Course Rc = new Remove_Course();
            showcontrol(Rc);
        }
    }
}
            
    

