using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartovoeForm1());
        }
        public class Param
        {
            public string pod = "server=chuc.caseum.ru; port=33333; user=st_2_19_17; password=55795425; database= is_2_19_st17_KURS";
        }
    }
}
