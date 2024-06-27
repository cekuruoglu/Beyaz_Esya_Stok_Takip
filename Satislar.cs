using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class Satislar : Form
    {
        public Satislar()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        DataSet daset = new DataSet();
        satismusteri smusteri = new satismusteri();
        private void UrunListele()
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun", con);
                adtr.Fill(daset, "urun");
                dataGridView1.DataSource = daset.Tables["urun"];
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }
        public void SepetListele()
        {
            if (!daset.Tables.Contains("sepet"))
            {
                daset.Tables.Add("sepet");
            }
            else
            {
                daset.Tables["sepet"].Clear();
            }

            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, garanti_sur, miktari, satis_fiyat, toplam_fiyat from sepet", con);
                adtr.Fill(daset, "sepet");
                dataGridView2.DataSource = daset.Tables["sepet"];
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }

        }
        public void hesapla()
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("select sum(toplam_fiyat) from sepet", con);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value) // Eğer sonuç null değilse işlem yap
                {
                    double Top = Convert.ToDouble(result); // object'i double'a dönüştür
                    lblGenelToplam.Text = Top.ToString("F2") + " TL";
                    lblToplam.Text = (Top - (Top * 0.18)).ToString("F2") + " TL"; 
                }
                else
                {
                    lblGenelToplam.Text = " TL";
                    lblToplam.Text =" TL";
                }
                
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu " + ex.Message, "Uyarı!!");
            }

        }

        private void Satislar_Load(object sender, EventArgs e)
        {
            UrunListele();
            SepetListele();
            kategorigetir();
            hesapla();


        }
        public void musteribilgi(string adsoyad, string adres, string telefon, string tc)
        {
            lblAdSoyad.Text = adsoyad;
            lblTelefon.Text = telefon;
            txtAdres1.Text = adres;
            lblTc.Text = tc;
        }
    
        
        private void kategorigetir()
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
        
        
        #region Filtreleme
        private void cbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKategori.Text == "")
            {
                cbMarka.Items.Clear();
                cbMarka.Text = "";
                UrunListele();
            }
            else
            {
              
                cbMarka.Items.Clear();
                cbMarka.Text = "";
                try
                {
                    DataTable table = new DataTable();
                    SqlConnection con = new SqlConnection(baglanti.con);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from markabilgileri where kategori='" + cbKategori.SelectedItem + "'", con);
                    SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where kategori= '" + cbKategori.Text + "'", con);
                    adtr.Fill(table);
                    dataGridView1.DataSource = table;
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
            
        }
        
        private void cbMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where marka like '%" + cbMarka.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void cbEnerji_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where enerji_sinif like '%" + cbEnerji.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void cbGaranti_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where garanti_sur like '%" + cbGaranti.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtBarkodNo_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where barkodno like '%" + txtBarkodNo.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtUrunAdi_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where urunadi like '%" + txtUrunAdi.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void cbKategori_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select barkodno, kategori, marka, urunadi, enerji_sinif, garanti_sur, satisfiyati, foto from urun where kategori like '%" + cbKategori.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        #endregion

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            byte[] fotoByteDizisi = GetFotoFromDatabase(); 

            if (fotoByteDizisi != null && fotoByteDizisi.Length > 0)
            {
                // Yeni formu oluştur ve fotoğrafı göster
                FotoForm(fotoByteDizisi);
            }
            else
            {
                MessageBox.Show("Fotoğraf bulunamadı.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilen = cbOdeme.SelectedItem.ToString();
            if(secilen == "Kredi Kartı")
            {
                lblTaksit.Visible = true;
                cbTaksit.Visible = true;
            }
            else
            {
                lblTaksit.Visible = false;
                cbTaksit.Visible = false;
                lblAylik.Visible = false;
                lblAylikTutar.Visible = false;
            }
        }

        private void cbTaksit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilen = cbTaksit.SelectedItem.ToString();
            int genelToplam = 0;
            using (SqlConnection con = new SqlConnection(baglanti.con))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(toplam_fiyat), 0) FROM sepet", con);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    genelToplam = Convert.ToInt32(result);
                }
                con.Close();
            }



            switch (secilen)
                {
                    case "Tek Çekim":
                        lblAylik.Text = lblGenelToplam.Text;
                    lblToplam.Text = (genelToplam - (genelToplam * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                        break;
                    case "2 Taksit":
                        lblAylik.Text = (genelToplam / 2).ToString("F2") + " TL";
                    lblGenelToplam.Text = (genelToplam).ToString("F2") + " TL";
                    lblToplam.Text = (genelToplam - (genelToplam * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                    case "3 Taksit":
                        lblAylik.Text = (genelToplam / 3).ToString("F2") + " TL";
                    lblGenelToplam.Text = (genelToplam).ToString("F2") + " TL";
                    lblToplam.Text = (genelToplam - (genelToplam * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                    case "4 Taksit":
                        lblAylik.Text = (genelToplam / 4).ToString("F2") + " TL";
                    lblGenelToplam.Text = (genelToplam).ToString("F2") + " TL";
                    lblToplam.Text = (genelToplam - (genelToplam * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                    case "5 Taksit":
                        lblAylik.Text = (genelToplam / 5).ToString("F2") + " TL";
                    lblGenelToplam.Text = (genelToplam).ToString("F2") + " TL";
                    lblToplam.Text = (genelToplam - (genelToplam * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                    case "6 Taksit":
                    lblAylik.Text = ( (genelToplam * 0.04) / 6).ToString("F2") + " TL";
                    double Top6 = ((genelToplam * 0.04) + genelToplam);
                    lblGenelToplam.Text = Top6.ToString("F2") + " TL";
                    lblToplam.Text = (Top6 - (Top6 * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                case "7 Taksit":
                    lblAylik.Text = ((genelToplam * 0.07) / 7).ToString("F2") + " TL";
                    double Top7 = ((genelToplam * 0.07) + genelToplam);
                    lblGenelToplam.Text = Top7.ToString("F2") + " TL";
                    lblToplam.Text = (Top7 - (Top7 * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                case "8 Taksit":
                    lblAylik.Text = ((genelToplam * 0.11) / 8).ToString("F2") + " TL";
                    double Top8 = ((genelToplam * 0.11) + genelToplam);
                    lblGenelToplam.Text = Top8.ToString("F2") + " TL";
                    lblToplam.Text = (Top8 - (Top8 * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                case "9 Taksit":
                    lblAylik.Text = ((genelToplam * 0.14) / 9).ToString("F2") + " TL";
                    double Top9 = ((genelToplam * 0.14) + genelToplam);
                    lblGenelToplam.Text = Top9.ToString("F2") + " TL";
                    lblToplam.Text = (Top9 - (Top9 * 0.18)).ToString("F2") + " TL";
                    lblAylik.Visible = true;
                    lblAylikTutar.Visible = true;
                    break;
                default:
                        break;
                }
            
            
        }

        private void SatisYap()
        {
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                try
                {
                    SqlConnection con1 = new SqlConnection(baglanti.con);
                    con1.Open();
                    SqlCommand cmd = new SqlCommand("insert into satis(tc,adsoyad,telefon,adres,barkodno,kategori,marka,urunadi,miktari,satis_fiyat,toplam_fiyat,odeme_tur,taksit_sayisi,tarih,garanti_sur) values(@tc,@adsoyad,@telefon,@adres,@barkodno,@kategori,@marka,@urunadi,@miktari,@satis_fiyat,@toplam_fiyat,@odeme_tur,@taksit_sayisi,@tarih,@garanti_sur)", con1);
                    cmd.Parameters.AddWithValue("@tc", lblTc.Text);
                    cmd.Parameters.AddWithValue("@adsoyad", lblAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@telefon", lblTelefon.Text);
                    cmd.Parameters.AddWithValue("@adres", txtAdres1.Text);
                    cmd.Parameters.AddWithValue("@kategori", dataGridView2.Rows[i].Cells["kategori"].Value.ToString());
                    cmd.Parameters.AddWithValue("@marka", dataGridView2.Rows[i].Cells["marka"].Value.ToString());
                    cmd.Parameters.AddWithValue("@barkodno", dataGridView2.Rows[i].Cells["barkodno"].Value.ToString());
                    cmd.Parameters.AddWithValue("@urunadi", dataGridView2.Rows[i].Cells["urunadi"].Value.ToString());
                    cmd.Parameters.AddWithValue("@garanti_sur", dataGridView2.Rows[i].Cells["garanti_sur"].Value.ToString());
                    cmd.Parameters.AddWithValue("@miktari", int.Parse(dataGridView2.Rows[i].Cells["miktari"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@satis_fiyat", double.Parse(dataGridView2.Rows[i].Cells["satis_fiyat"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@toplam_fiyat", double.Parse(dataGridView2.Rows[i].Cells["toplam_fiyat"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@odeme_tur", cbOdeme.Text);
                    cmd.Parameters.AddWithValue("@taksit_sayisi", cbTaksit.Text);
                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    // Satılan ürün miktarını stoktan düşme
                    SqlCommand cmd2 = new SqlCommand("update urun set miktari=miktari-'" + int.Parse(dataGridView2.Rows[i].Cells["miktari"].Value.ToString()) + "' where barkodno='" + dataGridView2.Rows[i].Cells["barkodno"].Value.ToString() + "'", con1);
                    cmd2.ExecuteNonQuery();
                    con1.Close();
                    MessageBox.Show("Satış gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveInvoiceAsPdf();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Hata Oluştu " + ex.Message, "Uyarı!!");
                }
                try
                {

                    SqlConnection con = new SqlConnection(baglanti.con);
                    con.Open();
                    SqlCommand cmd3 = new SqlCommand("delete from sepet", con);
                    cmd3.ExecuteNonQuery();
                    con.Close();
                    daset.Tables["sepet"].Clear();
                    SepetListele();
                    hesapla();
                    this.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Hata Oluştu " + ex.Message, "Uyarı!!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Satış yapılsın mı?", "Doğrulama", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (cbOdeme.SelectedIndex == 0)
                {
                    if (cbTaksit.SelectedIndex == -1)
                    {
                        MessageBox.Show("Lütfen taksit sayısını seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        SatisYap();
                    }
                }
                else if (cbOdeme.SelectedIndex == 1)
                {
                    SatisYap();
                }
                else
                {
                    MessageBox.Show("Lütfen Ödeme Türünü Giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
                
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from sepet where barkodno='" + dataGridView2.CurrentRow.Cells["barkodno"].Value.ToString() + "'", con); // Seçilen ürünü sepetten kaldır
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Ürün sepetten çıkarıldı!");
                daset.Tables["sepet"].Clear(); // Yeniden sepeti listele ve toplam fiyatı hesapla
                SepetListele();
                hesapla();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            BarkodNotxt.Text = dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString();
            Kategoritxt.Text = dataGridView1.CurrentRow.Cells["kategori"].Value.ToString();
            markatxt.Text = dataGridView1.CurrentRow.Cells["marka"].Value.ToString();
            UrunAditxt.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            txtSatis.Text = dataGridView1.CurrentRow.Cells["satisfiyati"].Value.ToString();
            txtGaranti.Text = dataGridView1.CurrentRow.Cells["garanti_sur"].Value.ToString();
            sepetekle sepetekle = new sepetekle();
            sepetekle.sepetekleme(lblTc.Text, lblAdSoyad.Text, lblTelefon.Text, txtAdres1.Text, BarkodNotxt.Text, Kategoritxt.Text, markatxt.Text, UrunAditxt.Text, txtSatis.Text,txtGaranti.Text);
            sepetekle.Show();
        }
        private byte[] GetFotoFromDatabase()
        {
            
            byte[] fotoByteDizisi = null;
            using (SqlConnection con = new SqlConnection(baglanti.con))
            {
                string query = "SELECT foto FROM urun WHERE barkodno = @barkodno"; 
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString()); 
                con.Open();
                fotoByteDizisi = (byte[])command.ExecuteScalar();
                con.Close();
            }
            return fotoByteDizisi;
        }
        private void FotoForm(byte[] fotoByteDizisi)
        {
            // Yeni formu oluştur
            Form fotoform = new Form();
            fotoform.Size = new Size(512, 423);
            fotoform.Text = "Ürün Fotoğrafı";
            fotoform.StartPosition = FormStartPosition.CenterScreen;

            // PictureBox kontrolünü oluştur
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(512, 423);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Resmi PictureBox boyutuna göre otomatik olarak boyutlandır
            fotoform.Controls.Add(pictureBox);

            // Byte dizisinden Image nesnesi oluştur
            using (MemoryStream ms = new MemoryStream(fotoByteDizisi))
            {
                System.Drawing.Image foto = System.Drawing.Image.FromStream(ms);
                pictureBox.Image = foto;
            }

            // Yeni formu göster
            fotoform.ShowDialog(); // Ana formun kullanılmasını engelleyen bir pencere açar
        }
        private void SaveInvoiceAsPdf()
        {

                // Dosya adı oluşturma
                string tc = lblTc.Text;
                string adSoyad = lblAdSoyad.Text.Replace(" ", "_"); // Boşlukları alt çizgi ile değiştir
                string tarih = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"); // Tarihi formatlayarak al
                string fileName = $"{tc}_{adSoyad}_{tarih}.pdf";

                // Masaüstü yolunu al
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, fileName);

                // Yeni bir PDF dökümanı oluştur
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Fatura başlığı
                BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", "windows-1254", true);
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font regularFont = new iTextSharp.text.Font(baseFont, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font bilgiFont = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font sirketFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.ITALIC);
            Paragraph sirket = new Paragraph("KARAKÖY BEYAZ EŞYA", sirketFont);
            Paragraph title = new Paragraph("Fatura", titleFont);
            // Yeni bir tablo oluştur
            PdfPTable headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;

            // İlk hücreye sirket bilgisi paragrafını ekle
            PdfPCell sirketCell = new PdfPCell(sirket);
            sirketCell.Border = PdfPCell.NO_BORDER;
            headerTable.AddCell(sirketCell);

            // İkinci hücreye fatura başlığı paragrafını ekle
            PdfPCell titleCell = new PdfPCell(title);
            titleCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            titleCell.Border = PdfPCell.NO_BORDER;
            headerTable.AddCell(titleCell);

            // Tabloyu belgeye ekle
            document.Add(headerTable);
            
            Paragraph adres = new Paragraph("Çelebi Mah. Gülderen sokak, No: 13/A", regularFont);
            Paragraph sehir = new Paragraph("Manisa/Şehzadeler, 45110", regularFont);
            Paragraph telefon = new Paragraph("Telefon: +90-507-135-68-90", regularFont);
            Paragraph _tarih = new Paragraph("Tarih: " + DateTime.Now.ToString(), regularFont);

            PdfPTable bilgiTable = new PdfPTable(2);
            bilgiTable.WidthPercentage = 100;

            PdfPCell adresCell = new PdfPCell(adres);
            adresCell.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(adresCell);

            PdfPCell bosluk1 = new PdfPCell(new Phrase("", regularFont));
            bosluk1.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(bosluk1);

            PdfPCell sehirCell = new PdfPCell(new Phrase("Manisa/Şehzadeler, 45110", regularFont));
            sehirCell.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(sehirCell);

            PdfPCell tarihCell = new PdfPCell(_tarih);
            tarihCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tarihCell.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(tarihCell);

            PdfPCell telefonCell = new PdfPCell(telefon);
            telefonCell.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(telefonCell);

            PdfPCell bosluk2 = new PdfPCell(new Phrase("", regularFont));
            bosluk2.Border = PdfPCell.NO_BORDER;
            bilgiTable.AddCell(bosluk2);

            document.Add(bilgiTable);

            document.Add(new Paragraph("\n"));
            // Fatura bilgileri
            document.Add(new Paragraph("Müşteri TC: " + lblTc.Text, bilgiFont));
                document.Add(new Paragraph("Ad Soyad: " + lblAdSoyad.Text, bilgiFont));
                document.Add(new Paragraph("Telefon: " + lblTelefon.Text, bilgiFont));
                document.Add(new Paragraph("Adres: " + txtAdres1.Text, bilgiFont));
                document.Add(new Paragraph("Ödeme Türü: " + cbOdeme.Text, bilgiFont));
                document.Add(new Paragraph("Taksit Sayısı: " + cbTaksit.Text, bilgiFont));
                document.Add(new Paragraph("\n"));

                // Ürün bilgileri başlıkları
                PdfPTable table = new PdfPTable(8);
                table.DefaultCell.Padding = 5; // Hücre içeriği etrafındaki boşluğu ayarla
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER; // Hücre içeriğini ortala
                table.WidthPercentage = 100; // Tablo genişliğini %100 yap

                // Tablo başlıklarını ayarla
                string[] headers = { "Barkod No","Kategori", "Marka", "Ürün Adı","Garanti Süresi" ,"Miktar", "Satış Fiyatı", "Toplam Fiyat" };
                foreach (string header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, regularFont));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY; // Başlık hücrelerini gri arka planla ayarla
                    table.AddCell(cell);
                }

                // Ürün bilgilerini tabloya ekle
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["barkodno"].Value.ToString(), regularFont)));
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["kategori"].Value.ToString(), regularFont)));
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["marka"].Value.ToString(), regularFont)));
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["urunadi"].Value.ToString(), regularFont)));
                table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["garanti_sur"].Value.ToString(), regularFont)));
                table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["miktari"].Value.ToString(), regularFont)));
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["satis_fiyat"].Value.ToString(), regularFont)));
                    table.AddCell(new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells["toplam_fiyat"].Value.ToString(), regularFont)));
                }
                table.SpacingAfter = 20f;
                document.Add(table);
                
                Paragraph genelToplamParagraf = new Paragraph("Genel Toplam: " + lblGenelToplam.Text, regularFont);
                Paragraph vergisizToplam = new Paragraph("Toplam: " + lblToplam.Text, regularFont);
                Paragraph kdv = new Paragraph("KDV: 18% ", regularFont);
                Chunk line = new Chunk(new LineSeparator(0.0f, 100.0f, BaseColor.BLACK, Element.ALIGN_RIGHT, 1));
                Paragraph lineParagraph = new Paragraph(line);
                genelToplamParagraf.Alignment = Element.ALIGN_RIGHT;
                kdv.Alignment = Element.ALIGN_RIGHT;
                vergisizToplam.Alignment = Element.ALIGN_RIGHT;
                document.Add(vergisizToplam);
                document.Add(kdv);
                document.Add(lineParagraph);
                document.Add(genelToplamParagraf);

                document.Close();

            Process.Start(filePath);

            MessageBox.Show("Fatura masaüstüne kaydedildi: " + filePath);
           

            
        }

        private void Satislar_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from sepet ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata Oluştu" + ex.Message, "Uyarı!!");
            }
        }
    }

}
