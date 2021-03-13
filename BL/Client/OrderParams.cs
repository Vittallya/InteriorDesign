using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    //public class OrderDetailsService
    //{
    //    private readonly OrderService orderService;

    //    private readonly AllDbContext dbContext;

        
    //    public Style Style { get; set; }

    //    public OrderDetailsService(OrderService orderService, AllDbContext dbContext)
    //    {
    //        this.orderService = orderService;
    //        this.dbContext = dbContext;
    //    }

    //    public async Task<bool> ApplyOrder(int clientId)
    //    {
    //        await dbContext.Orders.LoadAsync();
    //        var order = orderService.Order;

    //        order.ClientId = clientId;
    //        order.ServiceId = order.Service.Id;

    //        if (order.StartWorkingDate1.HasValue)
    //        {
    //            order.StartWorkingDate = order.StartWorkingDate1.Value;
    //        }
    //        order.CreationDate = DateTime.Now;
    //        order.Service = null;
    //        dbContext.Orders.Add(order);


    //        if (OrderD != null)
    //        {
    //            await dbContext.OrderDetails.LoadAsync();
    //            order.OrderDetail = OrderD;
    //            OrderD.Order = order;
    //            dbContext.OrderDetails.Add(OrderD);
    //            order.CommonCost = GetCommonCost();
    //        }

    //        try
    //        {
    //            await dbContext.SaveChangesAsync();
    //        }
    //        catch (Exception ex)
    //        {
    //            ErrorMessage = ex.Message;
    //            return false;
    //        }
    //        return true;
    //    }


    //    public bool IsBeginned { get; set; }
    //    public OrderDetail OrderD { get; private set; }

    //    public bool IsSetted => OrderD != null;

    //    public string ErrorMessage { get; set; }

    //    public bool Setup(OrderDetail order)
    //    {
    //        if (order.RoomsCount <= 0)
    //        {
    //            ErrorMessage = "Количество комнат должно быть положительным числом";
    //            return false;
    //        }
    //        if (order.FloorsHeight <= 0)
    //        {
    //            ErrorMessage = "Высота потолоков должна быть положительным числом";
    //            return false;
    //        }
    //        if (order.Area <= 0)
    //        {
    //            ErrorMessage = "Площадь должна быть положительным числом";
    //            return false;
    //        }
            
    //        OrderD = order;
    //        OrderD.Style = Style;
    //        return true;
    //    }

    //    public double GetCommonCost()
    //    {
    //        if (IsSetted)
    //        {
    //            double serviceCost = orderService.Order.Service.Cost;

    //            AllAreaCost = GetAreaCost(serviceCost, OrderD.Area);
    //            WallAlignmentCost = GetWallAlignmentCost(serviceCost, OrderD.Area, OrderD.IsWallAlignment);
    //            RoomsCost = GetRoomsCost(serviceCost, OrderD.Area, OrderD.RoomsCount);
    //            FloorsCost = GetFloorsCost(serviceCost, OrderD.Area, OrderD.FloorsHeight);

    //            double cost = AllAreaCost + WallAlignmentCost + RoomsCost + FloorsCost;

    //            cost += HouseTypeCost = GetHouseTypeCost(cost, OrderD.HouseType);

    //            return cost;

    //        }
    //        return 0;
    //    }


    //}

    public class OrderParams
    {
        public bool IsWallAlignment { get; set; }

        public double Area { get; set; }

        public double FloorsHeight { get; set; }

        public int Rooms { get; set; }

        public HouseType HouseType { get; set; }
        
    }

    public struct OrderPrice
    {
        public double WallAlignmentCost { get; set; }

        public double AreaCost { get; set; }
        public double FloorsCost { get; set; }
        public double RoomsCost { get; set; }

        public double CommonCost { get; set; }

        public double HouseTypeCost { get; set; }
    }

    public static class OrderParamsExtensions
    {
        public static Dictionary<HouseType, string> HouseTypes { get; } = new Dictionary<HouseType, string>
        {
            {DAL.Models.HouseType.Apartment, "Квартира" },
            {DAL.Models.HouseType.House, "Дом" },
            {DAL.Models.HouseType.Office, "Офис" },
        };
    }
}
