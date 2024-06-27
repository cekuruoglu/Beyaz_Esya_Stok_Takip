using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
            menustyle menuStyle = new menustyle();
            menuStrip1.Renderer = menuStyle;
        }
        #region Butonlar
        private void ürünEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UrunEkle urunekle = new UrunEkle();
            urunekle.Show();
            this.Close();
        }

        private void kategoriMarkaİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kategori_Marka kategorimarka = new Kategori_Marka();
            kategorimarka.Show();
            this.Close();
        }
        #endregion

        baglanti baglanti = new baglanti();
        DataSet daset = new DataSet();
        public void UrunListele() // Ürünleri DataGridView'e listeleyen fonksiyon
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", con);
                adtr.Fill(daset, "urun");
                dataGridView1.DataSource = daset.Tables["urun"];
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
                    KategoriCB.Items.Add(dr["kategori"].ToString());

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }

        private void Urunler_Load(object sender, EventArgs e)
        {
            UrunListele();
            kategorigetir();
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBarkodNo.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            cbKategori.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            cbMarka.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            cbEnerji.Text = dataGridView1.CurrentRow.Cells["enerji_sinif"].Value.ToString();
            cbGaranti.Text = dataGridView1.CurrentRow.Cells["garanti_sur"].Value.ToString();
            txtUrunAdi.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtMiktar.Text = dataGridView1.CurrentRow.Cells["miktari"].Value.ToString();
            txtAlis.Text = dataGridView1.CurrentRow.Cells["alisfiyati"].Value.ToString();
            txtSatis.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (bosluk_kontrol(panel1))
            {

                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("update urun set kategori=@kategori,marka=@marka,enerji_sinif=@enerji_sinif,garanti_sur=@garanti_sur,urunadi=@urunadi,miktari=@miktari,alisfiyati=@alisfiyati,satisfiyati=@satisfiyati where barkodno=@barkodno", con);
                cmd.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                cmd.Parameters.AddWithValue("@kategori", cbKategori.Text);
                cmd.Parameters.AddWithValue("@marka", cbMarka.Text);
                cmd.Parameters.AddWithValue("@enerji_sinif", cbEnerji.Text);
                cmd.Parameters.AddWithValue("@garanti_sur", cbGaranti.Text);
                cmd.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                cmd.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                cmd.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlis.Text));
                cmd.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatis.Text));
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

            daset.Tables["urun"].Clear();
            UrunListele();
            MessageBox.Show("Güncelleme Yapıldı!");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void btnFoto_Click(object sender, EventArgs e)
        {
            
            byte[] resimBytes = ResmiGetirFromSQL();
            UrunFoto urunfoto = new UrunFoto(dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString(), resimBytes) ;
            
            dataGridView1.DataSource = null;
            urunfoto.ShowDialog();

        }
       
        private byte[] ResmiGetirFromSQL()
        {
            byte[] resimBytes = null;
            try
            {
                // SQL sorgusu ile resmi al
                using (SqlConnection con = new SqlConnection(baglanti.con))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT foto FROM urun WHERE barkodno = @barkodno", con);
                    command.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        resimBytes = (byte[])reader["foto"];
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim getirme hatası: " + ex.Message);
            }
            return resimBytes;
        }

        private bool bosluk_kontrol(Control control)
        {
            bool _bos = false;

            foreach (Control childControl in control.Controls)
            {

                if (childControl is TextBox || childControl is ComboBox || childControl is MaskedTextBox)
                {

                    if (string.IsNullOrWhiteSpace(childControl.Text))
                    {
                        _bos = true;
                        break;
                    }
                }


                if (childControl.Controls.Count > 0)
                {
                    _bos = bosluk_kontrol(childControl);
                    if (_bos) break;
                }
            }

            return _bos;
        }

        private void cbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbMarka.Items.Clear();
            cbMarka.Text = "";
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from markabilgileri where kategori='" + cbKategori.SelectedItem + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbMarka.Items.Add(dr["marka"].ToString());

                }
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from urun where barkodno ='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                daset.Tables["urun"].Clear();
                UrunListele();
                MessageBox.Show("Kayıt Silindi!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }


        #region Tasarım
        private void ürünEkleToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ürünEkleToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#F4F4F2");
        }

        private void ürünEkleToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ürünEkleToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#495464");
        }

        private void kategoriMarkaİşlemleriToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            kategoriMarkaİşlemleriToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#F4F4F2");
        }

        private void kategoriMarkaİşlemleriToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            kategoriMarkaİşlemleriToolStripMenuItem.ForeColor = ColorTranslator.FromHtml("#495464");
        }
        #endregion

        #region Filtreleme

        private void KategoriCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            MarkaCB.Items.Clear();
            MarkaCB.Text = "";
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from markabilgileri where kategori='" + KategoriCB.SelectedItem + "'", con);
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

        private void cbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
                DataTable table = new DataTable();
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where marka like '%" + MarkaCB.Text + "%'", con);
                adtr.Fill(table);
                dataGridView1.DataSource = table;
                con.Close();
                
           
            
        }

        private void cbEnerji_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where enerji_sinif like '%" + EnerjiCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
            
        }

        private void cbGaranti_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where garanti_sur like '%" + GarantiCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
            
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where barkodno like '%" + BarkodNotxt.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
            
        }

        private void txtUrunAdi_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where urunadi like '%" + UrunAdiTXT.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
            
        }

       

        private void KategoriCB_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from urun where kategori like '%" + KategoriCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        #endregion


    }
}
