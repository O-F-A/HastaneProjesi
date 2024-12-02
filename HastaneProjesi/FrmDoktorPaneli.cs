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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısı için nesne

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            // DOKTORLARI ÇEKME
            DataTable dt = new DataTable(); // Doktor bilgilerini tutacak DataTable
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Doktorlar", bagla.baglanti());
            da.Fill(dt); // Verileri çekme
            dataGridView1.DataSource = dt; // Verileri DataGridView'e yansıtma

            // BRANŞLARI COMBOBOX'A ÇEKME
            SqlCommand komutbransaktarma = new SqlCommand("Select BransAd From Tbl_Brans", bagla.baglanti());
            SqlDataReader dr1 = komutbransaktarma.ExecuteReader(); // Veritabanından branşları çekme
            while (dr1.Read())
            {
                cmbbrans.Items.Add(dr1[0]); // Branşları combobox'a ekleme
            }
            bagla.baglanti().Close(); // Bağlantıyı kapatma
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            // YENİ DOKTOR EKLEME
            SqlCommand komutekle = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bagla.baglanti());
            komutekle.Parameters.AddWithValue("@d1", txtad.Text); // Ad parametresi
            komutekle.Parameters.AddWithValue("@d2", txtsoyad.Text); // Soyad parametresi
            komutekle.Parameters.AddWithValue("@d3", cmbbrans.Text); // Branş parametresi
            komutekle.Parameters.AddWithValue("@d4", mskTC.Text); // TC parametresi
            komutekle.Parameters.AddWithValue("@d5", txtsifre.Text); // Şifre parametresi
            komutekle.ExecuteNonQuery(); // Veritabanına sorgu gönderme
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Doktorun Kaydı Yapılmıştır", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void btnlisteyenile_Click_1(object sender, EventArgs e)
        {
            // DOKTORLARI LİSTELEME VE YENİLEME
            DataTable dt = new DataTable(); // Doktor bilgilerini tutacak DataTable
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Doktorlar", bagla.baglanti());
            da.Fill(dt); // Verileri çekme
            dataGridView1.DataSource = dt; // Verileri DataGridView'e yansıtma
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView'den seçilen satırı alma ve ilgili alanları formda gösterme
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen hücrenin satır index'ini al
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // Ad'ı formda göster
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // Soyad'ı formda göster
            cmbbrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString(); // Branş'ı formda göster
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString(); // TC'yi formda göster
            txtsifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString(); // Şifreyi formda göster
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            // SEÇİLEN DOKTORU SİLME
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC=@d1", bagla.baglanti());
            komutsil.Parameters.AddWithValue("@d1", mskTC.Text); // Silinecek doktorun TC'si
            komutsil.ExecuteNonQuery(); // Sorguyu çalıştırma
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Doktorun Kaydı Silinmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Kullanıcıyı bilgilendirme
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            // DOKTOR KAYDINI GÜNCELLEME
            SqlCommand komutguncelle = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@dg1, DoktorSoyad=@dg2, DoktorBrans=@dg3, DoktorSifre=@dg5 where DoktorTC=@dg4", bagla.baglanti());
            komutguncelle.Parameters.AddWithValue("@dg1", txtad.Text); // Ad parametresi
            komutguncelle.Parameters.AddWithValue("@dg2", txtsoyad.Text); // Soyad parametresi
            komutguncelle.Parameters.AddWithValue("@dg3", cmbbrans.Text); // Branş parametresi
            komutguncelle.Parameters.AddWithValue("@dg4", mskTC.Text); // TC parametresi
            komutguncelle.Parameters.AddWithValue("@dg5", txtsifre.Text); // Şifre parametresi      
            komutguncelle.ExecuteNonQuery(); // Veritabanına güncelleme sorgusunu gönderme
            MessageBox.Show("Doktorun Kaydı Güncellenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Kullanıcıyı bilgilendirme
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Formdaki alanları temizleme
            txtad.Clear(); // Ad alanını temizleme
            txtsoyad.Clear(); // Soyad alanını temizleme
            cmbbrans.Text = ""; // Branş combobox'ını sıfırlama
            mskTC.Clear(); // TC alanını temizleme
            txtsifre.Clear(); // Şifre alanını temizleme
        }
    }
}

