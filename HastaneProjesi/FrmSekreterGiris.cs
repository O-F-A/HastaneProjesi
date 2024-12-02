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
    public partial class FrmSekreterGiris : Form
    {
        // FrmSekreterGiris formunun constructor'ı. Form bileşenlerini başlatır.
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        // Veritabanı bağlantısı için gerekli sınıfın örneği.
        sqlbaglantisi bagla = new sqlbaglantisi();

        // "Giriş Yap" butonuna tıklandığında çalışacak metot.
        private void btngirisyap_Click(object sender, EventArgs e)
        {
            // **SEKRETER GİRİŞ KONTROLÜ**

            // Sekreter TC ve şifre bilgilerini doğrulamak için SQL sorgusu hazırlanır.
            SqlCommand komut = new SqlCommand("select SekreterTC, SekreterSifre from Tbl_Sekreter where SekreterTC=@p1 AND SekreterSifre=@p2", bagla.baglanti());

            // Kullanıcı tarafından girilen TC ve şifre bilgileri parametre olarak eklenir.
            komut.Parameters.AddWithValue("@p1", msktc.Text); // Sekreter TC kimlik numarası
            komut.Parameters.AddWithValue("@p2", txtsifre.Text); // Sekreter şifresi

            // Sorgu çalıştırılır ve sonuçlar bir SqlDataReader nesnesine atanır.
            SqlDataReader dr = komut.ExecuteReader();

            // Eğer sonuç dönerse (yani TC ve şifre doğruysa):
            if (dr.Read())
            {
                // Sekreter detay formu açılır.
                FrmSekreterDetay frmSekreter = new FrmSekreterDetay();

                // Girilen TC bilgisi detay formuna aktarılır (sekreterin bilgilerini göstermek için).
                frmSekreter.tckimlik = msktc.Text;

                // Sekreter detay formunu göster.
                frmSekreter.Show();

                // Mevcut giriş formunu gizle.
                this.Hide();
            }
            else
            {
                // Eğer sonuç dönmezse, hata mesajı göster.
                MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı!", "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Veritabanı bağlantısını kapat.
            bagla.baglanti().Close();
        }

        // "Çıkış Yap" butonuna tıklandığında çalışacak metot.
        private void btncikisyap_Click(object sender, EventArgs e)
        {
            // Girişler formunu açar.
            FrmGirisler girisler = new FrmGirisler();
            girisler.Show();

            // Mevcut formu kapatır.
            this.Close();
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}

