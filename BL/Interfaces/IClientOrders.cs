﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IClientOrders
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task RemoveOrder(Order order);
    }
}
