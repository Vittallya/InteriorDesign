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
    public class ClientOrders : IClientOrders
    {
        private readonly AllDbContext dbContext;
        private readonly Client _client;

        public ClientOrders(ICurrentUserService currentUser, DAL.AllDbContext dbContext)
        {
            _client = currentUser.CurrentUser as Client;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            if(_client != null)
            {
                await dbContext.Orders.Include(x => x.Service).LoadAsync();
                await dbContext.OrderDetails.Include(x => x.Style).LoadAsync();

                var orders = dbContext.Orders.Where(x => x.ClientId == _client.Id).ToList();

                return orders;
            }
            return null;
        }

        public async Task RemoveOrder(Order order)
        {
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
