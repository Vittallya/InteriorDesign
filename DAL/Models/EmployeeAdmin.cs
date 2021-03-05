using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models
{
    public class EmployeeAdmin: IUser
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public byte[] AccessCode { get; set; }

        public virtual Employee Employee { get; set; }

        [NotMapped]
        public string Name => Employee?.Name;
    }
}
