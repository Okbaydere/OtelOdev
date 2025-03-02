using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoomTypeService
    {
        List<RoomType> RoomTypeliste();
        void RoomTypeInsert(RoomType rt);
        void RoomTypeUpdate(RoomType rt);
        void RoomTypeDelete(RoomType rt);
        RoomType GetById(int id);
    }
}
