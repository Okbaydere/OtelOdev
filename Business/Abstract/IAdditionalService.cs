using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdditionalService
    {
        List<AdditionalService> AdditionalServiceliste();
        void AdditionalInsert(AdditionalService ad);
        void AdditionalUpdate(AdditionalService ad );
        void AdditionalDelete(AdditionalService ad);
        AdditionalService GetById(int id);
    }
}
