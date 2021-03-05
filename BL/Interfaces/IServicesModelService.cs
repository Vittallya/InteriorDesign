using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public interface IServicesModelService
    {
        Task<IEnumerable<Service>> GetServicesAsync(string name = null);
    }
}
