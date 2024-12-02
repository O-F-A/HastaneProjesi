using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneProjesi
{
    // Not sınıfı, soyut bir sınıf olup notların genel özelliklerini ve metodlarını tanımlar
    public abstract class Not
    {
        // Not başlığını tutacak property
        public string Baslik { get; set; }

        // Not içeriğini tutacak property
        public string Icerik { get; set; }

        // Notun tarihini tutacak property
        public string Tarih { get; set; }

        // Notu kaydetmek için soyut metod. Her sınıf bu metodu kendine göre implement etmek zorundadır.
        public abstract void Kaydet();

        // Notu silmek için soyut metod. Her sınıf bu metodu kendine göre implement etmek zorundadır.
        public abstract void Sil();

        // Notu göstermek için soyut metod. Her sınıf bu metodu kendine göre implement etmek zorundadır.
        public abstract void Goster();
    }
}

