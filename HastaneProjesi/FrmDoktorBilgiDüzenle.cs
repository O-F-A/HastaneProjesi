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
    public partial class FrmDoktorBilgiDüzenle : Form
    {
        public FrmDoktorBilgiDüzenle()
        {
            InitializeComponent(); // Form bileşenlerini başlatma (tasarım kısmı)
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını tutacak nesne

        public string TCno; // Doktorun TC kimlik numarasını tutacak public değişken

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            // Doktor bilgilerini güncelleme işlemi
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1 , DoktorSoyad=@d2 , DoktorBrans=@d3, DoktorSifre=@d4 where DoktorTC=@d5", bagla.baglanti());

            // Veritabanındaki ilgili alanlara girilen değerleri atama
            komut.Parameters.AddWithValue("@d1", txtad.Text); // Doktorun adı
            komut.Parameters.AddWithValue("@d2", txtsoyad.Text); // Doktorun soyadı
            komut.Parameters.AddWithValue("@d3", cmbbrans.Text); // Doktorun branşı
            komut.Parameters.AddWithValue("@d4", txtsifre.Text); // Doktorun şifresi
            komut.Parameters.AddWithValue("@d5", msktc.Text); // Doktorun TC kimlik numarası

            komut.ExecuteNonQuery(); // Sorguyu çalıştır ve veritabanında güncelleme işlemi yap
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapat

            MessageBox.Show("Bilgileriniz Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); // Bilgilerin güncellendiğini kullanıcıya bildir
        }

        private void FrmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde doktorun bilgilerini çekip, form alanlarına doldurma
            msktc.Text = TCno; // Doktorun TC bilgisini maskelediğimiz alana aktar

            // Branşları combobox'a yükleme
            SqlCommand komutgetir = new SqlCommand("select BransAd from Tbl_Brans", bagla.baglanti());
            SqlDataReader drgetir = komutgetir.ExecuteReader(); // Branşları veritabanından çek

            // Branşları combobox'a ekleyerek kullanıcıya seçenek sunma
            while (drgetir.Read())
            {
                cmbbrans.Items.Add(drgetir[0]); // Branşları combobox'a ekle
            }
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapat

            // Doktorun mevcut bilgilerini form alanlarına yükleme
            SqlCommand komut1 = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=" + TCno, bagla.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader(); // Veritabanından doktorun bilgilerini çek

            // Veritabanından gelen bilgilerle formu doldurma
            while (dr1.Read())
            {
                txtad.Text = dr1[1].ToString(); // Doktorun adını textbox'a yazdır
                txtsoyad.Text = dr1[2].ToString(); // Doktorun soyadını textbox'a yazdır
                cmbbrans.Text = dr1[3].ToString(); // Doktorun branşını combobox'a yazdır
                txtsifre.Text = dr1[5].ToString(); // Doktorun şifresini textbox'a yazdır
            }
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapat
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            // Formu temizleme işlemi
            txtad.Text = ""; // Ad alanını temizle
            txtsoyad.Text = ""; // Soyad alanını temizle
            cmbbrans.Text = ""; // Branş combobox'ını temizle
            txtsifre.Text = ""; // Şifre alanını temizle
        }
    }
}

