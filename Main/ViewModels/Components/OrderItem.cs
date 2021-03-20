using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace Main.ViewModels.Components
{
    public class OrderItem
    {
        public OrderItem(Order order)
        {
            Order = order;
        }

        public bool HasDetails => Order.OrderDetail != null;

        public bool MayBeDeleted => (DateTime.Now.Day - Order.CreationDate.Day) < 2;

        public Order Order { get; }
    }
}
