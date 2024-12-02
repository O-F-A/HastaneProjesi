using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneProjesi
{
    // Veritabanı bağlantısını yöneten sınıf
    class sqlbaglantisi
    {
        // baglanti() metodu: Veritabanına bağlantıyı açan ve döndüren metot
        public SqlConnection baglanti()
        {
            // SQL Server'a bağlanmak için SqlConnection nesnesi oluşturuluyor.
            // Bağlantı dizesinde, veritabanı sunucusu, veritabanı adı ve güvenlik bilgileri yer alır.
            SqlConnection baglan = new SqlConnection("Data Source=MUTLAK0\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");

            // Bağlantıyı açmak için Open() metodu çağrılır.
            baglan.Open();

            // Bağlantı başarılı bir şekilde açıldıktan sonra, SqlConnection nesnesi döndürülür.
            return baglan;
        }
    }
}

