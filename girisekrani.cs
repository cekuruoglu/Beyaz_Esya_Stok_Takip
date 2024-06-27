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
    public partial class girisekrani : Form
    {
        public girisekrani()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=CEMEREN\\SQLEXPRESS;Initial Catalog=Beyaz_Esya;Integrated Security=True"; // SQL Server bağlantı dizesi
            string query = "SELECT COUNT(1) FROM kullanicibilgileri WHERE kullaniciadi=@kullaniciadi AND sifre=@sifre";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@kullaniciadi", txtKAdi.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 1)
                    {
                        MessageBox.Show("Giriş Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Anasayfa anasayfa = new Anasayfa();
                        anasayfa.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Giriş Başarısız!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
