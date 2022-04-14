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
    public partial class PriceListForm4 : Form
    {
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        public PriceListForm4()
        {
            InitializeComponent();
        }

        private void PriceListForm4_Load(object sender, EventArgs e)
        {
            Program.Param poh = new Program.Param();
            conn = new MySqlConnection(poh.pod);
            Reload();

            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 40;
            dataGridView1.Columns[2].FillWeight = 15;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void GetListTarif()
        {
            table.Clear();
            string commandStr = $"SELECT id AS 'Айди', Name AS 'Название тарифа', Cost AS 'Стоимость' FROM Tarif";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            bSource.DataSource = table;
            dataGridView1.DataSource = bSource;
            conn.Close();
        }

        public bool InsertTarif(string IName, int ICost)
        {
            int InsertCount = 0;
            bool result = false;
            conn.Open();
            string query = $"INSERT INTO Tarif (Name, Cost) VALUES ('{IName}', '{ICost}')";
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

        public void Reload()
        {
            GetListTarif();
            Comboup();
        }

        public void Comboup()
        {
            comboBox1.Items.Clear();
            string com = $"SELECT id from Tarif";
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
        public void TextboxFill(TextBox txt, string kolonka)
        {
            string que1 = $"select {kolonka} from Tarif where id={Convert.ToInt32(comboBox1.SelectedItem)}";
            MySqlCommand com1 = new MySqlCommand(que1, conn);
            conn.Open();
            string result = com1.ExecuteScalar().ToString();
            conn.Close();
            txt.Text = result;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InsertTarif(textBox1.Text, Convert.ToInt32(textBox2.Text));
            Reload();
            MessageBox.Show("Тариф успешно добавлен");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            TextboxFill(textBox1, "Name");
            TextboxFill(textBox2, "Cost");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string com = $"UPDATE Tarif set Name ='{textBox1.Text}', Cost ='{textBox2.Text}' where id='{Convert.ToInt32(comboBox1.Text)}'";
            MySqlCommand cmd = new MySqlCommand(com, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Reload();
            MessageBox.Show("Тариф успешно отредактирован");
        }
    }
}
