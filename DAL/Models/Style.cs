using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Models
{
    public class Style
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

    }
}
