using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DAL.Models;
using System.Windows.Input;
using Main.Pages;

namespace Main.ViewModels
{
    public class OrderConfirmViewModel: BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly IUserRegisterService registerService;
        private readonly ICurrentUserService userService;
        private readonly EventBus eventBus;

        public OrderParams Params { get; }
        public OrderPrice Prices { get; }

        public bool IsDetailsOrder { get; set; }

        public OrderService orderService { get; }
        public string HouseType { get; set; }
        public OrderDetail Order { get; set; }
        public string ServiceName { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }

        public Style Style { get; set; }

        public OrderConfirmViewModel(PageService pageService, IUserRegisterService registerService,  OrderService paramsService, 
            ICurrentUserService userService, EventBus eventBus) : base(pageService)
        {
            this.pageService = pageService;
            this.registerService = registerService;
            this.orderService = paramsService;
            this.Params = paramsService.OrderParams;
            this.userService = userService;
            this.eventBus = eventBus;
            ServiceName = paramsService.SelectedService.Name;
            ServiceCost = paramsService.SelectedService.Cost;
            IsDetailsOrder = paramsService.HasOrderParams;


            if (IsDetailsOrder)
            {
                Prices = paramsService.GetPrices();
                HouseTypePercent = Prices.HouseTypeCost * 100;
                Style = paramsService.SelectedStyle;
                IsWallAlignment = Params.IsWallAlignment ? "Да" : "Нет";
                HouseType = OrderParamsExtensions.HouseTypes[Params.HouseType];
                UnitName = "\\" + paramsService.SelectedService.CostUnitName;
            }
            Cost = paramsService.GetCommonCost();
        }

        public double HouseTypePercent { get; set; }

        public string UnitName { get; set; }

        public double Cost { get; set; }

        public double ServiceCost { get; set; }

        public string IsWallAlignment { get; set; }

        public ICommand Next => new CommandAsync(async x =>
        {

            if (userService.IsAutorized)
            {
                await CompleteOrderPorcess(new MVVM_Core.Events.ClientRegistered(userService.CurrentUser));
                pageService.ChangePage<ClientHomePage>(AnimateTo.Rigth);
            }
            else
            {
                pageService.ChangePage<ClientRegisterPage>(Rules.Pages.CLIENT_REGISTRATION_POOL, AnimateTo.Left);
                eventBus.Subscribe<MVVM_Core.Events.ClientRegistered, OrderConfirmViewModel>(CompleteOrderPorcess);                
            }
        });

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;

        private async Task CompleteOrderPorcess(MVVM_Core.Events.ClientRegistered @event)
        {
            pageService.ClearHistoryByPool(Rules.Pages.SERVICES_POOL);
            await orderService.ApplyOrderAndCompleteAsync(@event.User.Id);            
            await eventBus.Publish(new MVVM_Core.Events.OrderCompleted(ServiceName));
            orderService.Clear();
        }

        private async void UserService_Registered()
        {
            await orderService.ApplyOrderAndCompleteAsync(userService.CurrentUser.Id);
        }
    }
}
