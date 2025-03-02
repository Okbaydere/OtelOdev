using Business.Abstract;
using Data.Abstract;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class ReservationManager : IReservationService
    {
        IReservationDal _reservationDal;
        public ReservationManager(IReservationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        public Reservation GetById(int id)
        {
            return _reservationDal.GetWithIncludes(
                x => x.ReservationID == id,
                "Customer",
                "Room",
                "AdditionalServices"
            );
        }

        public void ReservationInsert(Reservation r)
        {
            _reservationDal.Insert(r);
        }

        public void ReservationDelete(Reservation r)
        {
            _reservationDal.Delete(r);
        }

        public void ReservationUpdate(Reservation r)
        {
            _reservationDal.Update(r);
        }

        public List<Reservation> Reservationliste()
        {
            return _reservationDal.ListWithIncludes(
                null,
                "Customer",
                "Room",
                "AdditionalServices"
            );
        }
    }
}
