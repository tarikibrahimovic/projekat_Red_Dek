using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekat_Red_Dek.Models
{
    public class Red : INotifyPropertyChanged
    {

        public List<Linija> NizLinija { get; set; }

        public List<Clan> Niz { get; set; }

        public Red()
        {
            Niz = new List<Clan>();
            NizLinija = new List<Linija>();
        }

        //public List<Clan> Dodaj(Clan c, string v)
        //{
        //    c.Vrednost = v;
            
        //    if (Niz.Count == 0)
        //    {
        //        Clan prvi = new Clan();
        //        prvi.Vrednost = "Start";
        //        izracunajPoziciju(prvi);
        //        prvi.Sledeci = c;
        //        Niz.Add(prvi);
                
        //        Clan poslednji = new Clan();
        //        poslednji.Vrednost = "End";
        //        izracunajPoziciju(poslednji);
        //        poslednji.Prethodni = c;
        //        Niz.Add(poslednji);
                
        //        c.Prethodni = prvi;
        //        c.Sledeci = poslednji;
        //        nacrtajLiniju(prvi,c);
        //        nacrtajLiniju(c,poslednji);
        //        izracunajPoziciju(c);
        //        Niz.Add(c);
        //    }
            
        //    else
        //    {
        //        Clan poslednji = Niz[1];
        //        poslednji.Prethodni = c;
        //        Niz[Niz.Count - 1].Sledeci = c;
        //        c.Prethodni = Niz[Niz.Count - 1];
        //        c.Sledeci = poslednji;
        //        nacrtajLiniju(c, poslednji);
        //        nacrtajLiniju(c.Prethodni, c);
        //        izbrisiLiniju(c.Prethodni, poslednji);
        //        izracunajPoziciju(c);
        //        Niz.Add(c);
        //    }
        //    return Niz;
        //}

        //public List<Clan> Brisi()
        //{

        //    if(Niz.Count == 3)
        //    {
        //        Niz.Clear();
        //        NizLinija.Clear();
        //    }
        //    else
        //    {
        //        Clan c = Niz[2];
        //        Clan prvi = Niz[0];
        //        izbrisiLiniju(prvi,c);
        //        izbrisiLiniju(c, c.Sledeci);
        //        nacrtajLiniju(prvi,c.Sledeci);
        //        prvi.Sledeci = c.Sledeci;
        //        c.Sledeci.Prethodni = prvi;
        //        c.Prethodni = null;
        //        c.Sledeci = null;
        //        Niz.RemoveAt(1);
        //    }
        //    return Niz;
        //}

        //public void izracunajPoziciju(Clan c)
        //{
        //    //menjaj vrednosti
        //    if (Niz.Count == 0)
        //    {//start
        //        c.Pozicija.X = 370;
        //        c.Pozicija.Y = 50;
        //    }
        //    else if (Niz.Count == 1)
        //    {//null
        //        c.Pozicija.X = 370;
        //        c.Pozicija.Y = 200;
        //    }
        //    else if (Niz.Count == 2)
        //    {//prvi
        //        c.Pozicija.X = 430;
        //        c.Pozicija.Y = 70;
        //    }
        //    else
        //    {
        //        c.Pozicija.X = c.Prethodni.Pozicija.X + 50;
        //        c.Pozicija.Y = c.Prethodni.Pozicija.Y + 60;
        //    }
        //}

        //public void nacrtajLiniju(Clan c1,Clan c2)
        //{
        //    Linija l = new Linija(c1,c2);
        //    NizLinija.Add(l);
        //}

        //public void izbrisiLiniju(Clan c1, Clan c2)
        //{
        //    Linija l = new Linija(c1, c2);
        //    int index = NizLinija.FindIndex(li => li.Levi.Pozicija.X == c1.Pozicija.X && li.Desni.Pozicija.X == c2.Pozicija.X
        //    && li.Levi.Pozicija.Y == c1.Pozicija.Y && li.Desni.Pozicija.Y == c2.Pozicija.Y);
        //    l.Levi = null;
        //    l.Desni = null;
        //    //int index = NizLinija.IndexOf(l);
        //    //NizLinija.RemoveAt(index);
        //    //bool v = NizLinija.Remove(i => i.Pocetak.X == l.Pocetak.X && i.Pocetak.Y == l.Pocetak.Y && i.Kraj.X == l.Kraj.X && i.Kraj.Y == l.Kraj.Y);
        //    //NizLinija.RemoveAt(index);
        //    NizLinija.RemoveAt(index);
        //}

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
