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
    public partial class satislistele : Form
    {
        public satislistele()
        {
            InitializeComponent();
            menustyle menuStyle = new menustyle();
            menuStrip1.Renderer = menuStyle;
        }
        baglanti baglanti = new baglanti();
        DataSet daset = new DataSet();

        private void satislistele_Load(object sender, EventArgs e)
        {
            SatisListele();
            kategorigetir();
        }

        #region Tasarım
        private void iadeBTN_MouseEnter(object sender, EventArgs e)
        {
            iadeBTN.ForeColor = ColorTranslator.FromHtml("#F4F4F2");
        }

        private void iadeBTN_MouseLeave(object sender, EventArgs e)
        {
            iadeBTN.ForeColor = ColorTranslator.FromHtml("#495464");
        }

        #endregion

        #region Özel Fonksiyonlar
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
                    KategoriCB.Items.Add(dr["kategori"].ToString());

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }
        public void SatisListele()
        {
            if (!daset.Tables.Contains("satis"))
            {
                daset.Tables.Add("satis");
            }
            else
            {
                daset.Tables["satis"].Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from satis", con);
                adtr.Fill(daset, "satis");
                dataGridView1.DataSource = daset.Tables["satis"];
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }


        #endregion

        #region Filtreleme
        private void KategoriCB_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where kategori like '%" + KategoriCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void KategoriCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KategoriCB.Text == "")
            {
                MarkaCB.Items.Clear();
                MarkaCB.Text = "";
                SatisListele();
            }
            else
            {

                MarkaCB.Items.Clear();
                MarkaCB.Text = "";
                try
                {
                    DataTable table = new DataTable();
                    SqlConnection con = new SqlConnection(baglanti.con);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from markabilgileri where kategori='" + KategoriCB.SelectedItem + "'", con);
                    SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where kategori= '" + KategoriCB.Text + "'", con);
                    adtr.Fill(table);
                    dataGridView1.DataSource = table;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MarkaCB.Items.Add(dr["marka"].ToString());

                    }
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
                }
            }
        }

        private void MarkaCB_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where marka like '%" + MarkaCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void OdemeCB_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where odeme_tur like '%" + OdemeCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void UrunAdiTXT_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where urunadi like '%" + UrunAdiTXT.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where tc like '%" + txtTc.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where adsoyad like '%" + txtAdSoyad.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where telefon like '%" + txtTelefon.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }
        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from satis where barkodno like '%" + BarkodNotxt.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }


        #endregion

        private void iadeBTN_Click(object sender, EventArgs e)
        {
            txtGaranti.Text = dataGridView1.CurrentRow.Cells["garanti_sur"].Value.ToString();
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            txtKategori.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            txtUrunAd.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtToplamFiyat.Text = dataGridView1.CurrentRow.Cells["toplam_fiyat"].Value.ToString();
            txtSatisId.Text = dataGridView1.CurrentRow.Cells["satis_id"].Value.ToString();
            TCtxt.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            AdSoyadtxt.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            Telefontxt.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtOdeme.Text = dataGridView1.CurrentRow.Cells["odeme_tur"].Value.ToString();
            txtSatinTarih.Text = dataGridView1.CurrentRow.Cells["tarih"].Value.ToString();
            txtSatisFiyat.Text = dataGridView1.CurrentRow.Cells["satis_fiyat"].Value.ToString();
            txtTaksit.Text = dataGridView1.CurrentRow.Cells["taksit_sayisi"].Value.ToString();
            txtMiktar.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();

            iade_talep iade = new iade_talep();
            iade.bilgigetir(TCtxt.Text,AdSoyadtxt.Text,Telefontxt.Text,txtAdres.Text,txtBarkodNo.Text,txtKategori.Text,txtMarka.Text,txtUrunAd.Text,txtToplamFiyat.Text,txtSatisId.Text,txtOdeme.Text,txtSatinTarih.Text,txtSatisFiyat.Text,txtMiktar.Text,txtTaksit.Text,txtGaranti.Text);
            iade.Show();
        }

    }
}
