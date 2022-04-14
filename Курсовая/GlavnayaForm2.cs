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
    public partial class GlavnayaForm2 : Form
    {
        public GlavnayaForm2()
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

        private void button1_Click(object sender, EventArgs e)
        {
            PriceListForm4 Form4 = new PriceListForm4();
            Form4.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SotrudForm6 Form6 = new SotrudForm6();
            Form6.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientForm5 Form5 = new ClientForm5();
            Form5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SkadForm3 Form3 = new SkadForm3();
            Form3.Show();
        }
    }
}
