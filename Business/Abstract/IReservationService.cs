using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReservationService
    {
        List<Reservation> Reservationliste();
        void ReservationInsert(Reservation r);
        void ReservationDelete(Reservation r);
        void ReservationUpdate(Reservation r);
        Reservation GetById(int id);
    }
}
