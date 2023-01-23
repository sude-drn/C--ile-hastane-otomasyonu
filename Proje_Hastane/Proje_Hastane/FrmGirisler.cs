namespace Proje_Hastane
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void BtnHastagirisi_Click(object sender, EventArgs e)
        {
            frmHastaGiriþ fr = new frmHastaGiriþ();
            fr.Show();
            this.Hide();
            
        }

        private void BtnSekreterGirisi_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnDoktorGirisi_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris fr = new FrmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void FrmGirisler_Load(object sender, EventArgs e)
        {

        }
    }
}