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
using EntityOrnek.AdoNetEntityDataModel;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();//grobala taşıdık...
        private void BtnDersListesi_Click(object sender, EventArgs e)
        {//Ado.Net ile vt bağlantısı yapıldı...
            SqlConnection baglanti = new SqlConnection(@"Data Source=.;Initial Catalog=DbSinavOgrenci;Integrated Security=True");
            SqlCommand komut = new SqlCommand("select * from tblDersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {//BtnOgrenciListele  //entity framwork ile vt bağlantısı yapıldı
            DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
            dataGridView1.DataSource = db.tblOgrenci.ToList();
            dataGridView1.Columns[3].Visible = false;//listelerken fazla gelen kolonu false yapıyoruz
            dataGridView1.Columns[4].Visible = false;
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {

            var query = from item in db.tblNotlar
                        select new
                        {
                            // item.tblOgrenci,
                            item.NotID,
                            item.Ogr,
                            item.Ders,
                            item.Sinav1,
                            item.Sinav2,
                            item.Sinav3,
                            item.Ortalama,
                            item.Durum,
                        };
            dataGridView1.DataSource = query.ToList();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            tblOgrenci togr = new tblOgrenci();
            togr.Ad = TextOgrenciAd.Text;
            togr.Soyad = TextOgreciSoyad.Text;
            db.tblOgrenci.Add(togr);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye Eklenmiştir.");
            TextOgreciSoyad.Clear();
            TextOgrenciAd.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // öğrenci bilgilerinden id'si seçili olanı siler...
            int id = int.Parse(TextOgrenciID.Text);
            var sil = db.tblOgrenci.Find(id);
            db.tblOgrenci.Remove(sil);
            db.SaveChanges();
            MessageBox.Show("öğrenc sistemden silindi...");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TextOgrenciID.Text);
            var guncelle = db.tblOgrenci.Find(id);
            guncelle.Ad = TextOgrenciAd.Text;
            guncelle.Soyad = TextOgreciSoyad.Text;
            guncelle.Fotograf = TextOgrenciFoto.Text;
            //  guncelle.OgrendiID = int.Parse(TextOgrenciID.Text);
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi yapılmıştır...");
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {//ve & şartlı arama 
          /*  dataGridView1.DataSource = db.tblOgrenci.Where
                (x => x.Ad == TextOgrenciAd.Text & x.Soyad == TextOgreciSoyad.Text).ToList();*/
            //veya | şartlı arama
            dataGridView1.DataSource = db.tblOgrenci.Where
                (x => x.Ad == TextOgrenciAd.Text | x.Soyad == TextOgreciSoyad.Text).ToList();
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotListesi();

        }

        private void BtnDersEkle_Click(object sender, EventArgs e)
        {

        }
    }

}

