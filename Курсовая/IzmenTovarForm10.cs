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
    public partial class IzmenTovarForm10 : Form
    {
        MySqlConnection conn;

        public IzmenTovarForm10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            comboadd();
            toolTip1.SetToolTip(this.button1, "Изменить значение товара");
        }

        public void comboadd()
        {
            string com = "SELECT id FROM sklad";
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
            string com = $"SELECT * from sklad WHERE id={comboBox1.Text}";
            conn.Open();
            MySqlCommand sql = new MySqlCommand(com, conn);
            MySqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                label3.Text = "Название продукта - " + reader[1].ToString();
                textBox1.Text = reader[2].ToString();
                textBox2.Text = reader[3].ToString();
            }
            reader.Close();
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = $"UPDATE sklad SET Quantity='{textBox1.Text}', Price='{textBox2.Text}' WHERE id={comboBox1.Text}";
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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboup();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
