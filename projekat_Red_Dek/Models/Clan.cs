using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekat_Red_Dek.Models
{
    public class Clan : INotifyPropertyChanged
    {
        private Position pozicija;
        private string vrednost;
        private Clan prethodni;
        private Clan sledeci;
        public static int clanid = 0;
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value;
                OnProperyChanged("ID");
            }
        }



        public Position Pozicija
        {
            get { return pozicija; }
            set { pozicija = value;
                OnProperyChanged("Pozicija");
            }
        }


        public string Vrednost
        {
            get { return vrednost; }
            set { vrednost = value;
                OnProperyChanged("Vrednost");
            }
        }

        //mozda i ne trebaju oba

        public Clan Prethodni
        {
            get { return prethodni; }
            set { prethodni = value;
                OnProperyChanged("Prethodni");
            }
        }


        public Clan Sledeci
        {
            get { return sledeci; }
            set { sledeci = value;
                OnProperyChanged("Sledeci");
            }
        }
        public Clan()
        {
            Pozicija = new Position();
            Vrednost = "";
            Prethodni = null;
            Sledeci = null;
            ID = clanid++;
        }

        public Clan(Position p, string v, Clan pr, Clan sl)
        {
            Pozicija = p;
            Vrednost = v;
            Prethodni = pr;
            Sledeci = sl;
            ID = clanid++;
        }

        private void OnProperyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
