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

namespace Курсовая
{
    public partial class Form9 : Form
    {
        MySqlConnection conn;

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
        }

        public bool Insert()
        {
            bool result = false;
            string com = $"insert into sklad ( Name, Quantity, Price) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')";
            MySqlCommand comm = new MySqlCommand(com, conn);
            conn.Open();
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
            }
            finally
            {
                MessageBox.Show("новый товар успешно добавлен");
                conn.Close();
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Insert();
        }
    }
}
