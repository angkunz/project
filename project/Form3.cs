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
using System.Threading;
using System.Globalization;


namespace project
{
    public partial class Form3 : Form
    {

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
            cmd.CommandText = $"SELECT * FROM equipment WHERE address = \"{Program.facutyadmin}\" ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);

            conn.Close();

            showall2.DataSource = ds.Tables[0].DefaultView;
        }


        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (id.Text=="")
                {
                    MessageBox.Show("กรุณากรอกหมายเลขครุภัณฑ์");
                }
                else if (name.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อครุภัณฑ์");
                }
                else if (generation.Text == "")
                {
                    MessageBox.Show("กรุณากรอกรุ่น-ยี่ห้อของครุภัณฑ์");
                }
                else if (Convert.ToString(kind.SelectedItem) == "")
                {
                    MessageBox.Show("กรุณาเลือกประเภทครุภัณฑ์");
                }
                else if (date.Text == "  /  /")
                {
                    MessageBox.Show("กรุณากรอกวันที่รับครุภัณฑ์");
                }
                else if (address.Text == "")
                {
                    MessageBox.Show("กรุณากรอกที่อยู่ครุภัณฑ์");
                }
                else if (person.Text == "")
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้รับผิดชอบ");
                }
                else
                {
                    MySqlConnection conn = databaseConnection();
                    String sql = "INSERT INTO equipment(id,name,generation,kind,date,address,note,person) VALUES('" + id.Text + "','" + name.Text + "','" + generation.Text + "','" + Convert.ToString(kind.SelectedItem) + "','" + date.Text + "','" + address.Text + "','" + note.Text + "','" + person.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    conn.Close();

                    if (rows > 0)
                    {
                        showequipment();
                        MessageBox.Show("Add Data Complete");

                    }
                }
                
            }
            catch
            {
                MessageBox.Show("หมายเลขครุภัณฑ์นี้มีอยู่แล้ว");
            }
           
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            address.Text = Program.facutyadmin;
            showequipment();
            label7.Text = "ผู้ใช้ : " + Program.admin + " เมื่อ : " + System.DateTime.Now.ToString("ddd, dd MMM yyyy   HH : mm น.");
        }

        private void หนาหลกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            this.Hide();
            a.Show();
        }

        private void showall2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            showall2.CurrentRow.Selected = true;
            id.Text = showall2.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
            name.Text = showall2.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();
            generation.Text = showall2.Rows[e.RowIndex].Cells["generation"].FormattedValue.ToString();
            kind.SelectedItem = showall2.Rows[e.RowIndex].Cells["kind"].FormattedValue.ToString();
            date.Text = showall2.Rows[e.RowIndex].Cells["date"].FormattedValue.ToString();
            address.Text = showall2.Rows[e.RowIndex].Cells["address"].FormattedValue.ToString();
            note.Text = showall2.Rows[e.RowIndex].Cells["note"].FormattedValue.ToString();
            person.Text = showall2.Rows[e.RowIndex].Cells["person"].FormattedValue.ToString();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ต้องการลบข้อมูลหรือไม่", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    int selectedRow = showall2.CurrentCell.RowIndex;
                    String deleteId = Convert.ToString(showall2.Rows[selectedRow].Cells["id"].Value);

                    MySqlConnection conn = databaseConnection();

                    String sql = "DELETE FROM equipment WHERE id = '" + deleteId + "'";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    conn.Open();

                    int rows = cmd.ExecuteNonQuery();

                    conn.Close();

                    if (rows > 0)
                    {
                        showequipment();
                        MessageBox.Show("Delete Data Complete");
                    }

                    id.Clear();
                    name.Clear();
                    date.Clear();
                    address.Clear();
                    note.Clear();
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }
   
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = showall2.CurrentCell.RowIndex;
                String editId = Convert.ToString(showall2.Rows[selectedRow].Cells["id"].Value);

                MySqlConnection conn = databaseConnection();

                String sql = "UPDATE equipment SET id =  '" + id.Text + "',name = '" + name.Text + "',generation = '" + generation.Text + "',kind = '" + Convert.ToString(kind.SelectedItem) + "',date = '" + date.Text + "',address = '" + address.Text + "',note = '" + note.Text + "',person = '" + person.Text + "' WHERE id = '" + editId + "'";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                conn.Open();


                int rows = cmd.ExecuteNonQuery();

                conn.Close();

                if (rows > 0)
                {
                    showequipment();
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จแล้ว");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
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
            Form4 b = new Form4();
            b.Show();
            this.Hide();
            
        }

        private void แกไขขอมลToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 a = new Form3();
            this.Hide();
            a.Show();
        }

        private void kind_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
