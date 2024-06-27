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
    public partial class kayitekrani : Form
    {
        public kayitekrani()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKAdi.Text))
            {
                MessageBox.Show("Kullanıcı adı boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Şifre boş olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection con1 = new SqlConnection(baglanti.con);
            con1.Open();
            SqlCommand cmd = new SqlCommand("insert into kullanicibilgileri(kullaniciadi,sifre) values (@kullaniciadi,@sifre)", con1);
            cmd.Parameters.AddWithValue("@kullaniciadi", txtKAdi.Text);
            cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
            cmd.ExecuteNonQuery();
            con1.Close();
            MessageBox.Show("Kayıt Oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
