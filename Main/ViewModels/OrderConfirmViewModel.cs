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
        public OrderDetailsService paramsService { get; }

        public bool IsDetailsOrder { get; set; }

        public OrderService orderService { get; }
        public string HouseType { get; set; }

        public OrderDetail Order { get; set; }

        public Service Service { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }

        public OrderConfirmViewModel(PageService pageService, OrderService paramsService, OrderDetailsService detailsService) : base(pageService)
        {
            this.pageService = pageService;
            this.orderService = paramsService;
            this.paramsService = detailsService;
            Service = paramsService.Order.Service;

            IsDetailsOrder = detailsService.IsSetted;


            if (detailsService.IsSetted)
            {
                Order = detailsService.OrderD;



                
                HouseTypes = new Dictionary<HouseType, string>
                {
                    {DAL.Models.HouseType.Apartment, "Квартира" },
                    {DAL.Models.HouseType.House, "Дом" },
                    {DAL.Models.HouseType.Office, "Офис" },
                };
                IsWallAlignment = detailsService.OrderD.IsWallAlignment ? "Да" : "Нет";

                HouseType = HouseTypes[detailsService.OrderD.HouseType];
                Cost = detailsService.GetCommonCost();
                UnitName = "\\" + Service.CostUnitName;
            }
            else
            {
                Cost = paramsService.GetCommonCost();
            }
        }

        public string UnitName { get; set; }

        public double Cost { get; set; }

        public string IsWallAlignment { get; set; }

        public ICommand Next => new Command(x =>
        {
            pageService.ChangePage<OrderConfirmPage>(AnimateTo.Left);
        });
    }
}
