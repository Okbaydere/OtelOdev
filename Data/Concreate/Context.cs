using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concreate
{
    public class Context:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AdditionalService> AdditionalService { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }

    }
}
