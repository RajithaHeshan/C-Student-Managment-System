﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace My_project_1
{
    class Student_Class
    {
        // add a new student to the database

        Class1 db = new Class1();

        public bool InsertStudent(string fname,string lname ,DateTime bdate,string Phone,string Gender,string address,MemoryStream Picture)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `add_student`(`First_Name`, `Last_Name`, `Birth_date`, `Gender`, `Phone`, `Address`, `Picture`) VALUES(@fn,@ln,@bdt,@gdr,@Phn,@adrs,@pic)" ,db.getConnection);

            cmd.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            cmd.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            cmd.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            cmd.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = Gender;
            cmd.Parameters.Add("@phn", MySqlDbType.VarChar).Value = Phone;
            cmd.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            cmd.Parameters.Add("@pic", MySqlDbType.Blob).Value = Picture.ToArray();

            db.openConnection();

            if(cmd.ExecuteNonQuery()==1)
            {
                db.closeconnection();
                return true;
            }
            else
            {
                db.closeconnection();
                return false;
            }
            

        }
        public DataTable getStudents(MySqlCommand cmd)
        {
            cmd.Connection = db.getConnection;
            MySqlDataAdapter ad = new MySqlDataAdapter(cmd);
            DataTable da = new DataTable();
            ad.Fill(da);
              //return table student data
            return da;

        }

        
        public bool updateStudent(int id,string fname, string lname, DateTime bdate, string Phone, string Gender, string address, MemoryStream Picture)
        {
           
            MySqlCommand cmd = new MySqlCommand("UPDATE `add_student` SET `First_Name`=@fn,`Last_Name`=@ln,`Birth_date`=@bdt,`Gender`=@gdr,`Phone`=@Phn,`Address`=@adrs,`Picture`=@pic WHERE `id`=@ID", db.getConnection);

            cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            cmd.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            cmd.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            cmd.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            cmd.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = Gender;
            cmd.Parameters.Add("@phn", MySqlDbType.VarChar).Value = Phone;                           //update student information
            cmd.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            cmd.Parameters.Add("@pic", MySqlDbType.Blob).Value = Picture.ToArray();
            // cmd.ExecuteNonQuery();

            db.openConnection();

              if (cmd.ExecuteNonQuery()==1)
             {
            db.closeconnection();
                return true;
           }
           else
            {
                db.closeconnection();
                return false;
            }

        }

        
        public bool deleteStudent(int id)    //delete the selected student 
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `add_student` WHERE `id`=@studentID", db.getConnection);

            cmd.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id;
            

            db.openConnection();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeconnection();
                return true;
            }
            else
            {
                db.closeconnection();
                return false;
            }
        }

       
        public string execCount(string query) // excute count queries
        {
            MySqlCommand cmd = new MySqlCommand(query, db.getConnection);
            db.openConnection();
            string count = cmd.ExecuteScalar().ToString();
            db.closeconnection();

            return count;
        }

        public string totalstudent()  // get total Student
        {
            return execCount("SELECT COUNT(*) FROM `add_student`");  
        }

        public string totalmalestudent()  // get maletotal Student
        {
            return execCount("SELECT COUNT(*) FROM `add_student` WHERE `Gender`='Male'");
        }

        public string totalFemalestudent()  // get Fmaletotal Student
        {
            return execCount("SELECT COUNT(*) FROM `add_student` WHERE `Gender`='Female'");
        }
    }
}
