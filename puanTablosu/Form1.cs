using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puanTablosu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //EKSİKLER:
        //Aynı takımdan birden fazla olmamalı, güncelleme yapamıyor
        //Toplam 0 maç sayısı varsa gol hesaplama yapamamalı
        //Puan ve averaj aynı ise sıralama değişik oluyor
        //Arama ve filtreleme yok
        //Silerken soru sormadan siliyor
        //Sütun başlığı tıklanında sıralama yapamıyor
        //Hücre düzenleyerek de otomatik hesaplamalar olabilirdi

        List<TakimClass> _takimlar = new List<TakimClass>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                //yeni takım ekler
                TakimClass birTakim = new TakimClass();

                birTakim.TakimAdi = textBox1.Text;
                birTakim.Galibiyet = (int)numericUpDown2.Value;
                birTakim.Beraberlik = (int)numericUpDown3.Value;
                birTakim.Maglubiyet = (int)numericUpDown4.Value;
                birTakim.AtilanGol = (int)numericUpDown5.Value;
                birTakim.YenilenGol = (int)numericUpDown6.Value;
                birTakim.ToplamMacSayisi = birTakim.MasSayisi();
                birTakim.Averaj = birTakim.AverajHesapla();
                birTakim.Puan = birTakim.PuanHesapla();
                _takimlar.Add(birTakim);

                VeriYenile();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TakimClass birTakim = new TakimClass();

            birTakim.TakimAdi = "Başakşehir";
            birTakim.Galibiyet = 14;
            birTakim.Beraberlik = 5;
            birTakim.Maglubiyet = 9;
            birTakim.AtilanGol = 41;
            birTakim.YenilenGol = 28;
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);

            birTakim = new TakimClass();
            birTakim.TakimAdi = "Konyaspor";
            birTakim.Galibiyet = 15;
            birTakim.Beraberlik = 7;
            birTakim.Maglubiyet = 6;
            birTakim.AtilanGol = 44;
            birTakim.YenilenGol = 29;
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);

            birTakim = new TakimClass();
            birTakim.TakimAdi = "Trabzonspor";
            birTakim.Galibiyet = 20;
            birTakim.Beraberlik = 7;
            birTakim.Maglubiyet = 1;
            birTakim.AtilanGol = 52;
            birTakim.YenilenGol = 21;
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);

            birTakim = new TakimClass("Fenerbahçe", 13, 8, 7, 43, 32);
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);

            birTakim = new TakimClass("Alanyaspor", 13, 7, 8, 51, 42);
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);

            birTakim = new TakimClass("Adana Demirspor", 12, 9, 7, 42, 28);
            birTakim.ToplamMacSayisi = birTakim.MasSayisi();
            birTakim.Averaj = birTakim.AverajHesapla();
            birTakim.Puan = birTakim.PuanHesapla();
            _takimlar.Add(birTakim);


            VeriYenile();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //şampiyon ve son takım rengi değişir
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.IndianRed;
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.GreenYellow;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //seçili olanları siler
            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    _takimlar.RemoveAt(dataGridView1.SelectedRows[i].Index);
                }
                VeriYenile();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //güncellemek için seçilir
            int rowIndex = e.RowIndex;
            if (rowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                numericUpDown2.Value = Convert.ToInt32(row.Cells[1].Value);
                numericUpDown3.Value = Convert.ToInt32(row.Cells[2].Value);
                numericUpDown4.Value = Convert.ToInt32(row.Cells[3].Value);
                numericUpDown5.Value = Convert.ToInt32(row.Cells[4].Value);
                numericUpDown6.Value = Convert.ToInt32(row.Cells[5].Value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                //güncelle
                int nerede = dataGridView1.CurrentCell.RowIndex;
                if (_takimlar.Count > 0 && dataGridView1.CurrentCell.RowIndex > -1)
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];

                    foreach (var item in _takimlar)
                    {
                        if (item.TakimAdi == row.Cells[0].Value.ToString())
                        {
                            item.TakimAdi = textBox1.Text;
                            item.Galibiyet = (int)numericUpDown2.Value;
                            item.Beraberlik = (int)numericUpDown3.Value;
                            item.Maglubiyet = (int)numericUpDown4.Value;
                            item.AtilanGol = (int)numericUpDown5.Value;
                            item.YenilenGol = (int)numericUpDown6.Value;
                            item.ToplamMacSayisi = item.MasSayisi();
                            item.Averaj = item.AverajHesapla();
                            item.Puan = item.PuanHesapla();
                            break;
                        }
                    }

                    VeriYenile();

                    //imleci eski yerine alalım
                    dataGridView1.CurrentCell = dataGridView1.Rows[nerede].Cells[0];
                }
            }
        }

        private void VeriYenile()
        {
            //_takimlar.Sort((x, y) => x.Puan.CompareTo(y.Puan)); //1 sütuna göre olsaydı

            _takimlar = _takimlar.OrderBy(x => x.Puan)
                           .ThenBy(x => x.Averaj)
                           .ToList();
            _takimlar.Reverse();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _takimlar;

            dataGridView1.Columns["Galibiyet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Beraberlik"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Maglubiyet"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["AtilanGol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["YenilenGol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["ToplamMacSayisi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Averaj"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Puan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["ToplamMacSayisi"].DisplayIndex = 1;
        }
    }
}

class TakimClass
{
    [DisplayName("Takım Adı")]
    public string TakimAdi { get; set; }
    public int Galibiyet { get; set; }
    public int Beraberlik { get; set; }
    [DisplayName("Mağlubiyet")]
    public int Maglubiyet { get; set; }
    [DisplayName("Atılan Gol")]
    public int AtilanGol { get; set; }
    [DisplayName("Yenilen Gol")]
    public int YenilenGol { get; set; }

    //hesaplananlar
    [DisplayName("Toplam Maç Sayısı")]
    public int ToplamMacSayisi { get; set; }
    public int Averaj { get; set; }
    public int Puan { get; set; }


    public TakimClass()
    {
    }

    public TakimClass(string v1, int v2, int v3, int v4, int v5, int v6)
    {
        TakimAdi = v1;
        Galibiyet = v2;
        Beraberlik = v3;
        Maglubiyet = v4;
        AtilanGol = v5;
        YenilenGol = v6;
    }

    public int MasSayisi()
    {
        int sonuc = Galibiyet + Beraberlik + Maglubiyet;
        return sonuc;
    }

    public int AverajHesapla()
    {
        int sonuc = AtilanGol - YenilenGol;
        return sonuc;
    }

    public int PuanHesapla()
    {
        int sonuc = Galibiyet * 3 + Beraberlik;
        return sonuc;
    }

}
