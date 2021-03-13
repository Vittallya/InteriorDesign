using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Special { get; set; }
        public bool IsShow { get; set; }
        public DateTime StartWoriking { get; set; }
        public double Salary { get; set; }
        public WorkingStatus WorkingStatus { get; set; }

        public string ImagePath { get; set; }

        public EmployeeAdmin EmployeeAdmin { get; set; }
    }
}
