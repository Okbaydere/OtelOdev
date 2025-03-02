using Business.Abstract;
using Data.Abstract;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concreate
{
    public class RoomTypeManager : IRoomTypeService
    {

        IRoomTypeDal _roomTypeDal;
        public RoomTypeManager(IRoomTypeDal roomTypeDal)
        {
            _roomTypeDal = roomTypeDal;
        }
        public RoomType GetById(int id)
        {
            return _roomTypeDal.Get(x => x.RoomTypeId == id);
        }

        public void RoomTypeDelete(RoomType rt)
        {
            throw new NotImplementedException();
        }

        public void RoomTypeInsert(RoomType rt)
        {
            throw new NotImplementedException();
        }

        public List<RoomType> RoomTypeliste()
        {

           return _roomTypeDal.liste();
        }

        public void RoomTypeUpdate(RoomType rt)
        {
            throw new NotImplementedException();
        }
    }
}
