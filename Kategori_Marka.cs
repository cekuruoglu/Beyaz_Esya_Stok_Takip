using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class Kategori_Marka : Form
    {
        public Kategori_Marka()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        bool durum;
        private void kategorikontrol() // Kategori kontrolü yapan fonksiyon
        {
            durum = true;
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from kategoribilgileri", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (txtKategori.Text == dr["kategori"].ToString() || txtKategori.Text == "")
                {
                    durum = false;
                }
            }
            con.Close();
        }
        private void markakontrol() // Marka kontrolünü gerçekleştiren fonksiyon
        {
            durum = true;
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from markabilgileri", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    // Eğer seçilen kategori ve marka zaten mevcutsa veya kategori veya marka boşsa
                    if (cbKategori.Text == dr["kategori"].ToString() && txtMarka.Text == dr["marka"].ToString() || cbKategori.Text == "" || txtMarka.Text == "")
                    {
                        durum = false;
                    }
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }
        private void kategorigetir() // Kategori bilgilerini ComboBox'a getiren fonksiyon
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from kategoribilgileri", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbKategori.Items.Add(dr["kategori"].ToString());

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }

        private void btnKatEkle_Click(object sender, EventArgs e)
        {
            kategorikontrol();
            if (durum == true)
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into kategoribilgileri(kategori) values('" + txtKategori.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Kategori Eklendi!");
                cbKategori.Items.Clear();
                kategorigetir();
            }
            else // Eğer kategori kontrolü geçilmediyse, uyarı mesajı göster
            {
                MessageBox.Show("Böyle bir kategori var", "Uyarı");
            }

            txtKategori.Text = ""; // Kategori ekleme işlemi sonrasında giriş alanını temizle
        }

        private void Kategori_Marka_Load(object sender, EventArgs e)
        {
            kategorigetir();
        }

        private void btnMarEkle_Click(object sender, EventArgs e)
        {
            markakontrol();

            if (durum == true)// Eğer marka kontrolü geçildiyse
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into markabilgileri(kategori,marka) values('" + cbKategori.Text + "','" + txtMarka.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Marka Eklendi!");
            }
            else // Eğer marka kontrolü geçilmediyse, uyarı mesajı göster
            {
                MessageBox.Show("Böyle bir kategori ve marka var", "Uyarı");
            }
            // Marka ekleme işlemi sonrasında giriş alanlarını temizle
            txtMarka.Text = "";
            cbKategori.Text = "";
        }

        private void Kategori_Marka_FormClosing(object sender, FormClosingEventArgs e)
        {
            Urunler urun = new Urunler();
            urun.Show();
        }
    }
}
