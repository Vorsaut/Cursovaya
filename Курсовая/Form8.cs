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
    public partial class Form8 : Form
    {
        MySqlConnection conn;

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            comboadd();
        }

        public void comboadd()
        {
            string com = "SELECT id FROM Sotrudnik";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(com, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            reader.Close();
            conn.Close();
        }

        public void comboup()
        {
            string com = $"SELECT * from Sotrudnik WHERE id={comboBox1.Text}";
            conn.Open();
            MySqlCommand sql = new MySqlCommand(com, conn);
            MySqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();
                textBox3.Text = reader[3].ToString();
                textBox4.Text = reader[4].ToString();
            }
            reader.Close();
            conn.Close();
        }
        
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE Sotrudnik SET FIO='{textBox1.Text}', age='{textBox2.Text}', doljnost='{textBox3.Text}', numbers='{textBox4.Text}' WHERE id={comboBox1.Text}";
            conn.Open();
            MySqlCommand com = new MySqlCommand(sql, conn);
            try
            {
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка");
            }
            finally
            {
                MessageBox.Show("запись успешно отредактирована");
                conn.Close();
            }
        }
    }
}
