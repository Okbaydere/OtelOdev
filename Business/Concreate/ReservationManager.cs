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
            return _reservationDal.Get(x => x.ReservationID == id);
        }
        

        void IReservationService.ReservationDelete(Reservation r)
        {
            throw new NotImplementedException();
        }

        

        public List<Reservation> Reservationliste()
        {
            return _reservationDal.liste();
        }

        void IReservationService.ReservationUpdate(Reservation r)
        {
            throw new NotImplementedException();
        }

        public void ReservationInsert(Reservation r)
        {
            _reservationDal.Insert(r);
        }
    }
}
