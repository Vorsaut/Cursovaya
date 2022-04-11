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
    public partial class DobanitSotrudForm7 : Form
    {
        public DobanitSotrudForm7()
        {
            InitializeComponent();
        }

        MySqlConnection conn;

        private void button1_Click(object sender, EventArgs e)
        {
            int insert = 0;
            string com = $"INSERT INTO Sotrudnik (FIO, age, doljnost, numbers) VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}')";
            MySqlCommand sql = new MySqlCommand(com, conn);
            conn.Open();
            try
            {
                insert = sql.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка");

            }
            finally
            {
                if (insert > 0)
                { 
                    MessageBox.Show("новый сотрудник успешно добавлен");
            }
                conn.Close();
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            toolTip1.SetToolTip(this.button1, "Добавить сотрудника в БД");
        }
    }
}
