﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekat_Red_Dek.ViewModels.Commands
{
    public class DelateCommand : ICommand
    {
        private RedVM vm;

        public RedVM VM
        {
            get { return vm; }
            set { vm = value; }
        }
        public DelateCommand(RedVM vm)
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
            if(VM.NizObjekata.Count < 3)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            //neka funkcija iz viewmodela koju treba da napisem
            VM.obrisiClan();
        }
    }
}
