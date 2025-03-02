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
    public class AdditionalServiceManager : IAdditionalService
    {
        IAdditionalServiceDal _additionalService;
        public AdditionalServiceManager(IAdditionalServiceDal additionalServiceDal)
        {
            _additionalService = additionalServiceDal;
        }

        public void AdditionalDelete(AdditionalService ad)
        {
            throw new NotImplementedException();
        }

        public void AdditionalInsert(AdditionalService ad)
        {
            _additionalService.Insert(ad);
                }

        public List<AdditionalService> AdditionalServiceliste()
        {
            return _additionalService.liste();
        }

        public void AdditionalUpdate(AdditionalService ad)
        {
            throw new NotImplementedException();
        }

        public AdditionalService GetById(int id)
        {
            return _additionalService.Get(x => x.AdditionalServiceID == id);
        }
    }
}
