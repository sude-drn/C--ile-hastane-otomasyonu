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
    public partial class frmHastaGiriş : Form
    {
        public frmHastaGiriş()
        {
            InitializeComponent();
        }
        sqlbağlantisi bgl = new sqlbağlantisi();

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit(); 
            fr.Show();
               
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(" Select * From Tbl_Hastalar Where HastaTc=@p1 and HastaSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();// komuttan gelen değerleri oku.
            if(dr.Read())
            {
                FrmHastaDetay fr = new FrmHastaDetay();
                fr.tc= MskTC.Text;//hasta detay formundaki tc nosunu bu forma atadık
                fr.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show(" Hatalı TC & Şifre!");

            }
            bgl.baglanti().Close();



        }

        private void frmHastaGiriş_Load(object sender, EventArgs e)
        {

        }
    }
}
