using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels.Commands
{
    public class SaveCommandDek : ICommand
    {
        private DekVM vm;

        public DekVM VM
        {
            get { return vm; }
            set { vm = value; }
        }
        public SaveCommandDek(DekVM vm)
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
            if (VM.NizObjekata.Count < 3)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            VM.saveFunc();
        }
    }
}
