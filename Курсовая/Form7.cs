using System;
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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        MySqlConnection conn;

        private void button1_Click(object sender, EventArgs e)
        {
            string com = $"INSERT INTO Sotrudnik (FIO, age, doljnost, numbers) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')";
            MySqlCommand sql = new MySqlCommand(com, conn);
            conn.Open();
            try
            { 
                sql.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка");
            }
            finally
            {
                MessageBox.Show("новый сотрудник успешно добавлен");
                conn.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_17_19;database=st_2_17_19;password=78741203";
            conn = new MySqlConnection(connStr);
        }
    }
}
