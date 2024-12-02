using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProjesi
{
    // Temel sınıf
    public class bildirim : degiskenler
    {
        // Bu metot sanal (virtual) olarak tanımlandı, böylece türemiş sınıflar tarafından override edilebilir.
        public virtual void msjcagir(string X, string Y)
        {
            MessageBox.Show("Sayın Hasta " + X + " TC: " + Y + " Hoşgeldiniz  \nSistemimize giriş yaptınız. \n GEÇMİŞ OLSUN", "BİR MESAJIN VAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Türetilmiş sınıf: Sekreter için özel mesaj
    public class ozelBildirimsekreter : bildirim
    {
        // override anahtar kelimesi ile temel sınıftaki metot özelleştirilmiştir
        public override void msjcagir(string X, string Y)
        {
            MessageBox.Show("Sayın Sekreter " + X + ", TC: " + Y + " Hoşgeldiniz \nSistemimize giriş yaptınız. \n KOLAY GELSİN", "BİR MESAJIN VAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Türetilmiş sınıf: Doktor için özel mesaj
    public class ozelBildirimdoktor : bildirim
    {
        // override anahtar kelimesi ile temel sınıftaki metot özelleştirilmiştir
        public override void msjcagir(string X, string Y)
        {
            MessageBox.Show("Sayın Doktor " + X + ", TC: " + Y + " Hoşgeldiniz \nSistemimize giriş yaptınız. \n KOLAY GELSİN", "BİR MESAJIN VAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

