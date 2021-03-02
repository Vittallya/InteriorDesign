using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
