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
