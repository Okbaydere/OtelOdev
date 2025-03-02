using Business.Concreate;
using Data.EntityFramwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class RoomsController : Controller
    {
        private readonly RoomManager _roomManager;
        private readonly RoomTypeManager _roomTypeManager;

        public RoomsController()
        {
            _roomManager = new RoomManager(new EfRoomsDal());
            _roomTypeManager = new RoomTypeManager(new EfRoomTypeDal());
        }

        // GET: Rooms
        public ActionResult Index()
        {
            var roomTypes = _roomTypeManager.RoomTypeliste();
            return View(roomTypes);
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int id)
        {
            var roomType = _roomTypeManager.GetById(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }

            // Bu oda tipine ait odaları getir
            var rooms = _roomManager.Roomsliste().Where(r => r.RoomTypeId == id).ToList();
            
            ViewBag.Rooms = rooms;
            return View(roomType);
        }
    }
}
