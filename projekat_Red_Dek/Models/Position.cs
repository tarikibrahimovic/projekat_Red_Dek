using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekat_Red_Dek.Models
{
    public class Position : INotifyPropertyChanged
    {
        private double x;

        public double X
        {
            get { return x; }
            set { x = value;
                OnPropertyChanged("X");
            }
        }


        private double y;


        public double Y
        {
            get { return y; }
            set { y = value;
                OnPropertyChanged("Y");
            }
        }

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
