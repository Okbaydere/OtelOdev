using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class Rooms
    {
        public Rooms()
        {
            Reservations = new HashSet<Reservation>();
        }

        [Key]
        public int RoomId { get; set; }
        
        [Required]
        public int? RoomNo { get; set; }
        
        public bool IsAvaliable { get; set; }
        
        [ForeignKey("RoomType")]
        public int? RoomTypeId { get; set; }

        // Navigation Properties
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
