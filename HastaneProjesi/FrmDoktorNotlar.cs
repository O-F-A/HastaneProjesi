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
    public partial class FrmDoktorNotlar : Form
    {
        public FrmDoktorNotlar()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını oluşturacak nesne
        public string tc; // Doktorun TC kimlik numarasını tutacak değişken

        public void FrmDoktorNotlar_Load(object sender, EventArgs e)
        {
            // Doktor notlarını yüklemek için gerekli işlemler
            DoktorNotu doktornot = new DoktorNotu(); // Doktor notları için sınıf örneği oluştur
            doktornot.DOKTORTC = tc; // Doktorun TC kimlik numarasını sınıfa aktar

            // Veritabanından doktor notlarını çekme //LİSTELEME
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT NotBaslik, NotIcerik, NotTarih FROM Tbl_DoktorNotlar WHERE DoktorTC=@p1", bagla.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@p1", doktornot.DOKTORTC); // Filtre olarak TC kimlik numarası kullanılır
            da.Fill(dt); // Gelen veriler DataTable'a doldurulur
            dataGridView1.DataSource = dt; // DataGridView kontrolüne veriler bağlanır
            msktarih.Focus(); //imleci tarihe getirme
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            // Yeni bir doktor notu ekleme işlemi
            DoktorNotu doktornot = new DoktorNotu(); // Doktor notu sınıfından bir nesne oluşturulur
            doktornot.Baslik = txtbaslik.Text; // Başlık bilgisi alınır
            doktornot.Icerik = rtbicerik.Text; // İçerik bilgisi alınır
            doktornot.Tarih = msktarih.Text; // Tarih bilgisi alınır
            doktornot.DOKTORTC = tc; // Doktorun TC kimlik numarası eklenir
            doktornot.Kaydet(); // Notu kaydetme işlemi gerçekleştirilir

            // Not listesini güncelleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT NotBaslik, NotIcerik, NotTarih FROM Tbl_DoktorNotlar WHERE DoktorTC=@p1", bagla.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@p1", doktornot.DOKTORTC); // TC kimlik numarasına göre notlar listelenir
            da.Fill(dt); // Gelen veriler DataTable'a doldurulur
            dataGridView1.DataSource = dt; // DataGridView kontrolüne veriler bağlanır
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            // Mevcut bir doktor notunu silme işlemi
            DoktorNotu doktornot = new DoktorNotu(); // Doktor notu sınıfından bir nesne oluşturulur
            doktornot.Baslik = txtbaslik.Text; // Silinecek notun başlığı alınır
            doktornot.DOKTORTC = tc; // Doktorun TC kimlik numarası alınır
            doktornot.Sil(); // Not silme işlemi gerçekleştirilir

            // Not listesini güncelleme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT NotBaslik, NotIcerik, NotTarih FROM Tbl_DoktorNotlar WHERE DoktorTC=@p1", bagla.baglanti());
            da.SelectCommand.Parameters.AddWithValue("@p1", doktornot.DOKTORTC); // TC kimlik numarasına göre notlar listelenir
            da.Fill(dt); // Gelen veriler DataTable'a doldurulur
            dataGridView1.DataSource = dt; // DataGridView kontrolüne veriler bağlanır
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView'deki bir hücreye tıklanınca ilgili not detaylarını doldurma işlemi
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen hücrenin satırını bulur
            msktarih.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString(); // Tarih bilgisi TextBox'a yazdırılır
            txtbaslik.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // Başlık bilgisi TextBox'a yazdırılır
            rtbicerik.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // İçerik bilgisi RichTextBox'a yazdırılır
        }

        private void btngüncelle_Click(object sender, EventArgs e)
        {
            // Mevcut bir notu güncelleme işlemi
            DoktorNotu doktornot = new DoktorNotu(); // Doktor notu sınıfından bir nesne oluşturulur
            doktornot.Baslik = txtbaslik.Text; // Güncellenmiş başlık bilgisi alınır
            doktornot.Icerik = rtbicerik.Text; // Güncellenmiş içerik bilgisi alınır
            doktornot.Tarih = msktarih.Text; // Güncellenmiş tarih bilgisi alınır
            doktornot.Goster(); // Güncelleme işlemini gerçekleştiren metod çağrılır
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            // Not ekleme/güncelleme alanlarını temizleme işlemi
            msktarih.Clear(); // Tarih alanını temizler
            txtbaslik.Clear(); // Başlık alanını temizler
            rtbicerik.Clear(); // İçerik alanını temizler
        }
    }
}

