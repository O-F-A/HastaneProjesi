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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent(); // Form bileşenlerini başlatma (tasarım kısmı)
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını tutacak nesne

        public string TC; // Doktorun TC kimlik numarasını tutacak public değişken

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde doktor bilgilerini çekmek için işlemler
            lbltc.Text = TC; // Doktorun TC bilgisini label'a aktar

            // Doktorun ad ve soyadını veritabanından çekme
            SqlCommand komut = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorTC=@p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text); // Veritabanı sorgusunda TC'yi kullanarak doktor bilgilerini çekme
            SqlDataReader dr = komut.ExecuteReader(); // Veritabanından verileri oku
            while (dr.Read()) // Döngüyle verileri al
            {
                lbladsoyad.Text = dr[0] + " " + dr[1]; // Doktorun adı ve soyadını label'a yazdır
            }
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapat

            // Doktorun randevu bilgilerini çekmek için DataGridView'e veri ekleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuDoktor=@d1", bagla.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@d1", lbladsoyad.Text); // Randevu bilgilerinde doktor adı kullanarak veriyi filtreleme
            da.Fill(dt); // Randevu bilgilerini DataTable'a doldur
            dataGridView1.DataSource = dt; // DataGridView'de verileri göster

            // Doktora özel bildirim gönderme işlemi
            bildirim mesaj = new ozelBildirimdoktor(); // Bildirim sınıfından bir nesne oluştur
            mesaj.adsoyad = lbladsoyad.Text; // Doktorun ad soyad bilgisini al
            mesaj.tckimlikno = lbltc.Text; // Doktorun TC kimlik numarasını al
            mesaj.msjcagir(mesaj.adsoyad, mesaj.tckimlikno); // Bildirimi çağır ve ilgili bilgileri ilet
        }

        private void btnbilgiguncelle_Click(object sender, EventArgs e)
        {
            // Doktor bilgilerini güncelleme ekranına yönlendirme
            FrmDoktorBilgiDüzenle bilgiDüzenle = new FrmDoktorBilgiDüzenle(); // Bilgi düzenleme formunu oluştur
            bilgiDüzenle.TCno = lbltc.Text; // Doktorun TC kimlik numarasını bilgi düzenleme formuna aktar
            bilgiDüzenle.Show(); // Bilgi düzenleme formunu göster
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            // Duyurular formunu açma
            FrmDuyurular duyurular = new FrmDuyurular(); // Duyurular formunu oluştur
            duyurular.Show(); // Duyurular formunu göster
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }

       
         private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView'de bir hücreye tıklanırsa, şikayet bilgisini gösterme
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen satırı bul
            rtbşikyet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString(); // Şikayet bilgisini textbox'a aktar
        }
        private void btncikisyap_Click(object sender, EventArgs e)
        {
            // Doktor çıkışı yaparsa giriş ekranına yönlendirme
            FrmGirisler girisler = new FrmGirisler(); // Yeni giriş ekranı oluştur
            girisler.Show(); // Giriş ekranını göster
            this.Close(); // Şu anki formu kapat
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            // Doktorun bilgilerini yenileme işlemi
            SqlCommand komut = new SqlCommand("select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorTC=@p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text); // Doktor TC ile sorgu yap
            SqlDataReader dr = komut.ExecuteReader(); // Veritabanından veri çek
            while (dr.Read()) // Verileri döngüyle al
            {
                lbladsoyad.Text = dr[0] + " " + dr[1]; // Doktorun ad soyad bilgisini label'a yazdır
            }
            bagla.baglanti().Close(); // Veritabanı bağlantısını kapat
        }

        private void btnNotlarim_Click(object sender, EventArgs e)
        {
    
            // FrmDoktorNotlar formunu oluştur
            FrmDoktorNotlar doktorNotlar = new FrmDoktorNotlar();
            doktorNotlar.tc = lbltc.Text;

            // Yeni formu göster
            doktorNotlar.Show();
        }
    }
}

