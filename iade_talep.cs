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
    public partial class iade_talep : Form
    {
        public iade_talep()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        private void iade_talep_Load(object sender, EventArgs e)
        {

        }
        public void bilgigetir(string tc, string adsoyad, string telefon, string adres, string barkodno, string kategori, string marka, string urunadi, string toplamfiyat,string satis_id,string odemetur,string s_tarih, string satisfiyat, string miktar, string taksit, string garanti)
        {
            
            lblTC.Text = tc;
            lblAdSoyad.Text = adsoyad;
            lblTelefon.Text = telefon;
            lblBarkodno.Text = barkodno;
            lblKategori.Text = kategori;
            lblMarka.Text = marka;
            lblUrunAd.Text = urunadi;
            lblAdres.Text = adres;
            lblFiyat.Text = toplamfiyat;
            lblSatinTarih.Text = s_tarih;
            lblSatisId.Text = satis_id;
            lblOdeme.Text = odemetur;
            lblSatisFiyat.Text = satisfiyat;
            lblMiktar.Text = miktar;
            lblTaksit.Text = taksit;
            lblGaranti.Text = garanti;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("İade Talebi Oluşturulsun mu?", "Doğrulama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {

                    SqlConnection con = new SqlConnection(baglanti.con);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into iade (garanti_sur,satis_id,barkodno,kategori,marka,urunad,tc,adsoyad,telefon,adres,odeme_tur,toplam_fiyat,satinalinan_tarih,iade_sebep,iade_tarih,taksit_sayisi,miktari,satis_fiyat) values (@garanti_sur,@satis_id,@barkodno,@kategori,@marka,@urunad,@tc,@adsoyad,@telefon,@adres,@odeme_tur,@toplam_fiyat,@satinalinan_tarih,@iade_sebep,@iade_tarih,@taksit_sayisi,@miktari,@satis_fiyat)", con);
                    cmd.Parameters.AddWithValue("@satis_id", lblSatisId.Text);
                    cmd.Parameters.AddWithValue("@garanti_sur", lblGaranti.Text);
                    cmd.Parameters.AddWithValue("@barkodno", lblBarkodno.Text);
                    cmd.Parameters.AddWithValue("@kategori", lblKategori.Text);
                    cmd.Parameters.AddWithValue("@marka", lblMarka.Text);
                    cmd.Parameters.AddWithValue("@urunad", lblUrunAd.Text);
                    cmd.Parameters.AddWithValue("@tc", lblTC.Text);
                    cmd.Parameters.AddWithValue("@adsoyad", lblAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@telefon", lblTelefon.Text);
                    cmd.Parameters.AddWithValue("@adres", lblAdres.Text);
                    cmd.Parameters.AddWithValue("@odeme_tur", lblOdeme.Text);
                    cmd.Parameters.AddWithValue("@toplam_fiyat", lblFiyat.Text);
                    cmd.Parameters.AddWithValue("@satinalinan_tarih", DateTime.Parse(lblSatinTarih.Text));
                    cmd.Parameters.AddWithValue("@iade_sebep", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@iade_tarih", DateTime.Now);
                    cmd.Parameters.AddWithValue("@satis_fiyat", lblSatisFiyat.Text);
                    cmd.Parameters.AddWithValue("@miktari", lblMiktar.Text);
                    cmd.Parameters.AddWithValue("@taksit_sayisi", lblTaksit.Text);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd1 = new SqlCommand("delete from satis where satis_id = '" + lblSatisId.Text + "'", con);
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("update urun set miktari=miktari+'" + int.Parse(lblMiktar.Text) + "' where barkodno='" + lblBarkodno.Text + "'", con);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    satislistele satislarForm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is satislistele) as satislistele;
                    if (satislarForm != null)
                    {
                        satislarForm.SatisListele();
                    }
                    MessageBox.Show("Ürün İade Talebi Oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Hata Oluştu " + ex.Message, "Uyarı!!");
                }
                this.Close();
            }
                
        }
    }
}
