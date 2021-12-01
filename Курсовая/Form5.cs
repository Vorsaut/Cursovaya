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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        private void Form5_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_17_19;database=st_2_17_19;password=78741203";
            conn = new MySqlConnection(connStr);
            GetListClient(listBox1);
        }
        public void Getaue(ListBox lb)
        {
            lb.Items.Clear();
        }
        public void GetListClient(ListBox lb)
        {
            lb.Items.Clear();
            conn.Open();
            string sql = $"SELECT * FROM Client";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lb.Items.Add($"id: {reader[0].ToString()} ФИО: {reader[1].ToString()} Возвраст: {reader[2].ToString()} Тариф: {reader[3].ToString()} Телефон: {reader[4].ToString()}");
            }
            reader.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected_id_tarif = textBox1.Text;
            conn.Open();
          
            string sql = $"SELECT FIO, Age, id_tarif, numbers FROM Client WHERE id_tarif={selected_id_tarif}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
           
            while (reader.Read())
            {
                listBox1.Items.Add("ФИО - " + reader[0].ToString());
                listBox1.Items.Add("Возраст - " + reader[1].ToString());
                listBox1.Items.Add("Тариф - " + reader[2].ToString());
                listBox1.Items.Add("Номер телефона - " + reader[3].ToString()); 
                
            }
            reader.Close();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Getaue(listBox1);
        }
    }
}
