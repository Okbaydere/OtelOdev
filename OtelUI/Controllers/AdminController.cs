using Business.Concreate;
using Data.EntityFramwork;
using Entities.Concreate;
using OtelUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class AdminController : Controller
    {
        // Manager nesneleri
        private readonly EmployeeManager _employeeManager;
        private readonly RoomManager _roomManager;
        private readonly ReservationManager _reservationManager;

        public AdminController()
        {
            // Dependency Injection
            _employeeManager = new EmployeeManager(new EfEmployeesDal());
            _roomManager = new RoomManager(new EfRoomsDal());
            _reservationManager = new ReservationManager(new EfReservationDal());
        }

        // GET: Admin
        public ActionResult Index()
        {
            // Dashboard için özet bilgileri toplama
            ViewBag.TotalEmployees = _employeeManager.Employeesliste().Count;
            ViewBag.ActiveEmployees = _employeeManager.GetActiveEmployees().Count;
            ViewBag.TotalRooms = _roomManager.Roomsliste().Count;
            ViewBag.AvailableRooms = _roomManager.Roomsliste().Count(r => r.IsAvaliable);
            ViewBag.TotalReservations = _reservationManager.Reservationliste().Count;
            
            return View();
        }

        #region Personel Yönetimi

        // GET: Admin/Employees
        public ActionResult Employees()
        {
            var employees = _employeeManager.Employeesliste();
            return View(employees);
        }

        // GET: Admin/EmployeeCreate
        public ActionResult EmployeeCreate()
        {
            return View();
        }

        // POST: Admin/EmployeeCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeCreate(Employees employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeManager.EmployeesInsert(employee);
                    return RedirectToAction("Employees");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hata oluştu: " + ex.Message);
                }
            }
            return View(employee);
        }

        // GET: Admin/EmployeeEdit/5
        public ActionResult EmployeeEdit(int id)
        {
            var employee = _employeeManager.GetById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/EmployeeEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeEdit(Employees employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeManager.EmployeesUpdate(employee);
                    return RedirectToAction("Employees");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hata oluştu: " + ex.Message);
                }
            }
            return View(employee);
        }

        // POST: Admin/EmployeeDelete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeDelete(int id)
        {
            try
            {
                var employee = _employeeManager.GetById(id);
                if (employee != null)
                {
                    _employeeManager.EmployeesDelete(employee);
                }
                return RedirectToAction("Employees");
            }
            catch (Exception ex)
            {
                // Hata mesajını TempData ile taşıyabiliriz
                TempData["ErrorMessage"] = "Hata oluştu: " + ex.Message;
                return RedirectToAction("Employees");
            }
        }

        #endregion

        #region Oda Yönetimi

        // GET: Admin/Rooms
        public ActionResult Rooms()
        {
            var rooms = _roomManager.Roomsliste();
            return View(rooms);
        }

        // GET: Admin/RoomDetails/5
        public ActionResult RoomDetails(int id)
        {
            var room = _roomManager.GetById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        #endregion

        #region Rezervasyon Yönetimi

        // GET: Admin/Reservations
        public ActionResult Reservations()
        {
            var reservations = _reservationManager.Reservationliste();
            return View(reservations);
        }

        // GET: Admin/ReservationDetails/5
        public ActionResult ReservationDetails(int id)
        {
            var reservation = _reservationManager.GetById(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        #endregion
    }
}
