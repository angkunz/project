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
using Schedule;


namespace project
{
    public partial class Form2 : Form
    {
        string datecick;
        string namecick;
        string gencick;
        string kindcick;
        string personcick;

        public Form2()
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
                cmd.CommandText = $"SELECT * FROM equipment2 WHERE id=\"{name}\" OR name =\"{name}\" OR kind=\"{name}\" OR generation=\"{name}\" OR status=\"{name}\" OR date=\"{name}\" OR note=\"{name}\" OR person=\"{name}\" OR address = \"{name}\" ";
            }
            

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            showall.DataSource = ds.Tables[0].DefaultView;
        }

        private void showall_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            showall.CurrentRow.Selected = true;
            datecick = showall.Rows[e.RowIndex].Cells["date"].FormattedValue.ToString();
            namecick = showall.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
            gencick = showall.Rows[e.RowIndex].Cells["generation"].FormattedValue.ToString();
            kindcick = showall.Rows[e.RowIndex].Cells["kind"].FormattedValue.ToString();
            personcick = showall.Rows[e.RowIndex].Cells["person"].FormattedValue.ToString();
            amount();
        }

        private void showequipment2()
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM equipment2";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            showall.DataSource = ds.Tables[0].DefaultView;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            end_old();
            deletedata();
            move();
            comboBox1.SelectedIndex = 0;
            amount();
            showequipment2();
            
            label2.Text = "ผู้ใช้ : " + Program.admin + " เมื่อ : " + System.DateTime.Now.ToString("ddd, dd MMM yyyy   HH : mm น.");

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            showequipment();
        }

        private void แกไขขอมลToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 b = new Form3();
            this.Hide();
            b.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showequipment();
            }
        }

        private void ออกจากระบบToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 a = new Form1();
            this.Hide();
            a.Show();
        }

        private void พมพToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 a = new Form4();
            this.Hide();
            a.Show();
        }

        private void end_old()
        {
            MySqlConnection conn = databaseConnection();
            conn.Open();

            MySqlCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT * FROM equipment WHERE address = \"{Program.facutyadmin}\" ";

            MySqlDataReader row = cmd.ExecuteReader();

            while(row.Read())
            {
                string Id = row[0].ToString();
                string s = System.DateTime.Now.ToString("dd/MM/yyyy");
                string[] subs = s.Split('/');

                string r = row.GetString("date");
                string[] subr = r.Split('/');

                string kind = row.GetString("kind");
                int count = 0;
                if (kind == "โฆษณาและเผยแพร่")
                {
                    count = 10;
                }
                else if (kind == "ไฟฟ้าและวิทยุ")
                {
                    count = 10;
                }
                else if (kind == "การเกษตร")
                {
                    count = 5;
                }
                else if (kind == "การศึกษา")
                {
                    count = 5;
                }
                else if (kind == "กีฬา")
                {
                    count = 5;
                }
                else if (kind == "คอมพิวเตอร์")
                {
                    count = 5;
                }
                else if (kind == "ดนตรี")
                {
                    count = 5;
                }
                else if (kind == "ยานพหนะและขนส่ง")
                {
                    count = 8;
                }
                else if (kind == "วิทยาศาสตร์และการแพทย์")
                {
                    count = 8;
                }
                else if (kind == "สนาม")
                {
                    count = 5;
                }
                else if (kind == "สำนักงาน")
                {
                    count = 12;
                }
                else if (kind == "สำรวจ")
                {
                    count = 10;
                }

                DateTime date1 = new DateTime(Convert.ToInt32(subs[2]), Convert.ToInt32(subs[1]), Convert.ToInt32(subs[0]));
                DateTime date2 = new DateTime(Convert.ToInt32(subr[2]) + count, Convert.ToInt32(subr[1]), Convert.ToInt32(subr[0]));
                int result = DateTime.Compare(date1, date2);

                if (result > 0)
                {
                    try
                    {
                        MySqlConnection conn1 = databaseConnection();

                        String sql1 = "UPDATE equipment SET status = \"สิ้นอายุ-รอการจำหน่าย\" WHERE id = '" + Id + "' ";

                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn1);

                        conn1.Open();


                        int rows1 = cmd1.ExecuteNonQuery();

                        conn1.Close();

                        if (rows1 > 0)
                        {
                            showequipment();

                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }
                }
                else if (result == 0)
                {
                    try
                    {
                        MySqlConnection conn2 = databaseConnection();

                        String sql2 = "UPDATE equipment SET status = \"สิ้นอายุ-รอการจำหน่าย\" WHERE id = '" + Id + "' ";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);

                        conn2.Open();


                        int rows2 = cmd2.ExecuteNonQuery();

                        conn2.Close();

                        if (rows2 > 0)
                        {
                            showequipment();

                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }

                }
                else
                {
                    try
                    {
                        MySqlConnection conn3 = databaseConnection();

                        String sql3 = "UPDATE equipment SET status = \" \" WHERE id = '" + Id + "' ";

                        MySqlCommand cmd3 = new MySqlCommand(sql3, conn3);

                        conn3.Open();


                        int rows2 = cmd3.ExecuteNonQuery();

                        conn3.Close();

                        if (rows2 > 0)
                        {
                            showequipment();

                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }

                }
            }

            conn.Close();

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        } 

        private void amount()
        {
            string item = Convert.ToString(comboBox1.SelectedItem);
            if (item == "ครุภัณฑ์ทั้งหมด")
            {
                try
                {
                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount FROM equipment2 ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = item;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "ชื่อครุภัณฑ์")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE  name = \"{namecick}\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = namecick;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "ชื่อผู้รับผิดชอบครุภัณฑ์")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE  person = \"{personcick}\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = "ครุภัณฑ์ในความรับผิดชอบของ " + personcick;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "ชื่อรุ่นครุภัณฑ์")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE  generation = \"{gencick}\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = namecick + "รุ่น" + gencick;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "ประเภทครุภัณฑ์")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE kind = \"{kindcick}\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = kindcick;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "วันที่รับครุภัณฑ์")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE date = \"{datecick}\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = "ครุภัณฑ์ที่รับวันที่ " + datecick;

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }

            }
            else if (item == "ครุภัณฑ์ที่สิ้นอายุ")
            {
                try
                {

                    MySqlConnection conn = databaseConnection();
                    DataSet ds = new DataSet();
                    conn.Open();

                    MySqlCommand cmd;

                    cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT COUNT(*) AS amount1 FROM equipment2 WHERE status = \"สิ้นอายุ-รอการจำหน่าย\" ";

                    MySqlDataReader row = cmd.ExecuteReader();
                    row.Read();
                    string amount = row.GetString("amount1");
                    textBox3.Text = amount;
                    textBox2.Text = Program.facutyadmin;
                    textbox4.Text = "ครุภัณฑ์ที่สิ้นอายุ-รอการจำหน่าย";

                    conn.Close();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            amount();
        }
    }
}
