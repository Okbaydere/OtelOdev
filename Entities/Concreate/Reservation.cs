using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Reservation
    {
        public Reservation()
        {
            AdditionalServices = new HashSet<AdditionalService>();
        }

        [Key]
        public int ReservationID { get; set; }

        [Required]
        public DateTime EnterDate { get; set; }

        [Required]
        public DateTime ExitDate { get; set; }

        public int? Fee { get; set; }
        public int? PersonNumber { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerID { get; set; }

        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        
        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual ICollection<AdditionalService> AdditionalServices { get; set; }
    }
}
