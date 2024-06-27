using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beyaz_esya_stok_takip
{
    class Deneme
    {
        baglanti baglanti = new baglanti();
        private DataTable dataokuma()
        {
            SqlConnection con = new SqlConnection(baglanti.con);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from urun", con);
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable verigetir()
        {
            try
            {
                return dataokuma();
            }
            catch (Exception e )
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return null;
            }
        }

    }
}
