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
    public class StylesViewModel: BasePageViewModel
    {
        private readonly StylesService service;

        public ObservableCollection<Style> Styles { get; set; }

        public Style Selected { get; set; }

        public StylesViewModel(StylesService service, PageService pageService):base(pageService)
        {
            this.service = service;
            Init();
        }

        async void Init()
        {
            Styles = await service.GetStyles();
        }


        public ICommand NextPage => new Command(x =>
        {


        }, y => Selected != null);
    }
}
