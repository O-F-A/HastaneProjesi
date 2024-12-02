using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HastaneProjesi
{
    // DoktorNotu sınıfı, Not sınıfından türemektedir ve doktorlara ait not işlemlerini yönetir
    public class DoktorNotu : Not
    {
        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını sağlamak için nesne
        string DoktorTC; // Doktorun TC kimlik numarasını tutacak özel değişken

        // DoktorTC property'si
        public string DOKTORTC
        {
            get { return DoktorTC; } // Doktorun TC kimlik numarasını döndürür
            set { DoktorTC = value; } // Doktorun TC kimlik numarasını ayarlar
        }

        // Kaydet metodunun override edilmiş versiyonu
        public override void Kaydet()
        {
            // Veritabanına yeni bir not kaydetme işlemi
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_DoktorNotlar (DoktorTC, NotBaslik, NotIcerik, NotTarih) VALUES (@p1, @p2, @p3, @p4)", bagla.baglanti());

            // Parametrelerle SQL sorgusuna veri eklenir
            komut.Parameters.AddWithValue("@p1", DOKTORTC); // Doktorun TC kimlik numarasını ekler
            komut.Parameters.AddWithValue("@p2", Baslik); // Not başlığını ekler
            komut.Parameters.AddWithValue("@p3", Icerik); // Not içeriğini ekler
            komut.Parameters.AddWithValue("@p4", Tarih); // Not tarihini ekler

            // SQL komutu çalıştırılır ve veritabanına veri eklenir
            komut.ExecuteNonQuery();
        }

        // Sil metodunun override edilmiş versiyonu
        public override void Sil()
        {
            // Veritabanından belirli bir notu silme işlemi
            SqlCommand komut = new SqlCommand("DELETE FROM Tbl_DoktorNotlar WHERE NotBaslik=@p1 AND DoktorTC=@p2", bagla.baglanti());

            // Parametrelerle SQL sorgusuna veri eklenir
            komut.Parameters.AddWithValue("@p1", Baslik); // Silinecek not başlığını ekler
            komut.Parameters.AddWithValue("@p2", DOKTORTC); // Silinecek doktorun TC kimlik numarasını ekler

            // SQL komutu çalıştırılır ve not silinir
            komut.ExecuteNonQuery();
            bagla.baglanti().Close(); // Veritabanı bağlantısı kapatılır
        }

        // Goster metodunun override edilmiş versiyonu
        public override void Goster()
        {
            // Not detaylarını kullanıcıya göstermek için bir mesaj kutusu açar
            MessageBox.Show($"Başlık: {Baslik}\nİçerik: {Icerik}\nTarih: {Tarih}");
        }
    }
}

