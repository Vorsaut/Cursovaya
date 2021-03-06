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
    public partial class ClientForm5 : Form
    {
        public ClientForm5()
        {
            InitializeComponent();
        }
        MySqlConnection conn;

        private void Form5_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            GetListClient(listBox1);
            comboup();
        }
        public void ochishaet(ListBox lb)
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
            listBox1.Items.Clear();
          
            string sql = $"SELECT FIO, Age, id_tarif, numbers FROM Client WHERE id_tarif={selected_id_tarif}";
            try
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add("ФИО - " + reader[0].ToString());
                    listBox1.Items.Add("Возраст - " + reader[1].ToString());
                    listBox1.Items.Add("Тариф - " + reader[2].ToString());
                    listBox1.Items.Add("Номер телефона - " + reader[3].ToString());
                    listBox1.Items.Add("---------------------------------------------------");
                }
                reader.Close();
                conn.Close();
            }
            catch(Exception osh)
            {
                MessageBox.Show("Условия не выполнены");
                conn.Close();
            }
            
        }
        public bool InsertTarif(string IFIO, int Iage, string Itarif, string Inumbers)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO Client (FIO, Age, id_tarif, numbers ) VALUES ('{IFIO}', '{Iage}', '{Itarif}', '{Inumbers}')";
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

        private void button4_Click(object sender, EventArgs e)
        {
            ochishaet(listBox1);
            GetListClient(listBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FIO = textBox2.Text;
            int Age = Convert.ToInt32(textBox3.Text);
            string id_tarif = textBox4.Text;
            string numbers = textBox5.Text;

            if (InsertTarif(FIO, Age, id_tarif, numbers))
            {
                GetListClient(listBox1);
            }
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
            obnova();
        }

        public void DeletetUser()
        {
            string id = Convert.ToString(comboBox1.Text);
            string sql_delete_user = $"DELETE FROM Client WHERE id='{id}'";
            MySqlCommand delete_user = new MySqlCommand(sql_delete_user, conn);
            try
            {
                conn.Open();
                delete_user.ExecuteNonQuery();
                MessageBox.Show("Удален", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невозможно уволить \n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeletetUser();
            obnova();
        }

        public void comboup()
        {
            string com = $"SELECT id from Client";
            conn.Open();
            MySqlCommand sql = new MySqlCommand(com, conn);
            MySqlDataReader reader = sql.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
        }
        public void obnova()
        {
            ochishaet(listBox1);
            GetListClient(listBox1);
        }
    }
}
