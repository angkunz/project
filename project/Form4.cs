using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Image = System.Drawing.Image;
using project.Properties;
using Font = System.Drawing.Font;

namespace project
{
    public partial class Form4 : Form
    {


        private List<ForPrint> allbook = new List<ForPrint>();

        public Form4()
        {
            InitializeComponent();
        }

        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=equipment;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void showequipment()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            string name = textBox1.Text;
            if (name == "")
            {
                cmd.CommandText = $"SELECT * FROM equipment2 ";
            }
            else
            {
                cmd.CommandText = $"SELECT * FROM equipment WHERE id=\"{name}\" OR name =\"{name}\" OR kind=\"{name}\" OR generation=\"{name}\" OR status=\"{name}\" OR date=\"{name}\" OR note=\"{name}\" OR person=\"{name}\" ";
            }


            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataGridView.DataSource = ds.Tables[0].DefaultView;
        }
        private void showequipment2()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM equipment2 ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            dataGridView.DataSource = ds.Tables[0].DefaultView;
        }

        private void พมพToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 a = new Form4();
            this.Hide();
            a.Show();
        }

        private void ออกจากระบบToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }

        private void แกไขขอมลToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 a = new Form3();
            this.Hide();
            a.Show();
        }

        private void หนาหลกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            this.Hide();
            a.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            deletedata();
            move();
            showequipment2();
            label7.Text = "ผู้ใช้ : " + Program.admin + " เมื่อ : " + System.DateTime.Now.ToString("ddd, dd MMM yyyy   HH : mm น.");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showequipment();
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showequipment();
                
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            allbook.Clear();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image logo = Image.FromFile(@"C:\Users\qqx99\Documents\Angkun_C#\project\image\name2.png");
            e.Graphics.DrawImage(logo, new PointF(50, 50));
            e.Graphics.DrawString("สำนักบริหารจัดการพัสดุ สำนักงานอธิการบดี มหาวิทยาลัยขอนแก่น", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 140));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------------------", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 160));
            e.Graphics.DrawString("รายการทะเบียนครุภัณฑ์มหาวิทยาลัยขอนแก่น", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 192));
            e.Graphics.DrawString("หน่วยงาน/คณะ "+ Program.facutyadmin, new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 192));
            e.Graphics.DrawString("พิมพ์เมื่อ "+ System.DateTime.Now.ToString("dd / MM / yyyy   HH : mm : ss น."), new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 215));
            e.Graphics.DrawString("พิมพ์โดย " + Program.admin , new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 215));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------------------", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 240));
            e.Graphics.DrawString("รหัสครุภัณฑ์", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 255));
            e.Graphics.DrawString("ชื่อครุภัณฑ์", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(140, 255));
            e.Graphics.DrawString("ประเภทครุภัณฑ์", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(250, 255));
            e.Graphics.DrawString("วันที่รับ", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(380, 255));
            e.Graphics.DrawString("ผู้รับผิดชอบครุภัณฑ์", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(460, 255));
            e.Graphics.DrawString("สถานะครุภัณฑ์", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(650, 255));
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------------------", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, 265));
            int y = 290;
            loaddata();
            foreach(var i in allbook)
            {
                e.Graphics.DrawString(i.id, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(50, y));
                e.Graphics.DrawString(i.name, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(140, y));
                e.Graphics.DrawString(i.kind, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(250, y));
                e.Graphics.DrawString(i.date, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(380, y));
                e.Graphics.DrawString(i.person, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(460, y));
                e.Graphics.DrawString(i.status, new Font("TH SarabunPSK", 12, FontStyle.Regular), Brushes.Black, new PointF(650, y));
                y = y + 20;  
            }
            e.Graphics.DrawString("-----------------------------------------------------------------------------------------------------------------------------------------------", new Font("TH SarabunPSK", 16, FontStyle.Bold), Brushes.Black, new PointF(50, y));
        }
        private void loaddata()
        {
            allbook.Clear();
            MySqlConnection conn = new MySqlConnection("host=127.0.0.1;username=root;password=;database=equipment;");

            conn.Open();

            string name = textBox1.Text;
            if (name == "")
            {

                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM equipment2 ", conn);
                MySqlDataReader adapter = cmd.ExecuteReader();

                while (adapter.Read())
                {
                    Program.id = adapter.GetString("id");
                    Program.name = adapter.GetString("name");
                    Program.date = adapter.GetString("date");
                    Program.address = adapter.GetString("address");
                    Program.note = adapter.GetString("note");
                    Program.generation = adapter.GetString("generation");
                    Program.kind = adapter.GetString("kind");
                    Program.person = adapter.GetString("person");
                    Program.status = adapter.GetString("status");
                    ForPrint item = new ForPrint()
                    {
                        id = Program.id,
                        name = Program.name,
                        date = Program.date,
                        address = Program.address,
                        note = Program.note,
                        generation = Program.generation,
                        kind = Program.kind,
                        person = Program.person,
                        status = Program.status

                    };
                    allbook.Add(item);
                }
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM equipment WHERE id=\"{name}\" OR name =\"{name}\" OR kind=\"{name}\" OR generation=\"{name}\" OR status=\"{name}\" OR date=\"{name}\" OR note=\"{name}\" OR person=\"{name}\"  ", conn);
                MySqlDataReader adapter = cmd.ExecuteReader();

                while (adapter.Read())
                {
                    Program.id = adapter.GetString("id");
                    Program.name = adapter.GetString("name");
                    Program.date = adapter.GetString("date");
                    Program.address = adapter.GetString("address");
                    Program.note = adapter.GetString("note");
                    Program.generation = adapter.GetString("generation");
                    Program.kind = adapter.GetString("kind");
                    Program.person = adapter.GetString("person");
                    Program.status = adapter.GetString("status");
                    ForPrint item = new ForPrint()
                    {
                        id = Program.id,
                        name = Program.name,
                        date = Program.date,
                        address = Program.address,
                        note = Program.note,
                        generation = Program.generation,
                        kind = Program.kind,
                        person = Program.person,
                        status = Program.status

                    };
                    allbook.Add(item);
                }
            }
        }

        private void move()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO equipment2 SELECT * FROM equipment WHERE address = \"{Program.facutyadmin}\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();
        }
        private void deletedata()
        {
            MySqlConnection conn = databaseConnection();

            String sql = "DELETE FROM equipment2";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
