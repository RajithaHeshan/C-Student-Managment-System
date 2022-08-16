﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace My_project_1
{
    public partial class Login_form : Form
    {
        public Login_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 db = new Class1();
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable table = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `user` WHERE `Username`=@usn AND `Password`=@pass", db.getConnection);

            cmd.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textusername.Text;
            cmd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textPassword.Text;

            da.SelectCommand = cmd;
            da.Fill(table);
            if(table.Rows.Count>0)
            {

                MainForm a = new MainForm();
                a.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username Or Password","Login Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Login_form_Load(object sender, EventArgs e)
        {
           // pictureBox1.Image = Image.FromFile("../../images/user.png");  // set to image in to pictuer box
        }
    }
}
