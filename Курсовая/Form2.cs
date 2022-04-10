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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        private void Form2_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            toolTip1.SetToolTip(this.button2, "База данных сотрудников");
            toolTip2.SetToolTip(this.button4, "Складской учет");
            toolTip3.SetToolTip(this.button3, "База данных Клиентов");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                Form6 Form6 = new Form6();
                Form6.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
                Form3.ShowDialog();
        }

    }
}
