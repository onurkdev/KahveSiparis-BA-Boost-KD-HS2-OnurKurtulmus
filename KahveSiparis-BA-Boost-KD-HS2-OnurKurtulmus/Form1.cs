using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveSiparis_BA_Boost_KD_HS2_OnurKurtulmus
{
    public partial class Form1 : Form
    {
        
        List<SiparisItem> siparisler = new List<SiparisItem>();

        Dictionary<string, double> kahvelerList = new Dictionary<string, double> {
            {"Misto",4.5 },
            {"Americano", 5.75 },
            {"Bianco", 6 },
            {"Macchiato", 6.75 },
            {"Con Panna", 8},
            {"Mocha", 7.75 }
        };
        Dictionary<string, double > sicakIceceklerList = new Dictionary<string, double> {
            {"Çay", 3 },
            {"Hot Chocolate", 4.5},
            {"Chai Tea Latte", 6.5}
        };



        Dictionary<string, double> sogukIceceklerList = new Dictionary<string, double>
        {
            {"Kola", sogukIcecekFiyat },
            {"Fanta", sogukIcecekFiyat }
        };
        public string musteriAdi = "";
        public string musteriTelefon = "";
        public string musteriAdres = "";
        public static KeyValuePair<string,double> shotMultiplier1x = new KeyValuePair<string, double> ("Shot x1", 0.75 );
        public static KeyValuePair<string, double> shotMultiplier2x = new KeyValuePair<string, double>("Shot x2", 1.5);
        public static KeyValuePair<string, double> shotMultiplier0x = new KeyValuePair<string, double>("", 0);
        public static KeyValuePair<string, double> yagsizSut = new KeyValuePair<string, double>("Yağsız", 0.5);
        public static KeyValuePair<string, double> soyaSut = new KeyValuePair<string, double>("Soya", 0.5);
        public static KeyValuePair<string, double> icecekBoyutTall = new KeyValuePair<string, double>("Tall", 1);
        public static KeyValuePair<string, double> icecekBoyutGrande = new KeyValuePair<string, double>("Grande", 1.25);
        public static KeyValuePair<string, double> icecekBoyutVenti = new KeyValuePair<string, double>("Venti", 1.75);
        public static double sogukIcecekFiyat = 5.5;
        
        KeyValuePair<string, double> selectedShot = new KeyValuePair<string, double>("", 0);
        KeyValuePair<string, double> selectedIcecekBoyut = icecekBoyutTall; //boy default değer Tall olarak ayarlandı
        KeyValuePair<string, double> selectedSut = new KeyValuePair<string, double>("Normal Süt", 0);
        double icecekAdet = 0;
        double tumtoplam = 0;
        public Form1()
        {
            InitializeComponent();
            listsInitilazer();
            

        }
        void listsInitilazer() { 
            foreach (KeyValuePair<string, double> kahve in kahvelerList)
            {
                string item = kahve.Key;
                kahvelerComboBox.Items.Add(item);
            }
            foreach (KeyValuePair<string, double> sicakicecek in sicakIceceklerList)
            {
                string item = sicakicecek.Key;
                sicakComboBox.Items.Add(item);
            }
            foreach (KeyValuePair<string, double> sogukicecek in sogukIceceklerList) {
                string item = sogukicecek.Key;
                sogukComboBox.Items.Add(item);
            }
            bardakBoyutTallRadioButton.Checked = true;
        
        }
        private void hesaplaBtn_Click(object sender, EventArgs e)
        {
            if (kahvelerComboBox.SelectedIndex == -1 && sicakComboBox.SelectedIndex == -1 && sogukComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir ürün seçiniz!");
                return;
            }
            
            {
                if (kahvelerComboBox.SelectedIndex != -1)
                {
                    if(kahveAdet.Value==0)
                    {
                        MessageBox.Show("Lütfen adet giriniz!");
                        return;

                    }
                    else
                    {
                        double adet = Convert.ToDouble(kahveAdet.Value);
                        string selectedkahve = kahvelerComboBox.SelectedItem.ToString();
                        double siparisfiyat = (double)kahvelerList[selectedkahve];
                        SiparisItem newsiparis = new SiparisItem(
                            icecektipi: "Kahve",
                            icecekadi: selectedkahve,
                            icecekboyut: selectedIcecekBoyut,
                            icecekshot: selectedShot,
                            iceceksut: selectedSut,
                            icecekadet: adet,
                            icecekbasefiyat: siparisfiyat);
                        siparislerListBox.Items.Add(newsiparis.IcecekListItemString);
                        tumtoplam += newsiparis.IcecekFiyat;
                        icecekAdet += adet;
                        totalSipLabel.Text = $"Toplam Sipariş Tutarı : {tumtoplam} TL";
                        clearer();
                    }
                    
                }

                if (sogukComboBox.SelectedIndex != -1)
                {
                    if (sogukAdet.Value == 0)
                    {
                        MessageBox.Show("Lütfen adet giriniz!");
                        return;

                    }
                    else
                    {
                        double adet = Convert.ToDouble(sogukAdet.Value);
                        string selectedsoguk = sogukComboBox.SelectedItem.ToString();
                        double siparisfiyat = sogukIcecekFiyat;
                        SiparisItem newsiparis = new SiparisItem("Soğuk", selectedsoguk, siparisfiyat,adet);
                        siparislerListBox.Items.Add(newsiparis.IcecekListItemString);
                        tumtoplam += newsiparis.IcecekFiyat;
                        icecekAdet += adet;
                        totalSipLabel.Text = $"Toplam Sipariş Tutarı : {tumtoplam} TL";
                        clearer();
                    }

                }
                if (sicakComboBox.SelectedIndex != -1)
                {
                    if (sicakAdet.Value == 0)
                    {
                        MessageBox.Show("Lütfen adet giriniz!");
                        return;

                    }
                    else
                    {
                        double adet = Convert.ToDouble(sicakAdet.Value);
                        string selectedsicak = sicakComboBox.SelectedItem.ToString();
                        double siparisfiyat = (double)sicakIceceklerList[selectedsicak];
                        SiparisItem newsiparis = new SiparisItem(icecektipi: "Sıcak İçecek", icecekadi: selectedsicak, siparisfiyat, selectedIcecekBoyut, adet);
                        siparislerListBox.Items.Add(newsiparis.IcecekListItemString);
                        tumtoplam += newsiparis.IcecekFiyat;
                        icecekAdet += adet;
                        totalSipLabel.Text = $"Toplam Sipariş Tutarı : {tumtoplam} TL";
                        clearer();
                    }
                }
            }
            
            

        }

        void clearer ()
        {
            kahvelerComboBox.SelectedIndex = -1;
            sogukComboBox.SelectedIndex = -1;
            sicakComboBox.SelectedIndex = -1;
            yagsizsutRadioButton.Checked = false;
            soyasutRadioButton.Checked = false;
            bardakBoyutGrandeRadioButton.Checked=false;
            bardakBoyutVentiRadioButton.Checked = false;
            bardakBoyutTallRadioButton.Checked = true;
            kahveAdet.Value = 0;
            sicakAdet.Value = 0;
            sogukAdet.Value = 0;
            shot1xCheckBox.Checked = false;
            shotx2CheckBox.Checked = false;
            selectedIcecekBoyut = icecekBoyutTall;
            selectedShot = shotMultiplier0x;
            selectedSut = new KeyValuePair<string, double>("Normal Süt", 0);


        }
        class MusteriDetay
        {
            public int MyProperty { get; set; }
            public string MusteriAdi;
            public string MusteriAdres;
            public string MusteriTelefon;
            public List<SiparisItem> SiparisList;
            public MusteriDetay (string musteriadi,string musteriadres, string musteritelefon, List<SiparisItem> siparisListesi )
            {
                MusteriAdi = musteriadi;
                MusteriAdres = musteriadres;
                MusteriTelefon = musteritelefon;
                SiparisList = siparisListesi;

            }
            public void serverload() { }
        }
        
       




        class SiparisItem
        {
            public string IcecekTipi;
            public string IcecekAdi;
            public KeyValuePair<string, double> IcecekBoyut;
            public KeyValuePair<string,double> IcecekShot ;
            public double IcecekBaseFiyat;
            public KeyValuePair<string, double> IcecekSut ;
            public double IcecekAdet;
            public double IcecekFiyat;
            public string IcecekListItemString;

            public SiparisItem(string icecektipi , string icecekadi, KeyValuePair<string, double> icecekboyut, KeyValuePair<string, double> icecekshot, double icecekbasefiyat, KeyValuePair<string, double> iceceksut , double icecekadet  )
            {
                IcecekTipi = icecektipi;
                IcecekAdi = icecekadi;
                IcecekBoyut = icecekboyut;
                IcecekShot = icecekshot;
                IcecekBaseFiyat = icecekbasefiyat;
                IcecekSut = iceceksut;
                IcecekAdet = icecekadet;
                IcecekFiyat = (icecekbasefiyat + icecekshot.Value + iceceksut.Value)*icecekadet*icecekboyut.Value;
                IcecekListItemString = icecekadet + " adet " +icecekboyut.Key + " " + icecekadi + " " + iceceksut.Key + " " + icecekshot.Key + " : " + IcecekFiyat.ToString() +" - TL";


            }
            public SiparisItem(string icecektipi, string icecekadi, double icecekbasefiyat, double icecekadet)
            {
                IcecekTipi = icecektipi;
                IcecekAdi = icecekadi;
                IcecekBaseFiyat = icecekbasefiyat;
                IcecekAdet = icecekadet;
                IcecekFiyat = icecekbasefiyat*icecekadet;
                IcecekListItemString = icecekadet + " adet " + icecekadi + " : " + IcecekFiyat + " - TL";

            }
            public SiparisItem (string icecektipi, string icecekadi, double icecekbasefiyat , KeyValuePair<string, double> icecekboyut, double icecekadet)
            {
                IcecekTipi = icecektipi;
                IcecekAdi = icecekadi;
                IcecekBoyut = icecekboyut;
                
                IcecekBaseFiyat = icecekbasefiyat;
                
                IcecekAdet = icecekadet;
                IcecekFiyat = icecekbasefiyat* icecekadet* icecekboyut.Value;
                IcecekListItemString = icecekadet + " adet "+ icecekboyut.Key + " " + icecekadi  +  " : " + IcecekFiyat.ToString() + " - TL";

            }
                
        }

        private void sicakComboBox_Enter(object sender, EventArgs e)
        {
            sogukComboBox.SelectedIndex = -1;
            kahvelerComboBox.SelectedIndex = -1;
            sogukAdet.Value = 0;
            kahveAdet.Value = 0;
        }

        private void sogukComboBox_Enter(object sender, EventArgs e)
        {
            kahvelerComboBox.SelectedIndex = -1;
            sicakComboBox.SelectedIndex = -1;
            sicakAdet.Value = 0;
            kahveAdet.Value = 0;
        }

        private void kahvelerComboBox_Enter(object sender, EventArgs e)
        {
            sogukComboBox.SelectedIndex = -1;
            sicakComboBox.SelectedIndex = -1;
            sogukAdet.Value = 0;
            sicakAdet.Value = 0;

        }

        private void shot1xCheckBox_Click(object sender, EventArgs e)
        {
            
            shotx2CheckBox.Checked = false;
            selectedShot = shotSelector("1x");
            if (!shot1xCheckBox.Checked) selectedShot = shotSelector("0x");

            
        }


        private void shotx2CheckBox_Click(object sender, EventArgs e)
        {
            shot1xCheckBox.Checked = false;
            selectedShot = shotSelector("2x");
            if (!shotx2CheckBox.Checked) {
                selectedShot=shotSelector("0x");
                
            };
        }
        KeyValuePair<string, double> shotSelector(string selectedShot)
        {
            KeyValuePair<string, double> multiplier = new KeyValuePair<string, double>();
            if (selectedShot == "1x") { multiplier = shotMultiplier1x; }
            if (selectedShot == "2x") { multiplier = shotMultiplier2x; }
            if (selectedShot == "0x") { multiplier = shotMultiplier0x; }
            return multiplier;
        }


        private void yagsizsutRadioButton_Click(object sender, EventArgs e)
        {
            soyasutRadioButton.Checked = false;
            selectedSut = yagsizSut;
        }

        private void soyasutRadioButton_Click(object sender, EventArgs e)
        {
            yagsizsutRadioButton.Checked = false;
            selectedSut = soyaSut;
        }

        private void bardakBoyutVentiRadioButton_Click(object sender, EventArgs e)
        {
            bardakBoyutGrandeRadioButton.Checked = false;
            bardakBoyutTallRadioButton.Checked = false;
            selectedIcecekBoyut = icecekBoyutVenti;
        }

        private void bardakBoyutGrandeRadioButton_Click(object sender, EventArgs e)
        {
            bardakBoyutVentiRadioButton.Checked = false;
            bardakBoyutTallRadioButton.Checked = false;
            selectedIcecekBoyut = icecekBoyutGrande;
        }

        private void bardakBoyutTallRadioButton_Click(object sender, EventArgs e)
        {
            bardakBoyutGrandeRadioButton.Checked = false;
            bardakBoyutVentiRadioButton.Checked = false;
            selectedIcecekBoyut = icecekBoyutTall;
        }

        private void siparisVerBtn_Click(object sender, EventArgs e)
        {
            if (adsoyadTextbox.Text == "" || telefonTextbox.Text == "" || adresTextbox.Text== "")
            {
                MessageBox.Show("Lütfen Müşteri Bilgilerini tam giriniz!");
            }
            else
            {
                MusteriDetay newmusteri = new MusteriDetay(
                    musteriadi: adsoyadTextbox.Text, 
                    musteritelefon: telefonTextbox.Text,
                    musteriadres: adresTextbox.Text, 
                    siparisListesi: siparisler
                    );
                
                string message = $"Toplam {icecekAdet} Adet siparişiniz bulunmaktadır \n {tumtoplam} - TL Tutarındadır";
                MessageBox.Show($"{message} \nMüşteri Adı:{newmusteri.MusteriAdi} \nTeslimat Adresi:{newmusteri.MusteriAdres}\nTelefon:{newmusteri.MusteriTelefon} ");
                clearer();

            }


        }

    }
    

}
