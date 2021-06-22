using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static string id;
        public static string name;
        public static string date;
        public static string address;
        public static string note;
        public static string generation;
        public static string kind;
        public static string person;
        public static string status;

        public static string admin;
        public static string facutyadmin;

    
    }
}
