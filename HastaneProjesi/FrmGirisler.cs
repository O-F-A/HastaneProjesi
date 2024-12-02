using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProjesi
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void btnhastagirisi_Click(object sender, EventArgs e)
        {
            //HASTA GİRİŞ FORMUNU AÇABİLMEK İÇİN "frhasta" ADINDA BİR NESNE TÜRETTİK - TÜRETTİĞİMİZ NESNE İLE FORMU GÖSTER METODUNU ÇAĞIRIP GİRİŞLER FORMUNU GİZLEDİK 
            FrmHastaGiris frhasta = new FrmHastaGiris();           
            frhasta.Show();
            this.Hide();
        }

        private void btndoktorgirisi_Click(object sender, EventArgs e)
        {
            //DOKTOR GİRİŞ FORMUNU AÇABİLMEK İÇİN "frdoktor" ADINDA BİR NESNE TÜRETTİK - TÜRETTİĞİMİZ NESNE İLE FORMU GÖSTER METODUNU ÇAĞIRIP GİRİŞLER FORMUNU GİZLEDİK
            FrmDoktorGiris frdoktor = new FrmDoktorGiris();
            frdoktor.Show();
            this.Hide();


        }

        private void btnsekretergirisi_Click(object sender, EventArgs e)
        {
            //SEKRETER GİRİŞ FORMUNU AÇABİLMEK İÇİN "frsekreter" ADINDA BİR NESNE TÜRETTİK - TÜRETTİĞİMİZ NESNE İLE FORMU GÖSTER METODUNU ÇAĞIRIP GİRİŞLER FORMUNU GİZLEDİK
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();

        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}
