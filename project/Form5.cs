using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace project
{
    public partial class Form5 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=equipment;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Form5()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
        private void sign_up()
        {
            try
            {
                MySqlConnection conn = databaseConnection();
                String sql = "INSERT INTO login2(name,facuty,user,password) VALUES('" + textBox1.Text + "','" + Convert.ToString(comboBox1.SelectedItem) + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                confirmemail();

                MessageBox.Show("ลงทะเบียนสำเร็จ");
            }
            catch
            {
                MessageBox.Show("เกิดข้อผิดพลาด");
            }

        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน");
            }
            else if (textBox3.Text == textBox4.Text)
            {
                if (textBox3.TextLength < 6)
                {
                    MessageBox.Show("รหัสผ่านต้องมีความยาวมากกว่า 6 ตัวอักษร", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        MySqlConnection conn = databaseConnection();
                        conn.Open();

                        MySqlCommand cmd;

                        cmd = conn.CreateCommand();
                        cmd.CommandText = $"SELECT user FROM login2 WHERE user = \"{textBox2.Text}\" ";

                        MySqlDataReader row = cmd.ExecuteReader();
                        row.Read();
                        if (row.HasRows)
                        {
                            MessageBox.Show("ชื่อผู้ใช้นี้มีอยู่แล้ว");
                        }
                        else
                        {

                            if (chackmail.ChackFormail(textBox2.Text.ToString()))
                            {
                                Cursor = Cursors.WaitCursor;
                                sign_up();
                                Form1 a = new Form1();
                                this.Hide();
                                a.Show();
                            }
                            else
                            {
                                MessageBox.Show("รูปแบบ E-mail ไม่ถูกต้อง");
                            }

                        }
                        conn.Close();
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show(s.Message);
                    }
                }
                
            }
            else
            {
                MessageBox.Show("รหัสผ่านไม่ตรงกัน");
            } 
        }

        private void confirmemail()
        {
            string from, pass, messageBody;
            Random rand = new Random();
            string name = textBox1.Text;
            string fac = Convert.ToString(comboBox1.SelectedItem);

            MailMessage message = new MailMessage();
            string to = textBox2.Text;
            from = "angkun02811@gmail.com";
            pass = "angkunkaeowanna";
            messageBody = "เรียน " + name + " \n\tเราขอแจ้งให้คุณทราบว่าคุณได้ใช้ที่อยู่ E-mail นี้ในการลงทะเบียนเข้าใช้งานในระบบทะเบียนครุภัณฑ์มหาวิทยาลัยขอนแก่น ในส่วนของ" +fac+ "\n\n\n\t\t\t\tจึงเรียนมาเพื่อทราบ" ;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
