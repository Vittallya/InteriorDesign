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
using System.Windows.Input;

namespace Main.ViewModels
{
    public class EditStylesViewModel : BaseViewModel
    {
        public ObservableCollection<Style> Styles { get; set; }

        public Style SelectedItem { get; set; }

        public ICommand EditStyle => new Command(s =>
        {
            
        }, c => SelectedItem != null);
        public ICommand RemoveStyle => new Command(s =>
        {

        }, c => SelectedItem != null);


        public ICommand AddStyle => new Command(s =>
        {

        });

        public ICommand Update => new Command(s =>
        {

        });

    }
}
