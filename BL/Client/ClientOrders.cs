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

        public async Task ReloadAsync(int clId)
        {
            using(var context = new AllDbContext())
            {
                await context.Orders.Where(x => x.ClientId.Equals(clId)).Include(x => x.Services).Include(x => x.OrderDetail).LoadAsync();

                _orders = context.Orders.Local.ToList();

                foreach (var ord in _orders.Where(x => x.Services.Any(y => y.NeedDetails)))
                {
                    await context.Entry(ord.OrderDetail).Reference(x => x.Style).LoadAsync();
                }

            }

        }

        public IEnumerable<Order> GetOrders()
        {
            return _orders;
        }

        public async Task RemoveOrder(Order order)
        {
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
