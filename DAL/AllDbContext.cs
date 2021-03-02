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


        public DbSet<User> Users { get; set; }
    }
}
