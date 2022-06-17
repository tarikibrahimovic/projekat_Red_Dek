using projekat_Red_Dek.Models;
using projekat_Red_Dek.ViewModels.Commands;
using projekat_Red_Dek.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekat_Red_Dek.ViewModels
{
    public class RedVM : BaseViewModel, INotifyPropertyChanged
    {
        private string query;
        public ObservableCollection<Clan> RedPrikaz { get; set; }
        public ObservableCollection<Linija> LinijaPrikaz { get; set; }
        public List<Clan> NizObjekata { get; set; }
        public List<Linija> NizLinija { get; set; }
        public ResourcesCommandDek ResourcesCommand { get; }
        public Red Red { get; set; }
        public CreateCommand CreateCommand { get; set; }
        public DelateCommand DelateCommand { get; }
        public SaveCommand SaveCommand { get; }
        public LoadCommand LoadCommand { get; }
        public ClearCommand ClearCommand { get; }

        private double windowWidth;

        private double windowHeight;
        public string nazivReda { get; set; }
        public string redZaPretrazit { get; set; }
        public int BrojReda { get; set; }
        public DB baza;
        public int T;

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


        public RedVM()
        {
            CreateCommand = new CreateCommand(this);
            DelateCommand = new DelateCommand(this);
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
            ClearCommand = new ClearCommand(this);
            baza = new DB();
            Red = new Red();
            RedPrikaz = new ObservableCollection<Clan>();
            LinijaPrikaz = new ObservableCollection<Linija>();
            NizObjekata = new List<Clan>();
            NizLinija = new List<Linija>();
            ResourcesCommand = new ResourcesCommandDek(this);
            BrojReda = 1;
            T = 0;
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
            RedPrikaz.Clear();
            foreach (var b in NizObjekata)
            {
                RedPrikaz.Add(b);
            }
        }

        public List<Clan> Dodaj(Clan c, string value)
        {
            c.Vrednost = value;
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
            T++;
            if (T % 3 == 0)
            {
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
                    RedPrikaz.Clear();
                }
                else
                {
                    for (int i = 3; i < NizObjekata.Count; i++)
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
            }
        }

        public void saveFunc()
        {
            SaveWindow saveWindow = new SaveWindow();
            saveWindow.ShowDialog();
            nazivReda = saveWindow.Naziv;
            var res = baza.Create(nazivReda, NizObjekata);
            if (res == true)
            {
                MessageBox.Show("Uspesno sacuvano");
                LinijaPrikaz.Clear();
                RedPrikaz.Clear();
            }
            else
            {
                MessageBox.Show("Neuspesno sacuvano");
            }

        }

        public void loadFunc()
        {
            LoadWindow loadWindow = new LoadWindow();
            loadWindow.ShowDialog();
            redZaPretrazit = loadWindow.Naziv;
            List<string> vrednosti = new List<string>();
            List<string> res = baza.Read(redZaPretrazit, vrednosti);
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
        public void clearAll()
        {
            NizObjekata.Clear();
            NizLinija.Clear();
            LinijaPrikaz.Clear();
            RedPrikaz.Clear();
        }
        
        public void Resources()
        {
            string path = $"{Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)}\\Resources\\Queue_and_Deque.pdf";
            Process.Start(path);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
