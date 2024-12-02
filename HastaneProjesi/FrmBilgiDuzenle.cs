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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        // Veritabanı bağlantısı için gerekli sınıfın örneği.
        sqlbaglantisi bagla = new sqlbaglantisi();

        // Hasta detay formlardan alınan hasta TC bilgisini saklamak için bir değişken.
        public string TCno;

        // Form yüklendiğinde çalışacak metot.
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {

            // **VERİ TABANINDAN HASTA BİLGİLERİNİ GETİRME**

            // TC kimlik numarasını ilgili masked textbox'a yazdırıyoruz.
            msktc.Text = TCno;

            // SQL sorgusu ile ilgili hastanın bilgilerini veritabanından çekiyoruz.
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", TCno);// Parametre olarak TC kimlik numarasını TCno değişkeninden almasını istiyoruz.
            SqlDataReader dr = komut.ExecuteReader(); // SQL sorgusunu çalıştırıyoruz.

            // Veritabanından dönen bilgileri formdaki ilgili alanlara dolduruyoruz.
            while (dr.Read()) 
            {
                txtid.Text = dr[0].ToString(); // Hasta ID'si 0'ıncı indeksten gelen değer ile 
                txtad.Text = dr[1].ToString(); // Hasta adı 1'ıncı indeksten gelen değer ile 
                txtsoyad.Text = dr[2].ToString(); // Hasta soyadı 2'ıncı indeksten gelen değer ile 
                msktel.Text = dr[4].ToString(); // Hasta telefon numarası 3'ıncı indeksten gelen değer ile 
                txtsifre.Text = dr[5].ToString(); // Hasta şifresi 4'ıncı indeksten gelen değer ile 
                cmbcinsiyet.Text = dr[6].ToString(); // Hasta cinsiyeti 5'ıncı indeksten gelen değer ile 
            }

            // Veritabanı bağlantısını kapatıyoruz.
            bagla.baglanti().Close();
        }

        // Güncelle butonuna tıklandığında çalışacak metot.
        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            // **HASTA BİLGİLERİNİ GÜNCELLEME**

            // SQL sorgusu ile hasta bilgilerinde değişiklik yapıyoruz.
            SqlCommand komut1 = new SqlCommand("update Tbl_Hastalar set HastaAd=@h1 , HastaSoyad=@h2 , HastaTC=@h3 , HastaTelefon=@h4 , HastaSifre=@h5 , HastaCinsiyet=@h6 where Hastaid=@h7",bagla.baglanti());

            // Güncellenen hasta bilgilerini parametre olarak ekliyoruz.
            komut1.Parameters.AddWithValue("@h1", txtad.Text); // Hasta adı
            komut1.Parameters.AddWithValue("@h2", txtsoyad.Text); // Hasta soyadı
            komut1.Parameters.AddWithValue("@h3", msktc.Text); // Hasta TC kimlik numarası
            komut1.Parameters.AddWithValue("@h4", msktel.Text); // Hasta telefon numarası
            komut1.Parameters.AddWithValue("@h5", txtsifre.Text); // Hasta şifresi
            komut1.Parameters.AddWithValue("@h6", cmbcinsiyet.Text); // Hasta cinsiyeti
            komut1.Parameters.AddWithValue("@h7", txtid.Text); // Hasta ID'si

            // SQL sorgusunu çalıştırarak güncellemeyi gerçekleştiriyoruz.
            komut1.ExecuteNonQuery();

            // Veritabanı bağlantısını kapatıyoruz.
            bagla.baglanti().Close();

            // Kullanıcıya güncellemenin başarılı olduğuna dair bilgi mesajı gösteriyoruz.
            MessageBox.Show("Bilgileriniz Güncellenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            
        }
    }
}
