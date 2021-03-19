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
    public class DesignParamsViewModel : BasePageViewModel
    {
        private readonly PageService pageService;
        private readonly OrderService paramsService;

        public OrderParams Order { get; set; }



        public DesignParamsViewModel(PageService pageService, OrderService paramsService) : base(pageService)
        {
            this.pageService = pageService;
            this.paramsService = paramsService;

            HouseTypes = OrderParamsExtensions.HouseTypes;

            if (!paramsService.IsWrited)
            {
                Order = new OrderParams();
                Order.Area = 75.0;
                Order.FloorsHeight = 3.0;
                Order.Rooms = 3;
            }
            else
            {
                Order = paramsService.OrderParams;
            }

            paramsService.IsWrited = true;
        }

        public ICommand Next => new Command(x =>
        {
            Order.HouseType = SelectedHouseType;
            Order.IsWallAlignment = !Convert.ToBoolean(WallAlignmentIndex);

            if (paramsService.SetupOrderParams(Order))
            {
                pageService.ChangePage<AddressAndDateTimePage>(Rules.Pages.SERVICES_POOL, AnimateTo.Left);
            }
            else
            {
                MessageBox.Show(paramsService.ErrorMessage);
            }
        });


        public int WallAlignmentIndex { get; set; }

        public Dictionary<HouseType, string> HouseTypes { get; set; }
        public HouseType SelectedHouseType { get; set; }

        public override int PoolIndex => Rules.Pages.SERVICES_POOL;
    }
}
