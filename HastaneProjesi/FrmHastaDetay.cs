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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent(); // Form bileşenlerini başlatır.
        }
        sqlbaglantisi bagla = new sqlbaglantisi(); // SQL bağlantı sınıfını çağırma
        public string tc; // Hasta TC'si diğer formlardan alınacak.

        //Hasta detay formu açıldığında yüklenecek olan veriler
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            
            // SQL veri tabanından AD SOYAD biligisini TC'ye göre getirecek sorgu
            lbltc.Text = tc; // Hasta TC'sini etikete yazdırır.
            SqlCommand komut = new SqlCommand("select HastaAd , HastaSoyad from Tbl_Hastalar where HastaTC=@p1 ",bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", tc); // TC kimlik numarasını TC adlı değişkenden alacağını belirten ve parametreye atayan kod
            SqlDataReader dr = komut.ExecuteReader(); // Sorguyu çalıştırır.
            while (dr.Read()) // Veritabanından dönen sonucu okur. 
            {
                lbladsoyad.Text = dr[0] + " " + dr[1]; //Sorgudan dönen değeri Hasta ad ve soyadı etikete yazdırılır.
            }
            // Veritabanı bağlantısını kapatır.
            bagla.baglanti().Close();

            // Hasta TC'sine göre tüm randevuları tabloya çekiyoruz.
            DataTable dt = new DataTable(); // dt adında boş bir tablo çağırdık 
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC="+tc, bagla.baglanti());

            // da.SelectCommand.Parameters.AddWithValue("@tc", tc); //İstenirse bu şekilde de veriler alınabilir bu bir örnek kod.
            
            da.Fill(dt);// Randevu bilgilerini dt adlı boş tabloyu da sorgusundan gelen veri ile doldurur.
            dgvrandevularım.DataSource = dt; // dt tablosunu dgrandevularım adlı DataGridView ekranına yazar.

            // Veritabanındaki tüm branşları çekiyoruz.
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Brans", bagla.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader(); // komut2 de olan sorguyu okuyucuyu çalıştırıyoruz ve okunabilir dr2 nesnesine atıyoruz.
            while (dr2.Read()) // dr2 nesnesini okutuyoruz
            {
                cmbbrans.Items.Add(dr2[0]); // okunan Branş isimlerini combobox'a ekliyoruz.
            }
            bagla.baglanti().Close(); // Bağlantıyı kapatıyoruz.


            // Bildirim sınıfını kullanarak mesaj gönderme işlemi yapıyoruz. Form açıldığında kişiye özel bir mesaj çıkıyor.
            bildirim mesaj = new bildirim();
            mesaj.adsoyad = lbladsoyad.Text; // Ad soyadı bildirime ekliyoruz.
            mesaj.tckimlikno = lbltc.Text; // TC kimlik numarasını bildirime ekliyoruz.
            mesaj.msjcagir(mesaj.adsoyad,mesaj.tckimlikno); // bildirim sınıfındaki msjcagir metodunu çağırarak Mesajı tetikliyoruz.

        }

        // Combobox da seçilen veriye göre ne yapılacağını belirliyoruz  //BRANŞA AİT DOKTORLARI ÇEKME
        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            cmbdoktor.Items.Clear(); // Yeni bir branş seçildiğinde, önce doktor listesini temizliyoruz.

            // Seçilen branşa ait doktorları veritabanından çekmek için SQL komutunu hazırlıyoruz.
            SqlCommand komut3 = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@d1", bagla.baglanti());
            komut3.Parameters.AddWithValue("@d1", cmbbrans.Text); // Seçilen branşı parametre olarak ekliyoruz.
            SqlDataReader dr3 = komut3.ExecuteReader(); // komut3 de olan sorguyu okuyucuyu çalıştırıyoruz ve okunabilir dr3 nesnesine atıyoruz.

            // Doktor ad ve soyadlarını okuyup combobox'a ekliyoruz.
            while (dr3.Read()) 
            {
                cmbdoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            // Bağlantıyı kapatıyoruz.
            bagla.baglanti().Close();
        }

        // Combobox da seçilen veriye göre ne yapılacağını belirliyoruz //AKTİF RANDEVULARI GÖRME
        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Doktor seçildiğinde, o doktora ait aktif randevuları gösteriyoruz.

            DataTable dt1 = new DataTable(); // dt1 adında boş bir tablo çağırdık 
            // Aktif randevuları getiren SQL sorgusunu hazırlıyoruz
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans=@r1 and RandevuDoktor=@r2 and RandevuDurum=0",bagla.baglanti());// RandevuDurum=0 yazan yerde boş olan randevuları getirmesini sağlıyoruz.
            da1.SelectCommand.Parameters.AddWithValue("@r1", cmbbrans.Text); // Seçilen branşı parametre olarak ekliyoruz.
            da1.SelectCommand.Parameters.AddWithValue("@r2", cmbdoktor.Text); // Seçilen doktoru parametre olarak ekliyoruz.
            da1.Fill(dt1); // Sonuçları DataTable'a dolduruyoruz.
            dgvaktifrandevular.DataSource = dt1; // Aktif randevuları DataGridView'de gösteriyoruz.



        }

        //Bilgilerini Güncelle linkine tıklandığında ne olacağını belirliyoruz
        private void lnkbilgiduzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hasta bilgilerini düzenlemek için yeni bir form açıyoruz.
            FrmBilgiDuzenle frmBilgi = new FrmBilgiDuzenle();
            frmBilgi.TCno = lbltc.Text; // TC bilgisini diğer forma iletiyoruz.
            frmBilgi.Show(); // Yeni formu açıyoruz.
        }


        private void dgvaktifrandevular_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aktif randevular tablosunda bir hücreye çift tıklandığında, o satırın randevu ID'sini alıyoruz.
            int secilen = dgvaktifrandevular.SelectedCells[0].RowIndex; // Seçilen satırın indeksini alıyoruz.
            txtid.Text = dgvaktifrandevular.Rows[secilen].Cells[0].Value.ToString(); // Seçilen randevu ID'sini text kutusuna yazdırıyoruz.
        }


        //Randevu Al butonuna tıklandığında ne olacağını belirliyoruz.
        private void btnrandevual_Click(object sender, EventArgs e)
        {
            // Randevu alma işlemi: Veritabanındaki seçili randevunun durumunu güncelliyoruz. //RANDEVU ALMA İŞLEMİ
            //Randevu id=x olan randevuyu güncelleme mantığı ile sorgu yazıyoruz
            SqlCommand komutal = new SqlCommand("update Tbl_Randevular set RandevuDurum=1, HastaTC=@rd1, HastaSikayet=@rd2 where Randevuid=@rd3", bagla.baglanti());
            komutal.Parameters.AddWithValue("@rd1", lbltc.Text); // TC kimlik numarasını ekliyoruz.
            komutal.Parameters.AddWithValue("@rd2", rtbsikayet.Text); // Şikayet bilgisini ekliyoruz.
            komutal.Parameters.AddWithValue("@rd3", txtid.Text); // Seçilen randevu ID'sini ekliyoruz. 
            komutal.ExecuteNonQuery(); // Sorguyu çalıştırıyoruz.
            bagla.baglanti().Close();// Bağlantıyı kapatıyoruz.
            MessageBox.Show("Randevu Alındı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıya başarılı işlem mesajı gösteriyoruz.


            // Randevu listesini güncelliyoruz. Randevularımı görme kısmındaki kodun aynısı
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTC=" + tc, bagla.baglanti());
            // da.SelectCommand.Parameters.AddWithValue("@tc", tc);
            da.Fill(dt);
            dgvrandevularım.DataSource = dt;

            // Aktif randevuları güncelliyoruz.AKTİF RANDEVULARI GÖRME kısmındaki kodun aynısı
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans=@r1 and RandevuDoktor=@r2 and RandevuDurum=0", bagla.baglanti());
            da1.SelectCommand.Parameters.AddWithValue("@r1", cmbbrans.Text);
            da1.SelectCommand.Parameters.AddWithValue("@r2", cmbdoktor.Text);
            da1.Fill(dt1);
            dgvaktifrandevular.DataSource = dt1;
        }

        
        //Temizlik butonuna tıklandığında ne olacağını belirliyoruz.
        private void btntemizle_Click(object sender, EventArgs e)
        {
            // Form üzerindeki tüm alanları temizliyoruz.
            txtid.Clear();
            cmbbrans.Items.Clear();
            cmbdoktor.Items.Clear();
            rtbsikayet.Clear();
            
        }


        // yenileme butonuna tıklandığında ne olacağını belirliyoruz.
        private void btnYenile_Click(object sender, EventArgs e)
        {
            //form yüklendiğinde ad soyad çekme komutlarının aynısı buradaki görevi kullanıcının bilgilerini düzenledikten sonra bu buton ile düzenlenmiş halini görebilmesi.
            // AD SOYAD ÇEKME
            lbltc.Text = tc;
            SqlCommand komut = new SqlCommand("select HastaAd , HastaSoyad from Tbl_Hastalar where HastaTC=@p1 ", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bagla.baglanti().Close();
        }

        //çıkış yap butonuna tıklandığında ne olacağını belirliyoruz.
        private void btncikisyap_Click(object sender, EventArgs e)
        {
            // Giriş formuna dönüş yapıyoruz.
            FrmHastaGiris hastagiris = new FrmHastaGiris();
            hastagiris.Close();// Hasta giriş formunu kapatıyoruz.

            FrmGirisler girisler = new FrmGirisler();
            girisler.Show();// Girişler formunu açıyoruz.
            this.Close();// Mevcut formu kapatıyoruz.
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}
