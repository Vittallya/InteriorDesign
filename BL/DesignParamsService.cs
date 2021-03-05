using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DesignParamsService
    {
        
        public bool Beginned { get; set; }

        public OrderDetail Order { get; private set; } = new OrderDetail();

        public void Clear()
        {
            Order = new OrderDetail();
        }

        public double WallAlignmentCost => (Order.RoomsCount * 2 + Order.FloorsHeight - 3) * 5000;                

        public double GetCommonCost()
        {
            double cost = 0;

            if (Order.Service.CostUnitName == "кв. м") 
            {
                cost = Order.Service.Cost * Order.Area;
            }

            if (Order.IsWallAlignment)
            {
                cost += WallAlignmentCost;
            }

            cost += (Order.FloorsHeight - 3) * (Order.Service.Cost / 25) * Order.Area;

            cost += (Order.RoomsCount - 2) * (Order.Service.Cost / 30) * Order.Area;

            if(Order.HouseType != HouseType.Apartment)
            {
                cost *= 1.02;
            }

            return cost;
        }
    }
}
