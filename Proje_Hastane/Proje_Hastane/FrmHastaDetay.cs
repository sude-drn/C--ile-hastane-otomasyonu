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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbağlantisi bgl = new sqlbağlantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            //AdSoyad çekme
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);//lbl tc de yazan değere eşit olan adı soyadı getirir.
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];

            }
            bgl.baglanti().Close();

            //Randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(" Select * From Tbl_Randevular where HastaTC=" + tc, bgl.baglanti());
            da.Fill(dt); //tablodan gelecek değerle data adaptörünün içinde olur sanal tablo oluşturma mantığı...
            dataGridView1.DataSource = dt;// veri kaynağı=dataveriden gelen değer.




            //Branş Çekme
            SqlCommand komut2 = new SqlCommand(" Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);//0. indeksteki 1 tane sütunu döndürdük.


            }
            bgl.baglanti().Close();



        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //branşa uygun doktor çekme
            CmbDoktor.Items.Clear();//branşı seçtiğinde uygun doktorların tekrarlanmadan gelmesi için bir öncekileri sildik.
            SqlCommand komut3 = new SqlCommand(" Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+ " " + dr3[1]);
            }
            bgl.baglanti().Close();


        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(" Select * From Tbl_Randevular where RandevuBrans='" + CmbBrans.Text+ " '", bgl.baglanti() );
            da.Fill( dt );
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDüzenle fr = new FrmBilgiDüzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            //int secilen = dataGridView2.Rows[secilen].Cells[0].Value.ToString(); 
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();


        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(" update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3" ,bgl.baglanti());
            komut.Parameters.AddWithValue("p1", LblTC.Text);
            komut.Parameters.AddWithValue("@p2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3" , Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(" Randevu Alındı..."," uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
