using System;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class AllDbContext: DbContext
    {        


        public AllDbContext():base("InteriorDesign")
        {
            
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAdmin> EmployeeAdmins { get; set; }
    }
}
