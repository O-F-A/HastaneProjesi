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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bagla = new sqlbaglantisi();
        public string tckimlik;

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            // Sekreterin TC'sini formda gösterme
            lblTC.Text = tckimlik;

            // Sekreterin adı soyadını veritabanından çekme ve gösterme
            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=" + tckimlik, bagla.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0].ToString(); // Ad Soyad'ı labelda göster
            }
            bagla.baglanti().Close(); // Bağlantıyı kapatma

            // Branşları veri tabanından çekip DataGridView1'de listeleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Bransid as BranşNo, BransAd as BranşAd from Tbl_Brans", bagla.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt; // Branşları grid view'e yükleme

            // Doktorları veri tabanından çekip DataGridView2'de listeleme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select Doktorid AS DoktorNo, (DoktorAd+' '+DoktorSoyad) AS Doktorlar, DoktorBrans as DoktorBranşı from Tbl_Doktorlar", bagla.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1; // Doktorları grid view'e yükleme

            // Branşları ComboBox'a ekleme
            SqlCommand komutbransaktarma = new SqlCommand("Select BransAd From Tbl_Brans", bagla.baglanti());
            SqlDataReader dr1 = komutbransaktarma.ExecuteReader();
            while (dr1.Read())
            {
                cmbbrans.Items.Add(dr1[0]); // Branşları combobox'a ekleme
            }
            bagla.baglanti().Close(); // Bağlantıyı kapatma

            // Bildirim gönderme (Sekreter için)
            bildirim mesaj = new ozelBildirimsekreter();
            mesaj.adsoyad = lbladsoyad.Text;
            mesaj.tckimlikno = lblTC.Text;
            mesaj.msjcagir(mesaj.adsoyad, mesaj.tckimlikno); // Bildirimi tetikleme
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            // Randevu bilgilerini veritabanına kaydetme
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor) values (@p1,@p2,@p3,@p4)", bagla.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", msktarih.Text); // Randevu Tarihi
            komutkaydet.Parameters.AddWithValue("@p2", msksaat.Text);   // Randevu Saati
            komutkaydet.Parameters.AddWithValue("@p3", cmbbrans.Text);  // Seçilen Branş
            komutkaydet.Parameters.AddWithValue("@p4", cmbdoktor.Text); // Seçilen Doktor
            komutkaydet.ExecuteNonQuery(); // Sorguyu çalıştır
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Randevu Oluşturuldu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Branş seçildiğinde, o branşa ait doktorları combo box'a ekleme
            cmbdoktor.Items.Clear(); // Önceki doktorları temizle
            SqlCommand komutdoktoraktarma = new SqlCommand("select (DoktorAd+' '+DoktorSoyad) as doktor from Tbl_Doktorlar where DoktorBrans=@db", bagla.baglanti());
            komutdoktoraktarma.Parameters.AddWithValue("@db", cmbbrans.Text); // Seçilen branşa göre doktorları sorgula
            SqlDataReader dr2 = komutdoktoraktarma.ExecuteReader();
            while (dr2.Read())
            {
                cmbdoktor.Items.Add(dr2[0]); // Doktorları combobox'a ekle
            }
            bagla.baglanti().Close(); // Bağlantıyı kapatma
        }

        private void btnduyuruolustur_Click(object sender, EventArgs e)
        {
            // Duyuru metnini veritabanına kaydetme
            SqlCommand komutduyuru = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bagla.baglanti());
            komutduyuru.Parameters.AddWithValue("@d1", rtbduyuru.Text); // Duyuru metnini parametre olarak ekle
            komutduyuru.ExecuteNonQuery(); // Sorguyu çalıştır
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Duyuru Paylaşıldı", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            // Branş yönetim panelini açma
            FrmBransPaneli frmBrans = new FrmBransPaneli();
            frmBrans.Show();
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            // Doktor yönetim panelini açma
            FrmDoktorPaneli frmdoktorpaneli = new FrmDoktorPaneli();
            frmdoktorpaneli.Show();
        }

        private void btnrandevuliste_Click(object sender, EventArgs e)
        {
            // Randevu listesi formunu açma
            FrmRandevuListesi randevuListesi = new FrmRandevuListesi();
            randevuListesi.Show();
        }

        private void btnduyurulistele_Click(object sender, EventArgs e)
        {
            // Duyuruları listeleme formunu açma
            FrmDuyurular duyurular = new FrmDuyurular();
            duyurular.Show();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            // Duyuru metin alanını temizleme
            rtbduyuru.Text = ""; // Duyuru text alanını temizle
        }

        private void btntemizlerandevu_Click(object sender, EventArgs e)
        {
            // Randevu formunu temizleme
            txtid.Clear();    // Randevu ID alanını temizle
            msktarih.Clear(); // Randevu tarihini temizle
            msksaat.Clear();  // Randevu saatini temizle
            cmbbrans.Text="";  // Branş combobox'ını temizle
            cmbdoktor.Items.Clear(); // Doktor combobox'ını temizle
            msktc.Clear();    // Hasta TC alanını temizle
            ckdurum.Checked = false;  // Durum checkbox'ını temizle
        }

        private void btncikisyap_Click(object sender, EventArgs e)
        {
            // Çıkış yapma ve giriş formunu açma
            FrmGirisler girisler = new FrmGirisler();
            girisler.Show();
            this.Close(); // Sekreter detay formunu kapatma
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}

