using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public class EmployeeForClientService
    {
        private readonly AllDbContext dbContext;

        public EmployeeForClientService(AllDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            await dbContext.Employees.LoadAsync();
            return dbContext.Employees.Where(e => e.IsShow);
        }

    }
}
