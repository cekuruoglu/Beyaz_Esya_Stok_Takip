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
    public partial class sepetekle : Form
    {
        public sepetekle()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();

        public void sepetekleme(string tc, string adsoyad, string telefon, string adres, string barkodno, string kategori, string marka, string urunadi, string satisfiyat, string garanti)
        {
            txtSatisF.Text = satisfiyat;
            txtTc.Text = tc;
            txtAdSoyad.Text = adsoyad;
            txtTelefon.Text = telefon;
            txtBarkodNo.Text = barkodno;
            txtKategori.Text = kategori;
            txtMarka.Text = marka;
            txtUrunAdi.Text = urunadi;
            txtAdres.Text = adres;
            txtGaranti.Text = garanti;
        }
        private void sepetekle_Load(object sender, EventArgs e)
        {
            Toplam.Text = txtSatisF.Text;
            
        }
        bool durum;
        private void barkodkontrol()
        {
            durum = true;
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from sepet", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (txtBarkodNo.Text == dr["barkodno"].ToString()) 
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
        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Toplam.Text = (double.Parse(txtMiktar.Text) * double.Parse(txtSatisF.Text)).ToString();

            }
            catch
            {
               
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(baglanti.con);
            con1.Open();


            SqlCommand cmd5 = new SqlCommand("select miktari from urun where barkodno='" + txtBarkodNo.Text + "'", con1);
            SqlDataReader dr = cmd5.ExecuteReader();

            if (dr.Read())
            {
                Satislar satislarForm1 = new Satislar();
                // Mevcut ürün miktarını al
                int mevcutMiktar = Convert.ToInt32(dr["miktari"]);
                con1.Close();

                // Girilen miktarın mevcut miktarı aşmamasını kontrol eder
                if (mevcutMiktar >= Convert.ToInt32(txtMiktar.Text))
                {
                    // Barkod numarasının sepet içinde olup olmadığını kontrol eder
                    barkodkontrol();
                    if (durum == true)
                    {
                        try
                        {

                            SqlConnection con = new SqlConnection(baglanti.con);
                            con.Open();
                            SqlCommand cmd = new SqlCommand("insert into sepet(tc,adsoyad,telefon,adres,barkodno,kategori,marka,urunadi,miktari,satis_fiyat,toplam_fiyat,garanti_sur) values(@tc,@adsoyad,@telefon,@adres,@barkodno,@kategori,@marka,@urunadi,@miktari,@satis_fiyat,@toplam_fiyat,@garanti_sur)", con);
                            cmd.Parameters.AddWithValue("@tc", txtTc.Text);
                            cmd.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                            cmd.Parameters.AddWithValue("@telefon", txtTelefon.Text);
                            cmd.Parameters.AddWithValue("@adres", txtAdres.Text);
                            cmd.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                            cmd.Parameters.AddWithValue("@kategori", txtKategori.Text);
                            cmd.Parameters.AddWithValue("@marka", txtMarka.Text);
                            cmd.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                            cmd.Parameters.AddWithValue("@garanti_sur", txtGaranti.Text);
                            cmd.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                            cmd.Parameters.AddWithValue("@toplam_fiyat", double.Parse(Toplam.Text));
                            cmd.Parameters.AddWithValue("@satis_fiyat", double.Parse(txtSatisF.Text));
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Hata Oluştu " + ex.Message, "Uyarı!!");
                        }
                    }
                    else
                    {
                        // Eğer ürün zaten sepet içindeyse, miktarını ve toplam fiyatını günceller
                        SqlConnection con = new SqlConnection(baglanti.con);
                        con.Open();

                        SqlCommand cmd2 = new SqlCommand("update sepet set miktari=miktari+'" + int.Parse(txtMiktar.Text) + "' where barkodno='" + txtBarkodNo.Text + "'", con);
                        cmd2.ExecuteNonQuery();

                        SqlCommand cmd3 = new SqlCommand("update sepet set toplam_fiyat=miktari*satis_fiyat where barkodno='" + txtBarkodNo.Text + "'", con);
                        cmd3.ExecuteNonQuery();

                        con.Close();
                    }
                    DialogResult result = MessageBox.Show("Sepete Eklensin mi?", "Doğrulama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Satislar satislarForm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Satislar) as Satislar;
                        if (satislarForm != null)
                        {


                            satislarForm1.musteribilgi(txtAdSoyad.Text, txtAdres.Text, txtTelefon.Text, txtTc.Text);
                            satislarForm.SepetListele();
                            satislarForm.hesapla();


                        }

                    }
                    //satislarForm1.Show();
                    this.Close();
                }
                else
                {

                    MessageBox.Show("Miktar fazla girildi");
                }

            }
        }  
    }
}
