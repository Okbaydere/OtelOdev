using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int ?Fee { get; set; }
        public int? PersonNumber  { get; set; }
        public int ?CustomerID { get; set; }
        public int? RoomId { get; set; }
        public ICollection<Customer> customers { get; set; }
        public ICollection<Rooms> rooms { get; set; }
        public ICollection< AdditionalService> AdditionalServices { get; set; }

    }
}
