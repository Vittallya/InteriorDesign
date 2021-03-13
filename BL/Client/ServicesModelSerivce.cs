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

        public async Task<IEnumerable<Service>> GetServicesAsync(string name = null)
        {
            await alldbcontext.Services.LoadAsync();
            return name != null ? alldbcontext.Services.Where(x => x.Name == name) : alldbcontext.Services;
        }
    }
}
