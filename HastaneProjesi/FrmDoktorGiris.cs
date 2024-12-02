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


namespace HastaneProjesi
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent(); // Form bileşenlerini başlatma
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını tutacak nesne

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            // Doktorun TC ve şifresini kontrol etmek için SQL sorgusu hazırlama
            SqlCommand komut = new SqlCommand("select DoktorTC, DoktorSifre from Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text); // TC'yi sorguya ekleme
            komut.Parameters.AddWithValue("@p2", txtsifre.Text); // Şifreyi sorguya ekleme

            // Sorguyu çalıştırma ve sonucu kontrol etme
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) // Eğer veritabanından geçerli bir kayıt dönerse
            {
                // Doktor detaylarına yönlendirme
                FrmDoktorDetay frmdoktor = new FrmDoktorDetay(); // Yeni doktor detayları formu oluşturma
                frmdoktor.TC = msktc.Text; // Doktorun TC bilgisini detay formuna aktar
                frmdoktor.Show(); // Detay formunu göster
                this.Hide(); // Şu anki formu gizle
            }
            else
            {
                // Geçersiz giriş durumunda uyarı mesajı gösterme
                MessageBox.Show("Hatalı Giriş! TC & Şifre Hatalı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapatma
        }

        private void btncikisyap_Click(object sender, EventArgs e)
        {
            // Çıkış yapıldığında giriş ekranına yönlendirme
            FrmGirisler girisler = new FrmGirisler(); // Yeni giriş ekranı oluşturma
            girisler.Show(); // Giriş ekranını göster
            this.Close(); // Şu anki formu kapatma
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}

