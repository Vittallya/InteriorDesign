using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; } 
        public DateTime CreationDate { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartWorkingDate { get; set; }

        [NotMapped]
        public DateTime? StartWorkingDate1 { get; set; }

        public double CommonCost { get; set; }

        public Client Client { get; set; }
        public Service Service { get; set; }

        public OrderDetail OrderDetail { get; set; }

        public string Address { get; set; }
    }
}
