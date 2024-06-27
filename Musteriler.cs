using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace beyaz_esya_stok_takip
{
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
            menustyle menuStyle = new menustyle();
            menuStrip1.Renderer = menuStyle;
        }
        baglanti baglanti = new baglanti();
        bool durum;
        DataSet daset = new DataSet();
     
        private void Kayit_Goster() 
        {
            SqlConnection con = new SqlConnection(baglanti.con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri", con);
                adtr.Fill(daset, "musteri");
                dataGridView1.DataSource = daset.Tables["musteri"];
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        


        private void Musteriler_Load(object sender, EventArgs e)
        {
            Kayit_Goster();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // E-posta formatını kontrol etmek için Regex kullanımı
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            if (IsValidEmail(email))
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                SqlCommand cmd = new SqlCommand("update musteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc", con);
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd.Parameters.AddWithValue("@tc", txtTc.Text);
                    cmd.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                    cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.ExecuteNonQuery();
                    daset.Tables["musteri"].Clear();
                    Kayit_Goster();
                    MessageBox.Show("Müşteri Kaydı Güncellendi!");
                    txtTc.Text = "";
                    txtAdSoyad.Text = "";
                    txtTelefon.Text = "";
                    txtEmail.Text = "";
                    txtAdres.Text = "";
                }
                catch (SqlException ex)
                {
                    string hata = ex.Message;
                    throw;
                }
                finally
                {
                    con.Close();
                }

            }
            else
            {
                MessageBox.Show("Geçersiz bir e-posta adresi girdiniz.", "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from musteri where tc ='" + dataGridView1.CurrentRow.Cells["tc"].Value.ToString() + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            daset.Tables["musteri"].Clear();
            Kayit_Goster();
            MessageBox.Show("Kayıt Silindi!");
        }

        private void txtTCara_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where tc like '%" + txtTCara.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void müşteriEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musteriekleme musteriekleme = new musteriekleme();
            musteriekleme.Show();
            this.Close();
        }

        private void müşteriEkleToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            müşteriEkleToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#F4F4F2");
        }

        private void müşteriEkleToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            müşteriEkleToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#495464");
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where telefon like '%" + txtTelAra.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
            if (txtTelAra.Text == "(   )    -  -")
            {
                DataTable table1 = new DataTable();
                SqlConnection con1 = new SqlConnection(baglanti.con);
                con1.Open();
                SqlDataAdapter adtr1 = new SqlDataAdapter("select * from musteri", con1);
                adtr1.Fill(table1);
                dataGridView1.DataSource = table1;
                con1.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from musteri where adsoyad like '%" + textBox1.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }
    }
}
