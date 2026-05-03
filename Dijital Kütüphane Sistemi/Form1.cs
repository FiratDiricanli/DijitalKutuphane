using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dijital_Kütüphane_Sistemi
{
    // --- 1. KİTAP SINIFI ---
    public class Kitap
    {
        [DisplayName("Kitap ID")]
        public int kitap_id { get; set; }

        [DisplayName("Kitap Adı")]
        public string ad { get; set; }

        [DisplayName("Yazar")]
        public string yazar { get; set; }

        [DisplayName("Tür")]
        public string tur { get; set; }

        [DisplayName("Stok")]
        public int stok { get; set; }

        public override string ToString() => $"{ad} ({yazar})";
    }

    // --- 2. ÜYE SINIFI ---
    public class Uye
    {
        [DisplayName("Üye ID")]
        public int uye_id { get; set; }

        [DisplayName("Ad Soyad")]
        public string ad_soyad { get; set; }

        [DisplayName("E-Posta")]
        public string eposta { get; set; }

        public override string ToString() => ad_soyad;
    }

    // --- 3. ÖDÜNÇ İŞLEM SINIFI ---
    public class OduncIslem
    {
        [DisplayName("İşlem ID")]
        public int islem_id { get; set; }

        [DisplayName("Kitap")]
        public string kitap_adi { get; set; }

        [DisplayName("Üye")]
        public string uye_adi { get; set; }

        [DisplayName("Alış Tarihi")]
        public string alis_tarihi { get; set; }

        [DisplayName("Durum")]
        public string durum { get; set; } // "Ödünçte" veya "Teslim Edildi"
    }

    public partial class Form1 : Form
    {
        List<Kitap> kütüphane = new List<Kitap>();
        List<Uye> üyeler = new List<Uye>();
        List<OduncIslem> oduncKayitlari = new List<OduncIslem>();
        int islemSayac = 1001;

        TabControl sekmeler;
        TabPage sekmeEnvanter, sekmeUyeler, sekmeOdunc;
        DataGridView dgvEnvanter, dgvUyeler, dgvOdunc;
        ComboBox cmbKitapSec, cmbUyeSec;
        TextBox txtKitapAd, txtYazar, txtStok, txtUyeAd, txtEposta;

        public Form1()
        {
            this.Text = "Dijital Kütüphane Sistemi - 2300005412 Fırat Diricanlı";
            this.Size = new Size(1150, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            VerileriYukle();
            ArayuzuKur();
        }

        private void VerileriYukle()
        {
            // 20 Bilindik Kitap
            kütüphane.Add(new Kitap { kitap_id = 1, ad = "Nutuk", yazar = "Mustafa Kemal Atatürk", tur = "Tarih", stok = 5 });
            kütüphane.Add(new Kitap { kitap_id = 2, ad = "Harry Potter ve Felsefe Taşı", yazar = "J.K. Rowling", tur = "Fantastik", stok = 10 });
            kütüphane.Add(new Kitap { kitap_id = 3, ad = "Game of Thrones: Taht Oyunları", yazar = "George R.R. Martin", tur = "Fantastik", stok = 8 });
            kütüphane.Add(new Kitap { kitap_id = 4, ad = "Suç ve Ceza", yazar = "Dostoyevski", tur = "Klasik", stok = 3 });
            kütüphane.Add(new Kitap { kitap_id = 5, ad = "1984", yazar = "George Orwell", tur = "Distopya", stok = 12 });
            kütüphane.Add(new Kitap { kitap_id = 6, ad = "Hayvan Çiftliği", yazar = "George Orwell", tur = "Siyaset", stok = 7 });
            kütüphane.Add(new Kitap { kitap_id = 7, ad = "Simyacı", yazar = "Paulo Coelho", tur = "Felsefe", stok = 15 });
            kütüphane.Add(new Kitap { kitap_id = 8, ad = "Küçük Prens", yazar = "Antoine de Saint-Exupéry", tur = "Çocuk", stok = 20 });
            kütüphane.Add(new Kitap { kitap_id = 9, ad = "Dune", yazar = "Frank Herbert", tur = "Bilim Kurgu", stok = 6 });
            kütüphane.Add(new Kitap { kitap_id = 10, ad = "Yüzüklerin Efendisi", yazar = "J.R.R. Tolkien", tur = "Fantastik", stok = 4 });
            kütüphane.Add(new Kitap { kitap_id = 11, ad = "Sherlock Holmes", yazar = "Arthur Conan Doyle", tur = "Polisiye", stok = 9 });
            kütüphane.Add(new Kitap { kitap_id = 12, ad = "Sefiller", yazar = "Victor Hugo", tur = "Klasik", stok = 5 });
            kütüphane.Add(new Kitap { kitap_id = 13, ad = "Dönüşüm", yazar = "Franz Kafka", tur = "Klasik", stok = 11 });
            kütüphane.Add(new Kitap { kitap_id = 14, ad = "Fahrenheit 451", yazar = "Ray Bradbury", tur = "Bilim Kurgu", stok = 8 });
            kütüphane.Add(new Kitap { kitap_id = 15, ad = "Cesur Yeni Dünya", yazar = "Aldous Huxley", tur = "Distopya", stok = 7 });
            kütüphane.Add(new Kitap { kitap_id = 16, ad = "Otomatik Portakal", yazar = "Anthony Burgess", tur = "Psikolojik", stok = 5 });
            kütüphane.Add(new Kitap { kitap_id = 17, ad = "Kürk Mantolu Madonna", yazar = "Sabahattin Ali", tur = "Aşk", stok = 14 });
            kütüphane.Add(new Kitap { kitap_id = 18, ad = "İnce Memed", yazar = "Yaşar Kemal", tur = "Edebiyat", stok = 6 });
            kütüphane.Add(new Kitap { kitap_id = 19, ad = "Tutunamayanlar", yazar = "Oğuz Atay", tur = "Modern", stok = 4 });
            kütüphane.Add(new Kitap { kitap_id = 20, ad = "Saatleri Ayarlama Enstitüsü", yazar = "Ahmet Hamdi Tanpınar", tur = "Eleştiri", stok = 5 });

            üyeler.Add(new Uye { uye_id = 501, ad_soyad = "Fırat Diricanlı", eposta = "firat@mail.com" });
        }

        private void ArayuzuKur()
        {
            sekmeler = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11, FontStyle.Bold) };

            sekmeEnvanter = new TabPage("📚 Kitap Envanteri");
            sekmeUyeler = new TabPage("👥 Üye Yönetimi");
            sekmeOdunc = new TabPage("🔄 Ödünç Alma / İade");

            // --- KİTAP ENVANTER SEKMESİ ---
            Panel pnlKitap = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = Color.WhiteSmoke };
            txtKitapAd = new TextBox { Location = new Point(20, 40), Width = 150, PlaceholderText = "Kitap Adı" };
            txtYazar = new TextBox { Location = new Point(180, 40), Width = 150, PlaceholderText = "Yazar" };
            txtStok = new TextBox { Location = new Point(340, 40), Width = 60, PlaceholderText = "Stok" };
            Button btnKitapEkle = new Button { Text = "KİTAP EKLE", Location = new Point(410, 38), Size = new Size(120, 30), BackColor = Color.SteelBlue, ForeColor = Color.White };
            btnKitapEkle.Click += (s, e) => {
                if (int.TryParse(txtStok.Text, out int st))
                {
                    kütüphane.Add(new Kitap { kitap_id = kütüphane.Count + 1, ad = txtKitapAd.Text, yazar = txtYazar.Text, stok = st });
                    TablolariGuncelle();
                    ComboDoldur();
                }
            };
            dgvEnvanter = CreateGrid();
            pnlKitap.Controls.AddRange(new Control[] { txtKitapAd, txtYazar, txtStok, btnKitapEkle });
            sekmeEnvanter.Controls.Add(dgvEnvanter);
            sekmeEnvanter.Controls.Add(pnlKitap);

            // --- ÜYE SEKMESİ ---
            Panel pnlUye = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.WhiteSmoke };
            txtUyeAd = new TextBox { Location = new Point(20, 35), Width = 200, PlaceholderText = "Ad Soyad" };
            txtEposta = new TextBox { Location = new Point(230, 35), Width = 200, PlaceholderText = "E-Posta" };
            Button btnUyeEkle = new Button { Text = "ÜYE KAYDET", Location = new Point(440, 33), Size = new Size(120, 30), BackColor = Color.SeaGreen, ForeColor = Color.White };
            btnUyeEkle.Click += (s, e) => {
                üyeler.Add(new Uye { uye_id = üyeler.Count + 501, ad_soyad = txtUyeAd.Text, eposta = txtEposta.Text });
                TablolariGuncelle();
                ComboDoldur();
            };
            dgvUyeler = CreateGrid();
            pnlUye.Controls.AddRange(new Control[] { txtUyeAd, txtEposta, btnUyeEkle });
            sekmeUyeler.Controls.Add(dgvUyeler);
            sekmeUyeler.Controls.Add(pnlUye);

            // --- ÖDÜNÇ / İADE SEKMESİ ---
            Panel pnlOdunc = new Panel { Dock = DockStyle.Top, Height = 150, BackColor = Color.WhiteSmoke };
            Label l1 = new Label { Text = "Kitap Seç:", Location = new Point(20, 20), AutoSize = true };
            cmbKitapSec = new ComboBox { Location = new Point(100, 18), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            Label l2 = new Label { Text = "Üye Seç:", Location = new Point(20, 60), AutoSize = true };
            cmbUyeSec = new ComboBox { Location = new Point(100, 58), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Button btnOduncVer = new Button { Text = "KİTABI ÖDÜNÇ VER", Location = new Point(320, 18), Size = new Size(180, 70), BackColor = Color.DarkOrange, ForeColor = Color.White };
            btnOduncVer.Click += (s, e) => KitapOduncVer();

            // --- YENİ EKLENEN İADE AL BUTONU ---
            Button btnIadeAl = new Button { Text = "KİTABI İADE AL", Location = new Point(510, 18), Size = new Size(180, 70), BackColor = Color.Indigo, ForeColor = Color.White };
            btnIadeAl.Click += (s, e) => KitapIadeAl();

            dgvOdunc = CreateGrid();
            pnlOdunc.Controls.AddRange(new Control[] { l1, cmbKitapSec, l2, cmbUyeSec, btnOduncVer, btnIadeAl });
            sekmeOdunc.Controls.Add(dgvOdunc);
            sekmeOdunc.Controls.Add(pnlOdunc);

            sekmeler.TabPages.AddRange(new TabPage[] { sekmeEnvanter, sekmeUyeler, sekmeOdunc });
            this.Controls.Add(sekmeler);

            ComboDoldur();
            TablolariGuncelle();
        }

        private DataGridView CreateGrid()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
        }

        private void ComboDoldur()
        {
            cmbKitapSec.Items.Clear();
            cmbUyeSec.Items.Clear();
            foreach (var k in kütüphane) if (k.stok > 0) cmbKitapSec.Items.Add(k);
            foreach (var u in üyeler) cmbUyeSec.Items.Add(u);
        }

        private void KitapOduncVer()
        {
            if (cmbKitapSec.SelectedItem is Kitap seciliKitap && cmbUyeSec.SelectedItem is Uye seciliUye)
            {
                seciliKitap.stok--;
                oduncKayitlari.Add(new OduncIslem
                {
                    islem_id = islemSayac++,
                    kitap_adi = seciliKitap.ad,
                    uye_adi = seciliUye.ad_soyad,
                    alis_tarihi = DateTime.Now.ToShortDateString(),
                    durum = "Ödünçte"
                });
                TablolariGuncelle();
                ComboDoldur();
                MessageBox.Show($"{seciliKitap.ad} kitabı {seciliUye.ad_soyad} üyesine verildi.");
            }
        }

        // --- İADE ALMA METODU ---
        private void KitapIadeAl()
        {
            if (dgvOdunc.SelectedRows.Count > 0)
            {
                var seciliIslem = dgvOdunc.SelectedRows[0].DataBoundItem as OduncIslem;

                if (seciliIslem != null && seciliIslem.durum == "Ödünçte")
                {
                    // Kitabı bulup stoğunu artıralım
                    var kitap = kütüphane.FirstOrDefault(k => k.ad == seciliIslem.kitap_adi);
                    if (kitap != null) kitap.stok++;

                    // İşlem durumunu güncelleyelim
                    seciliIslem.durum = "Teslim Edildi";

                    TablolariGuncelle();
                    ComboDoldur();
                    MessageBox.Show($"{seciliIslem.kitap_adi} iade alındı ve stoka eklendi.");
                }
                else if (seciliIslem != null && seciliIslem.durum == "Teslim Edildi")
                {
                    MessageBox.Show("Bu kitap zaten iade edilmiş kral!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen iade edilecek işlemi tablodan seçin.");
            }
        }

        private void TablolariGuncelle()
        {
            dgvEnvanter.DataSource = null; dgvEnvanter.DataSource = kütüphane.ToList();
            dgvUyeler.DataSource = null; dgvUyeler.DataSource = üyeler.ToList();
            dgvOdunc.DataSource = null; dgvOdunc.DataSource = oduncKayitlari.ToList();
        }
    }
}