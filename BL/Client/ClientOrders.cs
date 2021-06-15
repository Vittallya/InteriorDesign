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

        private IEnumerable<Order> _orders;

        public async Task ReloadAsync()
        {
            await dbContext.Orders.Include(x => x.Services).LoadAsync();
            await dbContext.OrderDetails.Include(x => x.Style).LoadAsync();

            _orders = await dbContext.Orders.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Order> GetOrders(int clientId)
        {
            return _orders.Where(x => x.ClientId == clientId);
        }

        public async Task RemoveOrder(Order order)
        {
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
