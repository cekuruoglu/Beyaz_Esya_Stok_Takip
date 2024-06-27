using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class UrunEkle : Form
    {
        public UrunEkle()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        bool durum;
        private void barkodkontrol() // Barkod kontrolünü gerçekleştiren fonksiyon
        {
            durum = true;
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from urun", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (txtBarkodNo.Text == dr["barkodno"].ToString() || txtBarkodNo.Text == "")
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
        private void UrunEkle_Load(object sender, EventArgs e)
        {
            kategorigetir();
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

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (bosluk_kontrol(panel1))
            {
                
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            if (pictureBox1.Image==null)
            {
                
                MessageBox.Show("Lütfen Ürünün Fotoğrafını ekleyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
            else{
                barkodkontrol();
                if (durum == true)
                {
                    Image resizedImage = ResizeImage(pictureBox1.Image, new Size(300, 200));
                    byte[] resimBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        resimBytes = ms.ToArray();
                    }

                    try
                    {
                        SqlConnection con = new SqlConnection(baglanti.con);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into urun(barkodno,kategori,marka,urunadi,enerji_sinif,garanti_sur,miktari,alisfiyati,satisfiyati,foto,tarih) values(@barkodno,@kategori,@marka,@urunadi,@enerji_sinif,@garanti_sur,@miktari,@alisfiyati,@satisfiyati,@foto,@tarih)", con);
                        cmd.Parameters.AddWithValue("@barkodno", txtBarkodNo.Text);
                        cmd.Parameters.AddWithValue("@kategori", cbKategori.Text);
                        cmd.Parameters.AddWithValue("@marka", cbMarka.Text);
                        cmd.Parameters.AddWithValue("@enerji_sinif", cbEnerji.Text);
                        cmd.Parameters.AddWithValue("@garanti_sur", cbGaranti.Text);
                        cmd.Parameters.AddWithValue("@urunadi", txtUrunAdi.Text);
                        cmd.Parameters.AddWithValue("@miktari", int.Parse(txtMiktar.Text));
                        cmd.Parameters.AddWithValue("@alisfiyati", double.Parse(txtAlis.Text));
                        cmd.Parameters.AddWithValue("@satisfiyati", double.Parse(txtSatis.Text));
                        cmd.Parameters.AddWithValue("@foto", resimBytes);
                        cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Ürün Eklendi");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
                    }


                }
                else
                {
                    MessageBox.Show("Böyle bir Barkod No var", "Uyarı");
                }

                cbMarka.Items.Clear();
                pictureBox1.Image = null;
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

        private void btnYukle_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif|Tüm Dosyalar|*.*";
            openFileDialog1.Title = "Resim Seç";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string resimYolu = openFileDialog1.FileName;

                try
                {

                    pictureBox1.Image = new System.Drawing.Bitmap(resimYolu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yükleme hatası: " + ex.Message);
                }
            }
        }
        private Image ResizeImage(Image image, Size newSize)
        {
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, newSize.Width, newSize.Height);
            }
            return newImage;
        }

        private void UrunEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Urunler urun = new Urunler();
            urun.Show();
        }
    }
}
