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
    public partial class Avg : UserControl
    {
        public Avg()
        {
            InitializeComponent();
        }

        private void Avg_Load(object sender, EventArgs e)
        {
            Result_Class result = new Result_Class();
            dataGridView1.DataSource = result.avgResultByCourse();
        }
    }
}
