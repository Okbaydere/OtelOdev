using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Customer
    {
        public Customer()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerSurName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string CustomerTc { get; set; }

        [Required]
        [StringLength(15)]
        public string CustomerTel { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string CustomermMail { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
