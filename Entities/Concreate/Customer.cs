using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Customer
    {
        [Key]

        public int CustomerID { get; set; }
        
        public string CustomerName { get; set; }

        public string CustomerSurName { get; set; }

        public string CustomerTc { get; set; }
        
        public string CustomerTel { get; set; }
        
        public string CustomermMail { get; set; }
        public int? ReservationID { get; set; }
        public virtual Reservation Reservation  { get; set; }
        
    }
}
