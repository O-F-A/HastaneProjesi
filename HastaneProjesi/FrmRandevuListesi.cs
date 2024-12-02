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
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent(); // Form bileşenlerini başlatma
        }

        sqlbaglantisi bagla = new sqlbaglantisi(); // Veritabanı bağlantısını tutacak nesne

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            // Randevuları çekme ve DataGridView'e yükleme
            DataTable dt = new DataTable(); // Veritabanından çekilecek randevular için DataTable nesnesi
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular", bagla.baglanti()); // Veritabanı sorgusu
            da.Fill(dt); // Veritabanından gelen veriyi DataTable'e doldurma
            dataGridView1.DataSource = dt; // DataGridView'e veriyi bağlama
        }
    }
}

