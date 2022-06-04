using projekat_Red_Dek.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels
{
    public class MainVM : BaseViewModel, INotifyPropertyChanged
    {
        private BaseViewModel selectedViewModel = new RedVM();

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UpdateViewCommand UpdateViewCommand { get; set; }
        public MainVM()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
