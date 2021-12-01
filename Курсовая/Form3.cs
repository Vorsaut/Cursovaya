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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        private void Form3_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_2_17_19;database=st_2_17_19;password=78741203";
            conn = new MySqlConnection(connStr);
            GetListSotrudnik(listBox2);
        }
        public void DeletetUser()
        {
            string id = Convert.ToString(textBox6.Text);
            string sql_delete_user = $"DELETE FROM Sotrudnik WHERE id='{id}'";
            MySqlCommand delete_user = new MySqlCommand(sql_delete_user, conn);
            try
            {
                conn.Open();
                delete_user.ExecuteNonQuery();
                MessageBox.Show("Уволен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //Вывод информации о таблице
        public void GetListSotrudnik(ListBox lb)
        {
            lb.Items.Clear();
            conn.Open();
            string sql = $"SELECT * FROM Sotrudnik";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                lb.Items.Add($"id: {reader[0].ToString()} ФИО Сотрудника: {reader[1].ToString()} Возраст: {reader[2].ToString()} Должность: {reader[3].ToString()} Номер телефона: {reader[4].ToString()}");
            }
            reader.Close();
            conn.Close();
        }

        //Вывод информации о сотруднике
        private void button1_Click(object sender, EventArgs e)
        {
            string selected_id_stud = textBox1.Text;
            conn.Open();
            string sql = $"SELECT FIO, age, doljnost, numbers FROM Sotrudnik WHERE id={selected_id_stud}";
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add("ФИО сотрудника - " +reader[0].ToString());
                listBox1.Items.Add("Возраст - " +reader[1].ToString());
                listBox1.Items.Add("Должность - " +reader[2].ToString());
                listBox1.Items.Add("Номер телефона - " +reader[3].ToString());
            }
            reader.Close();
            conn.Close();
        }
        public bool InsertSotrudnik(string IFIO, int Iage, string Idoljnost, string Inumbers)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO Sotrudnik (FIO, age, doljnost, numbers) VALUES ('{IFIO}', '{Iage}', '{Idoljnost}', '{Inumbers}')";
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

        //Добавление нового сотрудника
        private void button2_Click(object sender, EventArgs e)
        {
            string FIO = textBox2.Text;
            int age = Convert.ToInt32(textBox3.Text);
            string doljnost = textBox4.Text;
            string numbers = textBox5.Text;

            if (InsertSotrudnik(FIO, age, doljnost, numbers))
            {
                GetListSotrudnik(listBox2);
            }
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeletetUser();
            GetListSotrudnik(listBox2);
        }
    }
}
