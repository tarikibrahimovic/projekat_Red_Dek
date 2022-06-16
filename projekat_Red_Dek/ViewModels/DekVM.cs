using projekat_Red_Dek.Models;
using projekat_Red_Dek.ViewModels.Commands;
using projekat_Red_Dek.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels
{
    public class DekVM : BaseViewModel, INotifyPropertyChanged
    {
        private string query;
        public ObservableCollection<Clan> DekPrikaz { get; set; }
        public ObservableCollection<Linija> LinijaPrikaz { get; set; }
        public List<Clan> NizObjekata { get; set; }
        public List<Linija> NizLinija { get; set; }
        public Red Dek { get; set; }
        private double windowWidth;
        private double windowHeight;
        public string nazivDeka { get; set; }
        public string dekZaPretrazit { get; set; }
        public int BrojReda { get; set; }
        public SaveCommandDek SaveCommandDek { get; }
        public LoadCommandDek LoadCommandDek { get; }
        public CreateCommandDek CreateCommandDek { get; }
        public DeleteCommandDek DeleteCommandDek { get; }
        public ClearCommandDek ClearCommandDek { get; }

        public DB baza;
        private int mestoDod;
        private List<string> vrednostiClanova;

        public List<string> VrednostiClanova
        {
            get { return vrednostiClanova; }
            set { vrednostiClanova = value; }
        }

        public int MestoDod
        {
            get { return mestoDod; }
            set
            {
                mestoDod = value;
                OnPropertyChanged("MestoDod");
            }
        }


        public double WindowHeight
        {
            get { return windowHeight; }
            set
            {
                windowHeight = value;
                OnPropertyChanged("WindowHeight");
            }
        }
        public double WindowWidth
        {
            get { return windowWidth; }
            set
            {
                windowWidth = value;
                OnPropertyChanged("WindowWidnt");
            }
        }
        private double canvasHeight;
        public double CanvasHeight
        {
            get { return canvasHeight; }
            set
            {
                canvasHeight = value;
                OnPropertyChanged("CanvasHeight");
            }
        }
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }


        public DekVM()
        {
            SaveCommandDek = new SaveCommandDek(this);
            LoadCommandDek = new LoadCommandDek(this);
            CreateCommandDek = new CreateCommandDek(this);
            DeleteCommandDek = new DeleteCommandDek(this);
            ClearCommandDek = new ClearCommandDek(this);
            baza = new DB();
            Dek = new Red();
            DekPrikaz = new ObservableCollection<Clan>();
            LinijaPrikaz = new ObservableCollection<Linija>();
            NizObjekata = new List<Clan>();
            NizLinija = new List<Linija>();
            VrednostiClanova = new List<string>();
            BrojReda = 1;
            MestoDod = 0;
        }

        public void DodajObjekat()
        {
            if (Query.Length > 6)
            {
                nacrtaj(Query.Substring(0, 6));
            }
            else
            {
                nacrtaj(Query);
            }
            Query = "";
        }
        public void nacrtaj(string v)
        {
            Clan clan = new Clan();
            NizObjekata = Dodaj(clan, v);
            LinijaPrikaz.Clear();
            foreach (var a in NizLinija)
            {
                LinijaPrikaz.Add(a);
            }
            DekPrikaz.Clear();
            foreach (var b in NizObjekata)
            {
                DekPrikaz.Add(b);
            }
        }

        public void promeniVrednosti()
        {
            if (MestoDod == 1)
            {
                for (int i = 3; i < NizObjekata.Count; i++)
                {
                    NizObjekata[i].Vrednost = VrednostiClanova[i - 3];
                }
                NizObjekata[2].Vrednost = VrednostiClanova[VrednostiClanova.Count - 1];
                string pom = VrednostiClanova[VrednostiClanova.Count - 1];
                VrednostiClanova.RemoveAt(VrednostiClanova.Count - 1);
                VrednostiClanova.Insert(0, pom);
                mestoDod = 0;
            }
        }

        public List<Clan> Dodaj(Clan c, string value)
        {
            if (NizObjekata.Count == 0)
            {
                BrojReda = 1;
                Clan prvi = new Clan();
                prvi.Vrednost = "Start";
                prvi.Pozicija.X = 0;
                prvi.Pozicija.Y = 0;
                prvi.Sledeci = c;
                NizObjekata.Add(prvi);

                Clan poslednji = new Clan();
                poslednji.Vrednost = "End";
                poslednji.Pozicija.X = 0;
                poslednji.Pozicija.Y = 200;
                poslednji.Prethodni = c;
                NizObjekata.Add(poslednji);

                c.Prethodni = prvi;
                c.Sledeci = poslednji;
                izracunajPoziciju(c);
                nacrtajLiniju(prvi, c);
                nacrtajLiniju(c, poslednji);
                NizObjekata.Add(c);
            }
            else
            {
                Clan poslednji = NizObjekata[1];
                poslednji.Prethodni = c;
                NizObjekata[NizObjekata.Count - 1].Sledeci = c;
                c.Prethodni = NizObjekata[NizObjekata.Count - 1];
                c.Sledeci = poslednji;
                izracunajPoziciju(c);
                proveriPoziciju();
                nacrtajLiniju(c, poslednji);
                nacrtajLiniju(c.Prethodni, c);
                izbrisiLiniju(c.Prethodni, poslednji);
                NizObjekata.Add(c);
            }
            c.Vrednost = value;
            VrednostiClanova.Add(value);
            promeniVrednosti();
            CanvasHeight = NizObjekata[1].Pozicija.Y + 70;
            return NizObjekata;
        }

        private void izracunajPoziciju(Clan c)
        {
            if (BrojReda % 2 == 1)
            {
                if (NizObjekata.Count == 2)
                {
                    c.Pozicija.X = -(windowWidth / 2 - 40);
                    c.Pozicija.Y = 70;
                }
                else
                {
                    c.Pozicija.X = c.Prethodni.Pozicija.X + 150;
                    c.Pozicija.Y = c.Prethodni.Pozicija.Y;
                }
            }
            else
            {
                c.Pozicija.X = c.Prethodni.Pozicija.X - 150;
                c.Pozicija.Y = c.Prethodni.Pozicija.Y;
            }
        }

        public void proveriPoziciju()
        {
            Clan poslednji = NizObjekata[1].Prethodni;
            if (poslednji.Pozicija.X >= WindowWidth / 2 - 20)
            {
                BrojReda++;
                poslednji.Pozicija.Y = poslednji.Prethodni.Pozicija.Y + 70;
                poslednji.Pozicija.X = poslednji.Prethodni.Pozicija.X - 30;
                NizObjekata[1].Pozicija.Y += 70;
            }
            else if (BrojReda % 2 == 0)
            {
                if (poslednji.Pozicija.X <= -(WindowWidth / 2 - 30))
                {
                    BrojReda = 1;
                    poslednji.Pozicija.Y = poslednji.Prethodni.Pozicija.Y + 70;
                    poslednji.Pozicija.X = poslednji.Prethodni.Pozicija.X + 30;
                    NizObjekata[1].Pozicija.Y += 70;
                }
            }
        }

        public void nacrtajLiniju(Clan c1, Clan c2)
        {
            double x1 = c1.Pozicija.X;
            double x2 = c2.Pozicija.X + 32;
            if (NizObjekata.Count == 2)
            {
                Linija l = new Linija(x1, c1.Pozicija.Y, x2, c2.Pozicija.Y);
                l.Desni = c2;
                l.Levi = c1;
                NizLinija.Add(l);
            }
            else
            {
                if (BrojReda % 2 == 1)
                {
                    Linija l = new Linija(x1 + 32, c1.Pozicija.Y, x2 - 32, c2.Pozicija.Y);
                    NizLinija.Add(l);
                    l.Desni = c2;
                    l.Levi = c1;
                }
                else
                {
                    Linija l = new Linija(x1, c1.Pozicija.Y, x2, c2.Pozicija.Y);
                    NizLinija.Add(l);
                    l.Desni = c2;
                    l.Levi = c1;
                }
            }
        }

        public void izbrisiLiniju(Clan c1, Clan c2)
        {
            Linija l = new Linija(c1, c2);
            int index = NizLinija.FindIndex(li => li.Levi.Pozicija.X == c1.Pozicija.X && li.Desni.Pozicija.X == c2.Pozicija.X
            && li.Levi.Pozicija.Y == c1.Pozicija.Y && li.Desni.Pozicija.Y == c2.Pozicija.Y);
            l.Levi = null;
            l.Desni = null;
            NizLinija.RemoveAt(index);
        }

        public void postaviDimenzije(double visina, double sirina)
        {
            WindowWidth = sirina;
            WindowHeight = visina;
            List<string> vrednosti = new List<string>();
            int brojClanova = NizObjekata.Count;

            for (int i = 2; i < brojClanova; i++)
            {
                vrednosti.Add(NizObjekata[i].Vrednost);
            }
            NizObjekata.Clear();
            NizLinija.Clear();
            foreach (string v in vrednosti)
            {
                nacrtaj(v);
            }
        }

        public void obrisiClan()
        {
            List<string> vrednosti = new List<string>();
            if (NizObjekata.Count >= 3)
            {
                if (NizObjekata.Count == 3)
                {
                    NizObjekata.Clear();
                    NizLinija.Clear();
                    LinijaPrikaz.Clear();
                    DekPrikaz.Clear();
                }
                else
                {
                    if (MestoDod == 0)
                    {
                        for (int i = 2; i < NizObjekata.Count - 1; i++)
                        {
                            vrednosti.Add(NizObjekata[i].Vrednost);
                        }
                    }
                    else
                    {
                        for (int i = 3; i < NizObjekata.Count; i++)
                        {
                            vrednosti.Add(NizObjekata[i].Vrednost);
                        }
                    }
                    NizObjekata.Clear();
                    NizLinija.Clear();
                    foreach (string v in vrednosti)
                    {
                        nacrtaj(v);
                    }
                }
            }
        }

        public void saveFunc()
        {
            SaveWindow saveWindow = new SaveWindow();
            saveWindow.ShowDialog();
            nazivDeka = saveWindow.Naziv;
            if (nazivDeka != "")
            {
                var res = baza.CreateDek(nazivDeka, NizObjekata);
                if (res == true)
                {
                    MessageBox.Show("Uspesno sacuvano");
                    LinijaPrikaz.Clear();
                    DekPrikaz.Clear();
                }
                else
                {
                    MessageBox.Show("Neuspesno sacuvano");
                }
            }
        }

        public void loadFunc()
        {
            LoadWindow loadWindow = new LoadWindow();
            loadWindow.ShowDialog();
            dekZaPretrazit = loadWindow.Naziv;
            List<string> vrednosti = new List<string>();
            List<string> res = baza.ReadDek(dekZaPretrazit, vrednosti);
            if (res != null)
            {
                NizObjekata.Clear();
                NizLinija.Clear();
                for (int i = 2; i < res.Count; i++)
                {
                    nacrtaj(res[i]);
                }
                MessageBox.Show("Uspesno ucitano");
            }
            else
            {
                MessageBox.Show("Neuspesno ucitano");
            }
        }

        public void mestoDodavanja(string s)
        {
            if (NizObjekata[0].ID == NizObjekata.Where(c => c.Vrednost == s).First().ID)
            {
                MestoDod = 1;
            }
            else
            {
                MestoDod = 0;
            }
        }

        public void clearAll()
        {
            NizObjekata.Clear();
            NizLinija.Clear();
            LinijaPrikaz.Clear();
            DekPrikaz.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
