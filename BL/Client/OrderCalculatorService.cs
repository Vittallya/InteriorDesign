using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderCalculatorServiceUsual : IOrderParamsCalculatorService
    {
        public OrderPrice GetPrices(double serviceCost, OrderParams od)
        {
            OrderPrice price = new OrderPrice();

            price.AreaCost = serviceCost * od.Area;
            
            if(od.FloorsHeight > 3)
                price.FloorsCost = (od.FloorsHeight - 3) * (serviceCost / 25) * od.Area;
            if(od.Rooms > 2)
                price.RoomsCost = (od.Rooms - 2) * (serviceCost / 30) * od.Area;
            price.HouseTypeCost = od.HouseType == DAL.Models.HouseType.Apartment ? 0 : 0.02;
            price.WallAlignmentCost = od.IsWallAlignment ? 
                ((od.Rooms * 2 + od.FloorsHeight - 3) * 5000) : 0;

            price.CommonCost = (price.AreaCost + price.FloorsCost + price.RoomsCost + price.WallAlignmentCost) * (1 + price.HouseTypeCost);

            return price;
        }
    }    
}
