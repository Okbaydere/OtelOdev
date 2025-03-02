using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Rooms
    {
        [Key]

        public int RoomId { get; set; }
        public int? RoomNo { get; set; }
        public bool IsAvaliable { get; set; }
        public int ?RoomTypeId { get; set; }
        public int? ReservationID { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual RoomType RoomType { get; set; }

    }
}
