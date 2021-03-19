using System;
using BL;
using System.Windows.Input;
using Main.MVVM_Core;
using Main.Pages;

namespace Main.ViewModels
{
    public class OtherParamsViewModel: BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly OrderService paramsService;

        public DateTime DateStart { get; set; }
        public DateTime? DateSelected { get; set; }

        public string Address { get; set; }

        public OtherParamsViewModel(PageService pageService, OrderService paramsService) : base(pageService)
        {
            
            DateStart = DateTime.Now.AddDays(5);
            Address = paramsService.Order.Address ?? "г. , ул. , д. ";
            DateSelected = paramsService.Order.StartWorkingDate1;

            this.pageService = pageService;
            this.paramsService = paramsService;
        }


        public ICommand Next => new Command(x =>
        {
            paramsService.Order.StartWorkingDate1 = DateSelected.Value;
            paramsService.Order.Address = Address;
            pageService.ChangePage<OrderConfirmPage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);

        }, x => Address != null && Address.Length > 0 && DateSelected.HasValue);

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }
}
