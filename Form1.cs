using StokTakip.Entitiy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnEkleme_Click(object sender, EventArgs e)
        {
            using (var context = new StokContext())
            {
                if (txtBoyaKodu.Text == "" || 
                    txtBoyaRengi.Text == "" || 
                    txtFiyat.Text == "" || 
                    txtStokAdet.Text == "" || 
                    txtUreticiFirma.Text == "" ||
                    tipBoya.Text == "" || tipBoya.Text == "Seçiniz...")
                {
                    MessageBox.Show("Tüm Alanlar Dolu olmalı");
                    return;
                }

                int boyaKodu = Convert.ToInt32(txtBoyaKodu.Text);
                var VarlikKontrol = context.Datas.FirstOrDefault(b => b.BoyaKodu == boyaKodu);

                if(VarlikKontrol == null)
                {
                    var yeniBoya = new Data
                    {
                        BoyaKodu = Convert.ToInt32(txtBoyaKodu.Text),
                        BoyaRengi = txtBoyaRengi.Text,
                        BoyaTipi = tipBoya.Text,
                        BoyaFiyat = Convert.ToInt32(txtFiyat.Text),
                        StokAdet = Convert.ToInt32(txtStokAdet.Text),
                        UreticiFirma = txtUreticiFirma.Text
                    };
                    context.Datas.Add(yeniBoya);
                    context.SaveChanges();
                    MessageBox.Show("Eklendi");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Boya Kodu Zaten mevcut");
                }
                
            }
        }

        private void BtnSilme_Click(object sender, EventArgs e)
        {
            using (var context = new StokContext())
            {
                // Boşluk kontrolü
                if (string.IsNullOrWhiteSpace(txtBoyaKodu.Text) ||
                    string.IsNullOrWhiteSpace(txtStokAdet.Text))
                {
                    MessageBox.Show("Boya Kodu ve Satılan Ürün Adeti Yazınız");
                    return;
                }


                // Veritabanından mevcut boyayı al
                var boyaKodu = Convert.ToInt32(txtBoyaKodu.Text);
                var mevcutBoya = context.Datas.FirstOrDefault(b => b.BoyaKodu == boyaKodu);

                if (mevcutBoya != null)
                {
                    // StokAdet güncelleme işlemi
                    if (!int.TryParse(txtStokAdet.Text, out int stokAdet))
                    {
                        if (mevcutBoya.StokAdet >= stokAdet)
                        {
                            mevcutBoya.StokAdet -= stokAdet;
                            context.SaveChanges();
                            MessageBox.Show("Stok güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Stok miktarı yeterli değil.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Satılan Ürün Adeti geçerli bir tamsayı olmalıdır.");
                    }
                }
                else
                {
                    MessageBox.Show("Belirtilen Boya Kodu ile eşleşen kayıt bulunamadı.");
                }

            }
        }

        private void LoadData()
        {
            using (var context = new StokContext())
            {
                var data = context.Datas.ToList(); // Veritabanındaki tüm verileri çekiyoruz

                // DataGridView'e veri bağlamadan önce sütunları sıfırlıyoruz
                dataGridView1.Columns.Clear();

                // DataGridView'e veri bağlıyoruz
                dataGridView1.DataSource = data;

                // 'Id' sütununu bulup gizliyoruz
                var idColumn = dataGridView1.Columns["Id"];
                if (idColumn != null)
                {
                    idColumn.Visible = false; // 'Id' sütununu gizliyoruz
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
