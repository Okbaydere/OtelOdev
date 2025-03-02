using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtelUI.Models
{
    public class ReservationConfirmationViewModel
    {
        public Reservation Reservation { get; set; }
        public Customer Customer { get; set; }
        public Rooms Room { get; set; }
        public RoomType RoomType { get; set; }
        public List<AdditionalService> AdditionalServices { get; set; }
    }
}
