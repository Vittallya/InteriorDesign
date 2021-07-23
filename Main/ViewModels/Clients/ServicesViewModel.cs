using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BL;
using DAL.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Main.Pages;
using System.Collections;

namespace Main.ViewModels
{
    public class ServicesViewModel : BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly IServicesModelService modelService;
        private readonly DbContextLoader loader;
        private readonly OrderService paramsService;
        private string searchText;

        public ObservableCollection<Service> Services { get; set; }

        public ServicesViewModel(PageService pageService,
                                 IServicesModelService modelService,
                                 DbContextLoader loader,
                                 OrderService paramsService) : base(pageService)
        {
            this.pageService = pageService;
            this.modelService = modelService;
            this.loader = loader;
            this.paramsService = paramsService;
            Init();
        }

        public Service Selected { get; set; }

        public bool IsLoaded { get; set; }

        async void Init()
        {
            while (!loader.IsLoaded)
            {
                await Task.Delay(200);
            }
            IsLoaded = true;

            await modelService.ReloadAsync();
            Services = new ObservableCollection<Service>(modelService.GetServicesAsync());
            //if (editing) -> SelectedService - param.Service
        }


        public string SearchText 
        { 
            get => searchText;
            set 
            { 
                if(value != searchText)
                {
                    searchText = value;
                    SearchByName(value);
                    OnPropertyChanged();
                }
                
            }
        }

        void SearchByName(string name)
        {
            if (name != null && name.Length == 0)
                name = null;

            Services = new ObservableCollection<Service>(
                modelService.GetServicesAsync(name));
        }

        public ICommand Search => new Command(name =>
        {
            if (name != null && name.ToString().Length == 0)
                name = null;

            Services = new ObservableCollection<Service>(
                modelService.GetServicesAsync(name?.ToString()));
        });

        public ICommand Next => new Command(obj =>
        {

            if(obj is IList list)
            {
                var services = list.OfType<Service>();

                if (services.Any(x => x.NeedDetails))
                {
                    paramsService.SetupServices(services);
                    pageService.ChangePage<StylesPage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);
                }
                else
                {
                    paramsService.Clear();
                    paramsService.SetupServices(services);
                    pageService.ChangePage<AddressAndDateTimePage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);
                }
            }


        }, x => Selected != null);

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }

}
