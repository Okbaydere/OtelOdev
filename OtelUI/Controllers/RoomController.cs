using Business.Concreate;
using Data.Abstract;
using Data.Concreate;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class RoomController : Controller
    {
        RoomManager rm = new RoomManager(new EfRoomsDal());


        // GET: Room
        public ActionResult Index()
        {
            var roomTypes = rm.Roomsliste();
            return View(roomTypes);
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            var roomType = rm.GetById(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            
            var rooms = rm.Roomsliste();
            ViewBag.AvailableRooms = rooms.Where(r => r.IsAvaliable).ToList();
            
            return View(roomType);
        }
    }
}
