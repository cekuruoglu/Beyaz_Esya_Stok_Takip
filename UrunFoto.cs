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
    public partial class UrunFoto : Form
    {
        Urunler urunler = new Urunler();
        DataSet daset = new DataSet();
        // Constructor, resmi parametre olarak alır ve PictureBox'a yükler
        public UrunFoto(string barkodNo, byte[] resimBytes)
        {
            InitializeComponent();
            lblBarkodNo.Text = barkodNo;
            pictureBox1.Image = ByteArrayToImage(resimBytes);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Resmi PictureBox'a sığacak şekilde ayarla
        }
        baglanti baglanti = new baglanti();
        // PictureBox'a resmi yüklemek için yardımcı fonksiyon
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null) return null;

            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Resmi güncellemek istediğinizden emin misiniz?", "Güncelleme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanıcı evet derse devam et
            if (result == DialogResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Resim Dosyaları (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Tüm Dosyalar (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın yolunu al
                    string resimPath = openFileDialog.FileName;

                    // Seçilen dosyayı PictureBox'a yükle
                    Image originalImage = Image.FromFile(resimPath);
                    Image resizedImage = ResizeImage(originalImage, 300, 200);

                    // PictureBox'a yüklenmiş resmi göster
                    pictureBox1.Image = resizedImage;

                    // Resmi SQL tablosunda güncelle
                    ResmiGuncelleInSQL(resizedImage);
                }
            }
        }
        private void ResmiGuncelleInSQL(Image image)
        {
            try
            {
                // Resmi byte dizisine çevir
                // Resmi byte dizisine çevir
                byte[] resimBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    resimBytes = ms.ToArray();
                }

                // SQL bağlantısı oluştur ve güncelleme komutunu çalıştır
                using (SqlConnection con = new SqlConnection(baglanti.con))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("UPDATE urun SET foto = @foto WHERE barkodno = @barkodno", con);
                    command.Parameters.AddWithValue("@foto", resimBytes);
                    command.Parameters.AddWithValue("@barkodno", lblBarkodNo.Text);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Resim başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Image ResizeImage(Image image, int width, int height)
        {
            // Yeni boyutları belirle
            Size newSize = new Size(width, height);

            // Yeni boyutta bir Bitmap oluştur
            Bitmap newBitmap = new Bitmap(newSize.Width, newSize.Height);

            // Yeni Bitmap üzerine eski resmi çiz
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.DrawImage(image, new Rectangle(Point.Empty, newSize));
            }

            return newBitmap;
        }

        private void UrunFoto_FormClosing(object sender, FormClosingEventArgs e)
        {
            Urunler urunlerForm = Application.OpenForms["Urunler"] as Urunler;

            // Ürünler formu null değilse ve ÜrünListele fonksiyonu tanımlıysa çağır
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from urun", con);
                adtr.Fill(daset, "urun");
                urunlerForm.dataGridView1.DataSource = daset.Tables["urun"];
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }
    }
}
