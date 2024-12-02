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
    public partial class FrmBransPaneli : Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısı için nesne

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            // BRANŞLARI ÇEKME
            DataTable dt = new DataTable(); // Branş bilgilerini tutacak DataTable
            SqlDataAdapter da = new SqlDataAdapter("Select Bransid as BranşNo, BransAd as BarnşAd from Tbl_Brans", bagla.baglanti());
            da.Fill(dt); // Verileri çekme
            dataGridView1.DataSource = dt; // Verileri DataGridView'e yansıtma
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DataGridView'den seçilen satırı alma ve ilgili alanları formda gösterme
            int secilen = dataGridView1.SelectedCells[0].RowIndex; // Seçilen hücrenin satır index'ini al
            txtbransid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString(); // Branş ID'yi formda göster
            txtbransad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString(); // Branş Adı'nı formda göster
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            // YENİ BRANŞ EKLEME
            SqlCommand komutekle = new SqlCommand("insert into Tbl_Brans (BransAd) values (@b1)", bagla.baglanti());
            komutekle.Parameters.AddWithValue("@b1", txtbransad.Text); // Branş adı parametresi
            komutekle.ExecuteNonQuery(); // Veritabanına sorgu gönderme
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Branş Eklenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            // SEÇİLEN BRANŞI SİLME
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Brans where Bransid=@b1", bagla.baglanti());
            komutsil.Parameters.AddWithValue("@b1", txtbransid.Text); // Silinecek branşın ID'si
            komutsil.ExecuteNonQuery(); // Sorguyu çalıştırma
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Branş Silinmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            // BRANŞI GÜNCELLEME
            SqlCommand komutekle = new SqlCommand("update Tbl_Brans set BransAd=@b1 where Bransid=@b2", bagla.baglanti());
            komutekle.Parameters.AddWithValue("@b1", txtbransad.Text); // Yeni branş adı parametresi
            komutekle.Parameters.AddWithValue("@b2", txtbransid.Text); // Güncellenecek branş ID'si
            komutekle.ExecuteNonQuery(); // Veritabanına güncelleme sorgusu gönderme
            bagla.baglanti().Close(); // Bağlantıyı kapatma
            MessageBox.Show("Branş Güncellenmiştir", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information); // Kullanıcıyı bilgilendirme
        }

        private void btnlisteyenileme_Click(object sender, EventArgs e)
        {
            // BRANŞLARI LİSTELEME VE YENİLEME
            DataTable dt = new DataTable(); // Branş bilgilerini tutacak DataTable
            SqlDataAdapter da = new SqlDataAdapter("Select Bransid as BranşNo, BransAd as BarnşAd from Tbl_Brans", bagla.baglanti());
            da.Fill(dt); // Verileri çekme
            dataGridView1.DataSource = dt; // Verileri DataGridView'e yansıtma
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Formdaki alanları temizleme
            txtbransid.Text = ""; // Branş ID alanını temizleme
            txtbransad.Text = ""; // Branş adı alanını temizleme
        }
    }
}

