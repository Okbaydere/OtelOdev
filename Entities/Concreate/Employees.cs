using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeFirstName { get; set; }   
        public string EmployeeLastName { get; set; }
 
     
        public string EmployeeNumber { get; set; }
       
        public string EmployeeMail { get; set; }
        
        public string EmployeeTask { get; set; }
       

        public string EmployeeUserName { get; set; }
       

        public string EmployeePassword { get; set; }

        public decimal Salary { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
