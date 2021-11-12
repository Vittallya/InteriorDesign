using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IClientOrders
    {
        Task ReloadAsync(int clientId);
        Task RemoveOrder(Order order);

        IEnumerable<Order> GetOrders();
    }
}
