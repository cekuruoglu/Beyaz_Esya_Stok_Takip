using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class musteriekleme : Form
    {
        public musteriekleme()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        bool durum;
        private void tckontrol()
        {
            durum = true;
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from musteri", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (txtTc.Text == dr["tc"].ToString() || txtTc.Text == "")
                {
                    durum = false;
                }
            }
            con.Close();
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
        private void btnEkle_Click(object sender, EventArgs e)
        {
            tckontrol();
            if (durum == true)
            {
                string email = txtEmail.Text;

                if (IsValidEmail(email))
                {
                    SqlConnection con = new SqlConnection(baglanti.con);
                    SqlCommand cmd = new SqlCommand("insert into musteri(tc,adsoyad,telefon,adres,email) values(@tc,@adsoyad,@telefon,@adres,@email)", con);

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
                        DialogResult result = MessageBox.Show("Kayıt Başarıyla Oluşturuldu!", "Kayıt Bilgilendirme", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {

                            Musteriler musteriler = new Musteriler();
                            musteriler.Show();
                            this.Close();
                        }
                        foreach (Control item in this.Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }

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
            else
            {
                MessageBox.Show("Böyle bir TC numarası var", "Uyarı");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void musteriekleme_FormClosing(object sender, FormClosingEventArgs e)
        {
            Musteriler musteriler = new Musteriler();
            musteriler.Show();
            
        }
    }
}
