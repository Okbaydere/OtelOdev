using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concreate
{
    public class RoomType
    {
        [Key]

        public int RoomTypeId { get; set; }
        public string TypeName { get; set; }
        public decimal? TypePrice { get; set; }
        public int? Capacity { get; set; }
        public string Features { get; set; }
        public ICollection<Rooms> Rooms { get  ; set; }
    }
}
