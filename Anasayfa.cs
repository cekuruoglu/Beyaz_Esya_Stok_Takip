using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Anasayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #region Butonlar
        private void musteriBtn_Click(object sender, EventArgs e)
        {
            Musteriler musteri = new Musteriler();
            musteri.Show();
           
            
        }

        private void urunlerBtn_Click(object sender, EventArgs e)
        {
            Urunler urun = new Urunler();
            urun.Show();
        }

        private void satisBtn_Click(object sender, EventArgs e)
        {
            satismusteri satis = new satismusteri();
            Satislar satis1 = new Satislar();
            satis.Show();
            
        }

        private void faturaBtn_Click(object sender, EventArgs e)
        {
            iadeler fatura_rapor = new iadeler();
            fatura_rapor.Show();
        }

        private void cikisBtn_Click(object sender, EventArgs e)
        {
            kayitekrani kayit = new kayitekrani();
            kayit.Show();
        }

        private void iadeBtn_Click(object sender, EventArgs e)
        {
            satislistele satis = new satislistele();
            satis.Show();
        }

        
        #endregion

    }
}
