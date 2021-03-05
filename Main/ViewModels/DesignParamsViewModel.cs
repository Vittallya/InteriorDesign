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
using System.Windows;

namespace Main.ViewModels
{
    public class DesignParamsViewModel: BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly OrderDetailsService paramsService;

        public OrderDetail Order { get; set; }

        public DesignParamsViewModel(PageService pageService, OrderDetailsService paramsService) : base(pageService)
        {
            this.pageService = pageService;
            this.paramsService = paramsService;

            HouseTypes = new Dictionary<HouseType, string>
            {
                {HouseType.Apartment, "Квартира" },
                {HouseType.House, "Дом" },
                {HouseType.Office, "Офис" },
            };

            if (!paramsService.IsBeginned)
            {
                Order = new OrderDetail();
                Order.RoomsCount = 3;
                Order.FloorsHeight = 4;
                Order.Area = 75;
            }
            paramsService.IsBeginned = true;
        }

        public ICommand Next => new Command(x =>
        {
            Order.HouseType = SelectedHouseType;
            Order.IsWallAlignment = !Convert.ToBoolean(WallAlignmentIndex);

            if (paramsService.Setup(Order))
            {
                pageService.ChangePage<OrderConfirmPage>(AnimateTo.Left);
            }
            else
            {
                MessageBox.Show(paramsService.ErrorMessage);
            }
        });


        public int WallAlignmentIndex { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }
        public HouseType SelectedHouseType { get; set; }
    }
}
