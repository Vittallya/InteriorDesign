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
using System.ComponentModel;
using System.Windows.Data;

namespace Main.ViewModels
{
    public class StylesViewModel : BasePageViewModel
    {
        private readonly StylesService service;
        private readonly PageService pageService;
        private readonly OrderService orderService;
        private string searchText;

        public ICollectionView StylesView { get; private set; }

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
            StylesView = CollectionViewSource.GetDefaultView(await service.GetStyles());
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

        void Search(string name)
        {
            if (name != null && name.Length == 0)
            {
                StylesView.Filter = null;                
            }
            else
            {
                name = name.ToLower();
                StylesView.Filter = st => (st as Style).Name.ToLower().StartsWith(name);
            }
        }

        public ICommand NextPage => new Command(x =>
        {
            orderService.SetupStyle(Selected);
            pageService.ChangePage<DesignParamsPage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);

        }, y => Selected != null);

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }
}
