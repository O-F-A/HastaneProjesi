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
    public partial class FrmUyeKayit : Form
    {
        public FrmUyeKayit()
        {
            InitializeComponent();
        }
        // Veritabanı bağlantısı için gerekli sınıfın örneği.
        sqlbaglantisi bagla = new sqlbaglantisi();

        // "Kayıt Yap" butonuna tıklandığında çalışacak metot.
        private void btnkayıtyap_Click(object sender, EventArgs e)
        {
            // **HASTA KAYIT EKLEME**

            // Yeni hasta bilgilerini Tbl_Hastalar tablosuna eklemek için SQL sorgusu hazırlıyoruz.
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", bagla.baglanti());
            // Formdan alınan bilgiler SQL sorgusuna parametre olarak ekleniyor.
            komut.Parameters.AddWithValue("@p1", txtad.Text); // Hasta adı
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text); // Hasta soyadı
            komut.Parameters.AddWithValue("@p3", msktc.Text); // Hasta TC kimlik numarası
            komut.Parameters.AddWithValue("@p4", msktelno.Text); // Hasta telefon numarası
            komut.Parameters.AddWithValue("@p5", txtsifre.Text); // Hasta şifresi
            komut.Parameters.AddWithValue("@p6", cmbcinsiyet.Text); // Hasta cinsiyeti

            // SQL sorgusunu çalıştırarak veritabanına yeni kayıt ekliyoruz.
            komut.ExecuteNonQuery();

            // Veritabanı bağlantısını kapatıyoruz.
            bagla.baglanti().Close();

            // Kullanıcıya kaydın başarıyla tamamlandığına dair bilgi mesajı gösteriyoruz.
            MessageBox.Show("Kaydınız başarıyla tamamlanmıştır - Şifreniz: " + txtsifre.Text, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
