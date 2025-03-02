using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class AdditionalService
    {
        [Key]
        public int AdditionalServiceID { get; set; }
 
        public string ServiceName { get; set; }
        public decimal? ServicePrice { get; set; }
        public string ServiceDescription { get; set; }
        public string ImageUrl { get; set; }
        
        // Foreign key
        public int? ReservationID { get; set; }
        
        // Navigation property
        public virtual Reservation Reservation { get; set; }
    }
}
