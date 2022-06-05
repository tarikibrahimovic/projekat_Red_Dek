using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels.Commands
{
    public class CreateCommandDek : ICommand
    {
        private DekVM vm;

        public DekVM VM
        {
            get { return vm; }
            set { vm = value; }
        }

        public CreateCommandDek(DekVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            if (string.IsNullOrWhiteSpace(query))
                return false;
            return true;
        }

        public void Execute(object parameter)
        {
            //neka funkcija iz viewmodela koju treba da napisem
            VM.DodajObjekat();
        }        
    }
}
