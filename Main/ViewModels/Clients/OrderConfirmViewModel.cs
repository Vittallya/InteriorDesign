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
using System.Collections.ObjectModel;
using System.Windows;

namespace Main.ViewModels
{
    public class OrderConfirmViewModel: BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly IUserRegisterService registerService;
        private readonly ICurrentUserService userService;
        private readonly EventBus eventBus;

        public OrderParams Params { get; }
        public OrderPrice Prices { get; private set; }

        public bool IsDetailsOrder { get; set; }

        public OrderService orderService { get; }
        public string HouseType { get; set; }
        public OrderDetail Order { get; set; }
        public string ServiceName { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }

        public DAL.Models.Style Style { get; set; }

        public ObservableCollection<Service> Services { get; set; }

        public OrderConfirmViewModel(PageService pageService,
                                     IUserRegisterService registerService,
                                     OrderService paramsService,
                                     ICurrentUserService userService,
                                     EventBus eventBus) : base(pageService)
        {
            this.pageService = pageService;
            this.registerService = registerService;
            this.orderService = paramsService;
            this.Params = paramsService.OrderParams;
            this.userService = userService;
            this.eventBus = eventBus;
            Init();
        }
        void Init()
        {

            Services = new ObservableCollection<Service>(orderService.SelectedServices);

            IsDetailsOrder = orderService.HasOrderParams;


            if (IsDetailsOrder)
            {
                Prices = orderService.GetPrices();
                HouseTypePercent = Prices.HouseTypeCost * 100;
                Style = orderService.SelectedStyle;
                IsWallAlignment = Params.IsWallAlignment ? "Да" : "Нет";
                HouseType = OrderParamsExtensions.HouseTypes[Params.HouseType];



                UnitName = "\\" + Services.First(x => x.NeedDetails).CostUnitName;
            }
            Cost = orderService.GetCommonCost();
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

            var w = new Windwos.DogovorWindow();

            var dc = new DogovorViewModel
            {
                IsConfirmRequiered = true,
                Address = orderService.Order.Address,
                Customer = @event.User.Name,
                Area = orderService.OrderParams?.Area ?? 0,
            };

            w.DataContext = dc;

            w.ShowDialog();


            if (dc.IsConfirmed)
            {
                MessageBox.Show("Заказ оформлен успешно", "", MessageBoxButton.OK, MessageBoxImage.Information);

                await orderService.ApplyOrderAndCompleteAsync(@event.User.Id);
                await eventBus.Publish(new MVVM_Core.Events.OrderCompleted(ServiceName));
                pageService.ClearHistoryByPool(Rules.Pages.SERVICES_POOL);
                orderService.Clear();
            }
            else
            {
                MessageBox.Show("Оформление заказа прервано. Для оформления заказа необходимо принять условия договора. Чтобы возобновить заказ, перейдите во вкладку \"Услуги\"", "", MessageBoxButton.OK, MessageBoxImage.Information);
                await eventBus.Publish(new MVVM_Core.Events.OrderCompleted(ServiceName, 2));
            }
        }

    }
}
