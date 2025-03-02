using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoomsService
    {
        List<Rooms> Roomsliste();
        void RoomsInsert(Rooms r);
        void RoomsUpdate(Rooms r );
        void RoomsDelete(Rooms r);
        Rooms GetById(int id);
    }
}
