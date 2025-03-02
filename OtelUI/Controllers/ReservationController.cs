using Business.Concreate;
using OtelUI.Models;
using Data.Abstract;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OtelUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly RoomManager _roomManager;
        private readonly CustomerManager _customerManager;
        private readonly ReservationManager r;
        private readonly RoomTypeManager _roomtypeManager;
        private readonly AdditionalServiceManager _serviceManager;

        public ReservationController()
        {
            _roomManager = new RoomManager(new EfRoomsDal());
            _customerManager = new CustomerManager(new EfCustomerDal());
            r = new ReservationManager(new EfReservationDal());
            _roomtypeManager = new RoomTypeManager(new EfRoomTypeDal());
            _serviceManager = new AdditionalServiceManager(new EfAdditionalServiceDal());
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            // 1) Tüm oda tiplerini alıyoruz
            var allRoomTypes = _roomtypeManager.RoomTypeliste();

            // 2) Tüm odaları alıyoruz
            var allRooms = _roomManager.Roomsliste();

            // 3) Tüm ek hizmetleri alıyoruz
            var allServices = _serviceManager.AdditionalServiceliste();

            // 4) Aktif oda tiplerini filtrele
            var filteredRoomTypes = allRoomTypes
                .Where(rt => allRooms.Any(r => r.RoomTypeId == rt.RoomTypeId && r.IsAvaliable))
                .ToList();

            // 5) Müsait odaları al
            var availableRooms = allRooms
                .Where(r => r.IsAvaliable == true)
                .ToList();

            // 6) ViewModel'e at
            var model = new ReservationCreateViewModel
            {
                AdditionalServices = allServices,
                RoomTypes = filteredRoomTypes,
                AvailableRooms = availableRooms,
                EnterDate = DateTime.Now,
                ExitDate = DateTime.Now.AddDays(1)
            };

            return View(model);
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Hatalı form için listeleri tekrar doldur
                    model.RoomTypes = _roomtypeManager.RoomTypeliste();
                    model.AdditionalServices = _serviceManager.AdditionalServiceliste();
                    model.AvailableRooms = _roomManager.Roomsliste().Where(r => r.IsAvaliable).ToList();
                    return View(model);
                }

              
                // 1) Müşteri kontrolü
                if (string.IsNullOrEmpty(model.CustomerTc))
                {
                    ModelState.AddModelError("", "TC Kimlik numarası gereklidir.");
                    return View(model);
                }

                var existingCustomer = _customerManager.GetByTc(model.CustomerTc);
                if (existingCustomer != null)
                {
                    // Mevcut müşteriyi kullan
                    model.CustomerId = existingCustomer.CustomerID;
                }
                else
                {
                    // Yeni müşteri oluştur
                    var newCustomer = new Customer
                    {
                        CustomerTc = model.CustomerTc,
                        CustomerName = model.FirstName,
                        CustomerSurName = model.LastName,
                        CustomermMail = model.CustomermMail,
                        CustomerTel = model.CustomerTel
                    };
                    _customerManager.CustomerInsert(newCustomer);
                    model.CustomerId = newCustomer.CustomerID;
                }

                // 2) Oda müsait mi kontrol et
                if (!model.SelectedRoomId.HasValue)
                {
                    ModelState.AddModelError("", "Lütfen bir oda seçin.");
                    return View(model);
                }

                var selectedRoom = _roomManager.GetById(model.SelectedRoomId.Value);
                if (selectedRoom == null)
                {
                    ModelState.AddModelError("", "Seçilen oda bulunamadı.");
                    return View(model);
                }

                // Aynı tarih aralığında başka rezervasyon var mı kontrol et
                var existingReservations = r.Reservationliste().Where(res => res.RoomId == model.SelectedRoomId.Value).ToList();
                var hasConflict = existingReservations.Any(res =>
                    (model.EnterDate >= res.EnterDate && model.EnterDate < res.ExitDate) || 
                    (model.ExitDate > res.EnterDate && model.ExitDate <= res.ExitDate) ||
                    (model.EnterDate <= res.EnterDate && model.ExitDate >= res.ExitDate));

                if (hasConflict)
                {
                    ModelState.AddModelError("", "Seçilen tarih aralığında bu oda için başka bir rezervasyon bulunmaktadır.");
                    return View(model);
                }

                // 3) Rezervasyon oluştur
                var newReservation = new Reservation
                {
                    EnterDate = model.EnterDate,
                    ExitDate = model.ExitDate,
                    Fee = CalculateReservationFee(model),
                    PersonNumber = model.PersonNumber ?? 1,
                    CustomerID = model.CustomerId,
                    RoomId = model.SelectedRoomId
                };

                // 4) Rezervasyonu kaydet
                r.ReservationInsert(newReservation);

                // 5) Ek hizmetleri bağla
                if (model.SelectedAdditionalServiceIDs != null && model.SelectedAdditionalServiceIDs.Any())
                {
                    foreach (var serviceId in model.SelectedAdditionalServiceIDs)
                    {
                        var service = _serviceManager.GetById(serviceId);
                        if (service != null)
                        {
                            // ReservationID'yi doğrudan atayarak çift kayıt sorununu önlüyoruz
                            service.ReservationID = newReservation.ReservationID;
                            _serviceManager.AdditionalUpdate(service);
                        }
                    }
                }
                return RedirectToAction("Confirmation", new { id = newReservation.ReservationID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Rezervasyon oluşturulurken bir hata oluştu: " + ex.Message);
                model.RoomTypes = _roomtypeManager.RoomTypeliste();
                model.AdditionalServices = _serviceManager.AdditionalServiceliste();
                model.AvailableRooms = _roomManager.Roomsliste().Where(r => r.IsAvaliable).ToList();
                return View(model);
            }
        }

        // Rezervasyon ücretini hesapla
        private int CalculateReservationFee(ReservationCreateViewModel model)
        {
            decimal totalFee = 0;

            // 1) Oda tipi fiyatını al
            var roomType = _roomtypeManager.GetById(model.RoomTypeId);
            if (roomType != null && roomType.TypePrice.HasValue)
            {
                totalFee += roomType.TypePrice.Value;
            }

            // 2) Seçilen ek hizmetlerin fiyatlarını ekle
            if (model.SelectedAdditionalServiceIDs != null)
            {
                foreach (var serviceId in model.SelectedAdditionalServiceIDs)
                {
                    var service = _serviceManager.GetById(serviceId);
                    if (service != null && service.ServicePrice.HasValue)
                    {
                        totalFee += service.ServicePrice.Value;
                    }
                }
            }

            // 3) Konaklama süresini hesapla
            var days = (model.ExitDate - model.EnterDate).Days;
            if (days <= 0) days = 1;

            // 4) Toplam ücreti konaklama süresiyle çarp
            totalFee *= days;

            return (int)totalFee;
        }

        // GET: Reservation/Create
        public JsonResult GetRoomTypePrice(int roomTypeId) // fiyatlar dinamik olarak değiştiği için Json oldu
        {
            // Manager veya Repository üzerinden oda tipini çek
            var roomType = _roomtypeManager.GetById(roomTypeId);
            if (roomType != null && roomType.TypePrice.HasValue)
            {
                return Json(new { success = true, price = roomType.TypePrice }, JsonRequestBehavior.AllowGet);
            }

            // Oda tipi bulunamadıysa
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // Tarih aralığına ve oda tipine göre uygun odaları getiren metot
        public JsonResult GetAvailableRooms(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Tüm rezervasyonları getir
                var allReservations = r.Reservationliste();
                
                // Seçilen oda tipine ait tüm odaları getir
                var roomsOfType = _roomManager.Roomsliste().Where(r => r.RoomTypeId == roomTypeId).ToList();
                
                // Bu tarih aralığında rezervasyonu olan odaları bul
                var reservedRoomIds = allReservations
                    .Where(r => 
                        // Rezervasyon tarihleri ile seçilen tarihler çakışıyorsa
                        (startDate < r.ExitDate && endDate > r.EnterDate))
                    .Select(r => r.RoomId)
                    .Distinct()
                    .ToList();
                
                // Rezervasyonu olmayan odaları filtrele
                var availableRooms = roomsOfType
                    .Where(r => !reservedRoomIds.Contains(r.RoomId))
                    .Select(r => new { 
                        RoomId = r.RoomId, 
                        RoomNo = r.RoomNo,
                        RoomTypeId = r.RoomTypeId
                    })
                    .ToList();
                
                return Json(new { success = true, rooms = availableRooms }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

 
        
        // Rezervasyon onay sayfası
        public ActionResult Confirmation(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Geçersiz rezervasyon ID!";
                return RedirectToAction("Create");
            }
            
            var reservation = r.GetById(id.Value);
            if (reservation == null)
            {
                TempData["ErrorMessage"] = "Rezervasyon bulunamadı!";
                return RedirectToAction("Create");
            }
            
            // Müşteri bilgilerini al
            Customer customer = null;
            if (reservation.CustomerID.HasValue)
            {
                customer = _customerManager.GetById(reservation.CustomerID.Value);
            }
            
            // Oda bilgilerini al
            Rooms room = null;
            if (reservation.RoomId.HasValue)
            {
                room = _roomManager.GetById(reservation.RoomId.Value);
            }
            
            // Oda tipi bilgilerini al
            RoomType roomType = null;
            if (room != null && room.RoomTypeId > 0)
            {
                roomType = _roomtypeManager.GetById(room.RoomTypeId.Value);
            }
            
            // Ek hizmetleri al
            var additionalServices = _serviceManager.AdditionalServiceliste()
                .Where(s => s.Reservation != null && s.Reservation.ReservationID == id.Value)
                .ToList();
            
            // ViewModel oluştur
            var model = new ReservationConfirmationViewModel
            {
                Reservation = reservation,
                Customer = customer,
                Room = room,
                RoomType = roomType,
                AdditionalServices = additionalServices ?? new List<AdditionalService>()
            };
            
            return View(model);
        }
    }
}
