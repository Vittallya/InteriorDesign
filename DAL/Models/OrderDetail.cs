using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Models
{
    public class OrderDetail
    {
        [Key]
        [ForeignKey("Order")]
        public int Id { get; set; }
        public int? StyleId { get; set; }
        public Style Style { get; set; }
        public double Area { get; set; }
        public double FloorsHeight { get; set; }
        public HouseType HouseType { get; set; }
        public int RoomsCount { get; set; }
        public bool IsWallAlignment { get; set; }
        public Service Service { get; set; }

        public Order Order { get; set; }
    }
}
