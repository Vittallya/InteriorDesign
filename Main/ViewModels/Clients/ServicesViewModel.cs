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

namespace Main.ViewModels
{
    public class ServicesViewModel: BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly IServicesModelService modelService;
        private readonly OrderService paramsService;

        public ObservableCollection<Service> Services { get; set; }

        public ServicesViewModel(PageService pageService, IServicesModelService modelService, OrderService paramsService):base(pageService)
        {
            this.pageService = pageService;
            this.modelService = modelService;
            this.paramsService = paramsService;
            Init();
        }

        public Service Selected { get; set; }

        async void Init()
        {
            Services = new ObservableCollection<Service>(await modelService.GetServicesAsync());
        }

        

        public ICommand Search => new CommandAsync(async name =>
        {
            if (name != null && name.ToString().Length == 0)
                name = null;

            Services = new ObservableCollection<Service>(
                await modelService.GetServicesAsync(name?.ToString()));
        });

        public ICommand Next => new Command(name =>
        {
            paramsService.SetupService(Selected);

            if (Selected.NeedDetails)
            {
                pageService.ChangePage<StylesPage>(AnimateTo.Left, Rules.Pages.SERVICES_POOL);
            }
            else
            {
                paramsService.Clear();
                pageService.ChangePage<AddressAndDateTimePage>(AnimateTo.Left, Rules.Pages.SERVICES_POOL);
            }

        }, x => Selected != null);

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }

}
