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

        public DesignParamsService ParamsService { get; set; }

        public string HouseType { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }

        public OrderConfirmViewModel(PageService pageService, DesignParamsService paramsService) : base(pageService)
        {
            this.pageService = pageService;
            ParamsService = paramsService;

            HouseTypes = new Dictionary<HouseType, string>
            {
                {DAL.Models.HouseType.Apartment, "Квартира" },
                {DAL.Models.HouseType.House, "Дом" },
                {DAL.Models.HouseType.Office, "Офис" },
            };

            HouseType = HouseTypes[paramsService.Order.HouseType];
            Cost = paramsService.GetCommonCost();
        }

        public double Cost { get; set; }

        public string IsWallAlignment => ParamsService.Order.IsWallAlignment ? "Да" : "Нет";

        public ICommand Next => new Command(x =>
        {
            pageService.ChangePage<OrderConfirmPage>(AnimateTo.Left);
        });
    }
}
