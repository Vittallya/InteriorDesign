using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL;
using DAL.Models;
using Main.MVVM_Core;
using System.Collections.ObjectModel;

namespace Main.ViewModels
{
    public class StylesViewModel: BaseViewModel
    {
        public ObservableCollection<Style> Styles { get; set; }



    }
}
