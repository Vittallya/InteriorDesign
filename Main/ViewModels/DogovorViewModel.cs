using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Main.ViewModels
{
    public class DogovorViewModel
    {
        public bool IsConfirmRequiered { get; set; }

        public DateTime Date { get; set; }
        public string Customer { get; set; }

        public string Address { get; set; }
        public double Area { get; set; }

        public int DogovorNumber { get; set; }

        public bool IsDetail { get; set; }

        public string[] Services { get; set; }

        public bool IsConfirmed { get; private set; }

        public ICommand Confirm => new Command(x =>
        {
            if(x is Window w)
            {
                IsConfirmed = true;
                w.DialogResult = true;
            }
        });

        public DogovorViewModel()
        {

        }
    }
}
