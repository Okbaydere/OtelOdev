using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Rooms>();
        }

        [Key]
        public int RoomTypeId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }
        
        [Required]
        public decimal? TypePrice { get; set; }
        
        [Required]
        public int? Capacity { get; set; }
        
        [StringLength(1000)]
        public string Features { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [StringLength(255)]
        public string ImageUrl { get; set; }
        
        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
