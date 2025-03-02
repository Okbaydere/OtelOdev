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
    public class RoomManager : IRoomsService
    {
        IRoomsDal _roomsdal;
        public RoomManager(IRoomsDal rooms)
        {
            _roomsdal = rooms;
        }
        public Rooms GetById(int id)
        {
            return _roomsdal.Get(x => x.RoomId == id);
        }

        public void RoomsDelete(Rooms r)
        {
            throw new NotImplementedException();
        }

        public void RoomsInsert(Rooms r)
        {
            throw new NotImplementedException();
        }

        public List<Rooms> Roomsliste()
        {
            return _roomsdal.liste();
        }

        public void RoomsUpdate(Rooms r)
        {
            throw new NotImplementedException();
        }
    }
}
