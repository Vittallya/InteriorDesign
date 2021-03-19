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
using Main.Pages;

namespace Main.ViewModels
{
    public class StylesViewModel : BasePageViewModel
    {
        private readonly StylesService service;
        private readonly PageService pageService;
        private readonly OrderService orderService;
        private string searchText;

        public ObservableCollection<Style> Styles { get; set; }

        public Style Selected { get; set; }

        public StylesViewModel(StylesService service, PageService pageService, OrderService designParams) : base(pageService)
        {
            this.service = service;
            this.pageService = pageService;
            this.orderService = designParams;
            Init();
        }

        async void Init()
        {
            Styles = await service.GetStyles();
        }

        public string SearchText
        {
            get => searchText;
            set 
            {
                searchText = value;
                Search(value);
                OnPropertyChanged();
            }
        }

        async void Search(string name)
        {
            if (name != null && name.Length == 0)
                name = null;

            Styles = await service.GetStyles(name);
        }

        public ICommand NextPage => new Command(x =>
        {
            orderService.SetupStyle(Selected);
            pageService.ChangePage<DesignParamsPage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);

        }, y => Selected != null);

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }
}
