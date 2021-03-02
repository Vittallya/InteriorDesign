﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main.MVVM_Core
{
    class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }            
        }


        private Action<object> _execute;
        private Func<object, bool> _canExeute;

        public Command(Action<object> execute, Func<object, bool> canExeute = null)
        {
            _execute = execute;
            _canExeute = canExeute;

        }

        public bool CanExecute(object parameter)
        {
            return _canExeute == null || _canExeute(parameter);
        }

        public void Execute(object parameter)
        {            
            _execute(parameter);
        }
    }
}