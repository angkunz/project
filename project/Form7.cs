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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=equipment;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else
            {
                if (textBox1.Text == textBox2.Text)
                {

                    MySqlConnection conn = databaseConnection();
                    string sql = "UPDATE login2 SET password ='" + textBox2.Text + "' WHERE user = '" + Form6.to + "' ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("เปลี่ยนรหัสผ่านเรียบร้อย", "แจ้งเตือน");
                    Form1 a = new Form1();
                    this.Hide();
                    a.Show();
                }
                else
                {
                    MessageBox.Show("โปรดตรวจสอบรหัสผ่านอีกครั้ง", "แจ้งเตือน");
                }
            }
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
    }
}
