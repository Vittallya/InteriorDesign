using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public class OrderDetailsService
    {
        private readonly OrderService orderService;

        public Style Style { get; set; }

        public OrderDetailsService(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public bool IsBeginned { get; set; }
        public OrderDetail OrderD { get; private set; }

        public bool IsSetted => OrderD != null;

        public string ErrorMessage { get; set; }

        public bool Setup(OrderDetail order)
        {
            if (order.RoomsCount <= 0)
            {
                ErrorMessage = "Количество комнат должно быть положительным числом";
                return false;
            }
            if (order.FloorsHeight <= 0)
            {
                ErrorMessage = "Высота потолоков должна быть положительным числом";
                return false;
            }
            if (order.Area <= 0)
            {
                ErrorMessage = "Площадь должна быть положительным числом";
                return false;
            }
            
            OrderD = order;
            OrderD.Style = Style;
            return true;
        }

        public double GetCommonCost()
        {
            if (IsSetted)
            {
                double serviceCost = orderService.Order.Service.Cost;

                AllAreaCost = GetAreaCost(serviceCost, OrderD.Area);
                WallAlignmentCost = GetWallAlignmentCost(serviceCost, OrderD.Area, OrderD.IsWallAlignment);
                RoomsCost = GetRoomsCost(serviceCost, OrderD.Area, OrderD.RoomsCount);
                FloorsCost = GetFloorsCost(serviceCost, OrderD.Area, OrderD.FloorsHeight);

                double cost = AllAreaCost + WallAlignmentCost + RoomsCost + FloorsCost;

                cost += HouseTypeCost = GetHouseTypeCost(cost, OrderD.HouseType);

                return cost;

            }
            return 0;
        }

        public void Clear()
        {
            WallAlignmentCost = AllAreaCost = FloorsCost = RoomsCost = HouseTypeCost = 0;
            OrderD = null;
            IsBeginned = false;
        }

        public double WallAlignmentCost { get; set; }
        public double AllAreaCost { get; set; }
        public double FloorsCost { get; set; }
        public double RoomsCost { get; set; }
        public double HouseTypeCost{ get; set; }

        public double GetWallAlignmentCost(double roomsCount, double floorsHeight, bool isSet) 
            => isSet ? ((OrderD.RoomsCount * 2 + OrderD.FloorsHeight - 3) * 5000) : 0;

        public double GetAreaCost(double serviceCost, double area) => serviceCost * area;
        public double GetFloorsCost(double serviceCost, double area, double floorsH) => (floorsH - 3) * (serviceCost / 25) * area;
        public double GetRoomsCost(double serviceCost, double area, int rooms) => (rooms - 2) * (serviceCost / 30) * area;
        public double GetHouseTypeCost(double commonCost, HouseType type) => type != HouseType.Apartment ? commonCost * 0.112 : 0;
    }
}
