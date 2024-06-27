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
    public partial class satismusteri : Form
    {
        public satismusteri()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        public string adsoyad;
        public string telefon;
        public string adres;
        public string tc;
        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            if (txtTc.Text == "")
            {
                
                txtAdSoyad.Text = "";
                txtTelefon.Text = "";
                txtEmail.Text = "";
                txtAdres.Text = "";
            }
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from musteri where tc like '" + txtTc.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   
                    txtAdSoyad.Text = dr["adsoyad"].ToString();
                    txtTelefon.Text = dr["telefon"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtAdres.Text = dr["adres"].ToString();

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            if (txtTelefon.Text == "")
            {

                txtAdSoyad.Text = "";
                txtTc.Text = "";
                txtEmail.Text = "";
                txtAdres.Text = "";
            }
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from musteri where telefon like '" + txtTelefon.Text + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   
                    txtAdSoyad.Text = dr["adsoyad"].ToString();
                    txtTc.Text = dr["tc"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtAdres.Text = dr["adres"].ToString();

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdres.Text) || string.IsNullOrWhiteSpace(txtAdSoyad.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                DialogResult result = MessageBox.Show("Müşteri bilgileri doğru mu?", "Doğrulama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    adres = txtAdres.Text;
                    telefon = txtTelefon.Text;
                    adsoyad = txtAdSoyad.Text;
                    tc = txtTc.Text;
                    Satislar satis = new Satislar();
                    satis.musteribilgi(adsoyad, adres, telefon, tc);
                    satis.Show();
                    this.Visible =false;

                }
            }
            
        }
    }
}
