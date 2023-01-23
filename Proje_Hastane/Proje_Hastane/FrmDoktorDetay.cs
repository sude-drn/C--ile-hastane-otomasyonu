using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlbağlantisi bgl= new sqlbağlantisi();
        public String TC;
        private void FmDoktorDetay_Load(object sender, EventArgs e)
        {   //forma doktorun tc sini yazdırma.
            LblTC.Text = TC;


            //Doktor Ad Soyad çekme.
            SqlCommand komut = new SqlCommand(" select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];


            }
            bgl.baglanti().Close();

            //Doktora ait Randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Randevular where RandevuDoktor='"+ LblAdSoyad.Text+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource= dt;


        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle fr = new FrmDoktorBilgiDüzenle();
            fr.TCNO= LblTC.Text;
            fr.Show();

        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();

        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text= dataGridView1.Rows[secilen].Cells[7].Value.ToString();

        }
        //yeni
        private void BtnDuyurular_Click_1(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            FrmDoktorBilgiDüzenle fr = new FrmDoktorBilgiDüzenle();
            fr.TCNO = LblTC.Text;
            fr.Show();

        }

        private void BtnCikis_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RchSikayet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
