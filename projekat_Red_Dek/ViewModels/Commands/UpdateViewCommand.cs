using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainVM VM;
        public event EventHandler CanExecuteChanged;

        public UpdateViewCommand(MainVM mainVM)
        {
            VM = mainVM;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Red")
            {
                VM.SelectedViewModel = new RedVM();
            }
            else if(parameter.ToString() == "Dek")
            {
                VM.SelectedViewModel = new DekVM();
            }
        }
    }
}
