using Data.Abstract;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class ServiceController : Controller
    {
        private IAdditionalServiceDal _serviceDal;

        public ServiceController()
        {
            _serviceDal = new EfAdditionalServiceDal();
        }

        // GET: Service
        public ActionResult Index()
        {
            var services = _serviceDal.liste();
            return View(services);
        }

        // GET: Service/Details/5
        public ActionResult Details(int id)
        {
            var service = _serviceDal.Get(x => x.AdditionalServiceID == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }
    }
}
