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
    public class ServicesModelSerivce : IServicesModelService
    {
        private readonly AllDbContext alldbcontext;

        public ServicesModelSerivce(AllDbContext alldbcontext)
        {
            this.alldbcontext = alldbcontext;
        }

        private IEnumerable<Service> _services;
        public async Task ReloadAsync()
        {
            await alldbcontext.Services.LoadAsync();
            _services = await alldbcontext.Services.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Service> GetServicesAsync(string name = null)
        {
            return name != null ? _services.Where(x => x.Name.Contains(name)) : _services;
        }
    }
}
