using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Client: IUser
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
