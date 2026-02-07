using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kayıt_Program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection connect = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Erdem\\Documents\\entry1.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader read;

        private void verilerigöster()
        {
            listView1.Items.Clear();
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = ("select * from Kişiler ");
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = read["Kimlik"].ToString();
                ekle.SubItems.Add(read["TC"].ToString());
                ekle.SubItems.Add(read["AD"].ToString());
                ekle.SubItems.Add(read["SOYAD"].ToString());

                listView1.Items.Add(ekle);
            }
            connect.Close();
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Boş alanları doldurun.");
                return;
            }
            else
            {
                connect.Open();
                cmd.Connection = connect;
                cmd.CommandText = ($"INSERT INTO Kişiler (TC, AD, SOYAD)" + $"VALUES ('{textBox1.Text}', ' {textBox2.Text}', '{textBox3.Text}')");
                cmd.ExecuteNonQuery();
                connect.Close();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            verilerigöster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = "DELETE FROM Kişiler WHERE AD= '"  + textBox4.Text + "'";

            cmd.ExecuteNonQuery();
            connect.Close();
            textBox4.Clear();
            verilerigöster();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = "UPDATE Kişiler SET AD = '" + textBox2.Text + "', SOYAD = '" + textBox3.Text + "' WHERE TC = '" + textBox1.Text + "'";

            cmd.ExecuteNonQuery();
            connect.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            verilerigöster();
        }

    }
}
