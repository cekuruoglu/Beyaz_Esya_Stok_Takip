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
    public partial class iadeler : Form
    {
        public iadeler()
        {
            InitializeComponent();
        }
        baglanti baglanti = new baglanti();
        DataSet daset = new DataSet();

        private void FaturaRapor_Load(object sender, EventArgs e)
        {
            kategorigetir();
            iadeListele();
        }

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
        public void iadeListele()
        {
            if (!daset.Tables.Contains("iade"))
            {
                daset.Tables.Add("iade");
            }
            else
            {
                daset.Tables["iade"].Clear();
            }
            try
            {
                SqlConnection con = new SqlConnection(baglanti.con);
                con.Open();
                SqlDataAdapter adtr = new SqlDataAdapter("select * from iade", con);
                adtr.Fill(daset, "iade");
                dataGridView1.DataSource = daset.Tables["iade"];
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
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where kategori like '%" + KategoriCB.Text + "%'", con);
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
                iadeListele();
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
                    SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where kategori= '" + KategoriCB.Text + "'", con);
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
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where marka like '%" + MarkaCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void OdemeCB_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where odeme_tur like '%" + OdemeCB.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void UrunAdiTXT_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where urunad like '%" + UrunAdiTXT.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtTc_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where tc like '%" + txtTc.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtAdSoyad_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where adsoyad like '%" + txtAdSoyad.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where telefon like '%" + txtTelefon.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }
        private void BarkodNotxt_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where barkodno like '%" + BarkodNotxt.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from iade where iade_sebep like '%" + comboBox1.Text + "%'", con);
            adtr.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }


        #endregion


    }
}
