using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderService
    {
        public Order Order { get; private set; } = new Order();

        public string ErrorMessage { get; private set; }             

        public void SetupService(Service service)
        {
            Order.Service = service;
        }

        public double GetCommonCost()
        {
            return Order.Service.Cost;
        }
    }
}
