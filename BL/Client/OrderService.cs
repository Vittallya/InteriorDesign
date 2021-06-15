using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderService
    {
        public Order Order { get; private set; } = new Order();

        private readonly IOrderParamsCalculatorService calculatorService;
        private readonly AllDbContext dbContext;

        public bool IsWrited { get; set; }

        public OrderParams OrderParams { get; private set; }

        public bool HasOrderParams => _service?.NeedDetails ?? false;

        public string ErrorMessage { get; private set; }

        public Service SelectedService => _service;
        public Style SelectedStyle => _style;

        private Service _service;
        private ICollection<int> _services;
        private Style _style;

        private double _serviceCost;
        private int _styleId;

        public void SetupServices(IEnumerable<Service> services)
        {
            _services = services.Select(x => x.Id).ToList();
        }

        public void SetupStyle(Style style)
        {
            _style = style;
            _styleId = style.Id;
        }

        public OrderService(IOrderParamsCalculatorService calculatorService, AllDbContext dbContext)
        {
            this.calculatorService = calculatorService;
            this.dbContext = dbContext;
        }

        public bool SetupOrderParams(OrderParams @params)
        {
            OrderParams = @params;
            return true;
        }



        public async Task<bool> ApplyOrderAndCompleteAsync(int clientId)
        {
            if(_service == null)
            {
                ErrorMessage = "Service is not setted";
                return false;
            }

            if(HasOrderParams && _style == null)
            {
                ErrorMessage = "Style is not setted";
                return false;
            }

            await dbContext.Orders.LoadAsync();
            var order = Order;

            order.CommonCost = GetCommonCost();
            order.ClientId = clientId;

            foreach(var sId in _services)
            {
                var serv = dbContext.Services.FindAsync(sId);
                
            }

            if (order.StartWorkingDate1.HasValue)
            {
                order.StartWorkingDate = order.StartWorkingDate1.Value;
            }

            order.CreationDate = DateTime.Now;

            dbContext.Orders.Add(order);


            if (HasOrderParams)
            {
                await dbContext.OrderDetails.LoadAsync();

                var orderDtl = new OrderDetail
                {
                    Area = OrderParams.Area,
                    FloorsHeight = OrderParams.FloorsHeight,
                    HouseType = OrderParams.HouseType,
                    IsWallAlignment = OrderParams.IsWallAlignment,
                    RoomsCount = OrderParams.Rooms,
                    Order = Order,
                    StyleId = _styleId,
                };

                dbContext.OrderDetails.Add(orderDtl);

            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            return true;
        }

        public double GetCommonCost()
        {
            return !HasOrderParams ? _serviceCost : calculatorService.GetPrices(_serviceCost, OrderParams).CommonCost;
        }

        public OrderPrice GetPrices()
        {
            return calculatorService.GetPrices(_serviceCost, OrderParams);
        }

        public void Clear()
        {
            OrderParams = default(OrderParams);
            IsWrited = false;
            _style = null;
            _service = null;
            Order = new Order();
        }
    }
}
