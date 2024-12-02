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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent(); // Formun bileşenlerini başlatır.
        }
        // SQL bağlantısı oluşturmak için kullanılan sınıfın bir örneği
        sqlbaglantisi bagla = new sqlbaglantisi();

        // Üye olma linkine tıklandığında çalışacak olay
        private void lnkuyeol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Üye kayıt formunu açar
            FrmUyeKayit fr = new FrmUyeKayit();
            fr.Show(); // Yeni formu ekrana getirir.

        }
        // Giriş yap butonuna tıklandığında çalışacak olay
        private void btngirisyap_Click(object sender, EventArgs e)
        {
            // Hasta giriş bilgilerini veritabanından kontrol etmek için SQL sorgusu oluşturulur
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2",bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text); // Kullanıcıdan alınan TC kimlik numarası
            komut.Parameters.AddWithValue("@p2",txtsifre.Text); // Kullanıcıdan alınan şifre
            SqlDataReader dr = komut.ExecuteReader(); // SQL sorgusunu çalıştırır ve sonuçları okur

            if (dr.Read()) // Eğer kayıt bulunursa
            {
                FrmHastaDetay fr = new FrmHastaDetay(); // Hasta detay formunu açar ve hasta TC bilgisini aktarır
                fr.tc = msktc.Text; // Hasta detay formuna TC bilgisini gönderir
                fr.Show(); // Hasta detay formunu ekrana getirir
                this.Hide(); // Mevcut formu gizler
            }
            else
            {
                // Hatalı giriş durumunda kullanıcıya uyarı mesajı gösterir
                MessageBox.Show("Hatalı Giriş! TC & Şifre Hatalı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // SQL bağlantısını kapatır
            bagla.baglanti().Close();

        }
        // Geri dönüş butonuna tıklandığında çalışacak olay
        private void btngeridon_Click(object sender, EventArgs e)
        {
            FrmGirisler girisler = new FrmGirisler(); // Girişler formunu açar
            girisler.Show(); // Yeni formu ekrana getirir
            this.Close(); // Mevcut formu kapatır
        }

        private void btncikis_Click(object sender, EventArgs e)
        {
            // Uygulamadan çıkış yapma
            Application.Exit(); // Uygulamayı kapat
        }
    }
}
