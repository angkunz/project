using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace project
{
    public partial class Form6 : Form
    {
        string OTPCode;
        public static string to;

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=equipment;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public Form6()
        {
            InitializeComponent();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM login2 WHERE user = \"{txtemail.Text}\" ";

            MySqlDataReader row = cmd.ExecuteReader();
            row.Read(); 
            if (row.HasRows)
            {
                if (txtemail.Text != "")
                {
                    string from, pass, messageBody;
                    Random rand = new Random();
                    OTPCode = (rand.Next(999999)).ToString();

                    MailMessage message = new MailMessage();
                    to = (txtemail.Text).ToString();
                    from = "angkun02811@gmail.com";
                    pass = "angkunkaeowanna";
                    messageBody = "Your Reset OTP code is " + OTPCode;
                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "รหัสยืนยันความปลอดภัย";

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    try
                    {
                        smtp.Send(message);
                        MessageBox.Show("Code send successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else 
                {
                    MessageBox.Show("Code send successfully");
                }
                
            }
            else
            {
                MessageBox.Show("ไม่มีชื่อผู้ใช้นี้ในระบบ");
            }
            conn.Close();
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (OTPCode == (txtotp.Text).ToString())
            {
                to = txtemail.Text;
                Form7 np = new Form7();
                this.Hide();
                np.Show();
            }
            else
            {
                MessageBox.Show("รหัสไม่ถูกต้อง");
            }
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
    }
}
