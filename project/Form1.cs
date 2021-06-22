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


namespace project
{
    public partial class Form1 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=equipment;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Login()
        {
            try
            {
                MySqlConnection conn = databaseConnection();
                conn.Open();

                MySqlCommand cmd;

                cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM login2 WHERE user = \"{user.Text}\" AND password = \"{password.Text}\"";

                MySqlDataReader row = cmd.ExecuteReader();
                row.Read();
                Program.admin = row.GetString("name");
                Program.facutyadmin = row.GetString("facuty");
                if (row.HasRows)
                {
                    Form2 a = new Form2();
                    this.Hide();
                    a.Show();
                    MessageBox.Show($"ยินดีต้อนรับ \"{Program.admin}\" ", "เข้าสู่ระบบสำเร็จ");
                }
                conn.Close();
            }
            catch
            {
                status.Text = "ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง";
            } 
        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void password_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.PasswordChar = '\0';
            }
            else
            {
                password.PasswordChar = '•';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form5 b = new Form5();
            this.Hide();
            b.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form6 a = new Form6();
            this.Hide();
            a.Show();
        }
    }
}
