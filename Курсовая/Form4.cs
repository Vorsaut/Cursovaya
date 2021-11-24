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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        private void Form4_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_17_19;database=st_2_17_19;password=78741203";
            conn = new MySqlConnection(connStr);
            GetListsklad(listBox1);
        }
        public void GetListsklad(ListBox lb)
        {
            lb.Items.Clear();
            conn.Open();
            string sql = $"SELECT * FROM sklad";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lb.Items.Add($"id: {reader[0].ToString()} Название {reader[1].ToString()} Количество: {reader[2].ToString()} Стоимость: {reader[3].ToString()}");
            }
            reader.Close();
            conn.Close();
        }
        public bool Insertsklad(int id, string Name, string Quantity, string Price)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO sklad (id, Name, Quantity, Price) VALUES ('{id}', '{Name}', '{Quantity}', '{Price}')";
            try
            {
                MySqlCommand command = new MySqlCommand(query, conn);
                InsertCount = command.ExecuteNonQuery();
            }
            catch
            {
                InsertCount = 0;
            }
            finally
            {
                conn.Close();
                if (InsertCount != 0)
                {
                    result = true;
                }
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string redact_id = textBox1.Text;
            string new_fio = Convert.ToString(textBox2.Text);
            conn.Open();
            string abdul = $"select Quantity WHERE id = {redact_id}";
            string query2 = $"UPDATE sklad SET Quantity = '{new_fio}' WHERE id = {redact_id}";
            MySqlCommand command = new MySqlCommand(query2, conn);
            command.ExecuteNonQuery();
            conn.Close();
            GetListsklad(listBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Name = textBox3.Text;
            string Quantity = textBox4.Text;
            string Price = textBox5.Text;
            int id = Convert.ToInt32(textBox6.Text);

            if (Insertsklad(id, Name, Quantity, Price))
            {
                GetListsklad(listBox1);
            }
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
        }
    }
}
