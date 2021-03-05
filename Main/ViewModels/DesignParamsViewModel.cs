using Main.MVVM_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using DAL;
using DAL.Models;
using System.Windows.Input;
using Main.Pages;

namespace Main.ViewModels
{
    public class DesignParamsViewModel: BasePageViewModel
    {
        private readonly PageService pageService;

        public DesignParamsViewModel(PageService pageService, DesignParamsService paramsService) : base(pageService)
        {
            this.pageService = pageService;
            ParamsService = paramsService;
            HouseTypes = new Dictionary<HouseType, string>
            {
                {HouseType.Apartment, "Квартира" },
                {HouseType.House, "Дом" },
                {HouseType.Office, "Офис" },
            };

            if (!paramsService.Beginned)
            {
                paramsService.Order.RoomsCount = 3;
                paramsService.Order.FloorsHeight = 3.5;
                paramsService.Order.Area = 75;
            }
            paramsService.Beginned = true;
        }

        public ICommand Next => new Command(x =>
        {
            ParamsService.Order.HouseType = SelectedHouseType;
            ParamsService.Order.IsWallAlignment = !Convert.ToBoolean(WallAlignmentIndex);
            pageService.ChangePage<OrderConfirmPage>(AnimateTo.Left);
        });

        public DesignParamsService ParamsService { get; set; }

        public int WallAlignmentIndex { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }
        public HouseType SelectedHouseType { get; set; }
    }
}
