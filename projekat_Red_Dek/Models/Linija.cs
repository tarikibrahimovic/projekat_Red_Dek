using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekat_Red_Dek.Models
{
    public class Linija : INotifyPropertyChanged
    {
        private Position pocetak;
        private Position kraj;
        private Clan levi;
        private Clan desni;

        public Clan Desni
        {
            get { return desni; }
            set { desni = value; }
        }

        public Clan Levi
        {
            get { return levi ; }
            set { levi = value; }
        }


        public Position Pocetak
        {
            get { return pocetak; }
            set { pocetak = value;
                OnPropertyChanged("Pocetak");
            }
        }


        public Position Kraj
        {
            get { return kraj; }
            set { kraj = value;
                OnPropertyChanged("Kraj");
            }
        }

        public Linija()
        {
            Pocetak = new Position(0, 0);
            Kraj = new Position(0, 0);
            Levi = null;
            Desni = null;
        }

        public Linija(Clan c1, Clan c2)
        {
            Pocetak = new Position(c1.Pozicija.X + 15, c1.Pozicija.Y + 15);
            Kraj = new Position(c2.Pozicija.X + 15, c2.Pozicija.Y + 15);
            Levi = c1;
            Desni = c2;
        }

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
