using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRUPO1
{
    static class Program
    {
        public static string RazonSocial = "Ponzio SRL";
        public static string CUIT = "20-12774982-2";
        public static string Domicilio = "Hortiguera 600";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmTP1());
        }
    }
}
