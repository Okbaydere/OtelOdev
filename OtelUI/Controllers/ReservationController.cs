using Business.Concreate;
using OtelUI.Models;
using Data.Abstract;
using Data.EntityFramwork;
using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business.Abstract;

namespace OtelUI.Controllers
{
    public class ReservationController : Controller
    {
        // Manager örnekleri
        ReservationManager r = new ReservationManager(new EfReservationDal());
        CustomerManager _customerManager = new CustomerManager(new EfCustomerDal());
        AdditionalServiceManager _serviceManager = new AdditionalServiceManager(new EfAdditionalServiceDal());
        RoomTypeManager _roomtypeManager=new RoomTypeManager(new EfRoomTypeDal());
        RoomManager _roomManager = new RoomManager(new EfRoomsDal());


        public JsonResult GetRoomTypePrice(int roomTypeId)
        {
            // Manager veya Repository üzerinden oda tipini çek
            var roomType = _roomtypeManager.GetById(roomTypeId);
            if (roomType != null)
            {
                return Json(new { success = true, price = roomType.TypePrice }, JsonRequestBehavior.AllowGet);
            }

            // Oda tipi bulunamadıysa
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            // 1) Tüm oda tiplerini çek
            var allRoomTypes = _roomtypeManager.RoomTypeliste() ?? new List<RoomType>(); ;
            // Bu listede Rooms yüklenmedi, sadece RoomType tablosu var

            // 2) Tüm odaları çek
            var allRooms = _roomManager.Roomsliste() ?? new List<Rooms>(); ;
            // IsAvailable = true/false hepsi gelir

            // 3) IsAvailable = true olan odaların RoomTypeId değerlerini al
            var availableTypeIds = allRooms
                .Where(r => r.IsAvaliable == true)
                .Select(r => r.RoomTypeId)
                .Distinct()
                .ToList();

            // 4) Oda tipleri arasında, availableTypeIds içinde olanları filtrele
            var filteredRoomTypes = allRoomTypes
                .Where(rt => availableTypeIds.Contains(rt.RoomTypeId))
                .ToList();
            var allServices = _serviceManager.AdditionalServiceliste();

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
            // Model validation (eğer DataAnnotations kullandıysanız)
            if (!ModelState.IsValid)
            {
                // Hatalı form için listeleri tekrar doldur
                model.RoomTypes = _roomtypeManager.RoomTypeliste() ?? new List<RoomType>();
                model.AdditionalServices = _serviceManager.AdditionalServiceliste() ?? new List<AdditionalService>();
                model.AvailableRooms = _roomManager.Roomsliste().Where(r => r.IsAvaliable).ToList();
                
                // Hatalı formu geri döndür
                return View(model);
            }

            try
            {
                // 1) CUSTOMER OLUŞTUR
                Customer customer = new Customer
                {
                    CustomerName = model.FirstName,
                    CustomerSurName = model.LastName,
                    CustomerTel = model.CustomerTel,
                    CustomerTc = model.CustomerTc,
                    CustomermMail = model.CustomermMail
                };

                _customerManager.CustomerInsert(customer);

                // 2) RESERVATION NESNESİ OLUŞTUR
                var newReservation = new Reservation
                {
                    EnterDate = model.EnterDate,
                    ExitDate = model.ExitDate,
                    Fee = CalculateReservationFee(model),
                    PersonNumber = model.PersonNumber ?? 1,
                    CustomerID = customer.CustomerID,
                    RoomId = model.SelectedRoomId
                };

                // 3) Rezervasyonu kaydet
                r.ReservationInsert(newReservation);

                // 4) Seçilen odayı rezerve et
                if (model.SelectedRoomId.HasValue)
                {
                    var room = _roomManager.GetById(model.SelectedRoomId.Value);
                    if (room != null)
                    {
                        room.IsAvaliable = false;
                        room.ReservationID = newReservation.ReservationID;
                        _roomManager.RoomsUpdate(room);
                    }
                }

                // 5) Ek hizmetleri rezervasyona bağla
                if (model.SelectedAdditionalServiceIDs != null && model.SelectedAdditionalServiceIDs.Any())
                {
                    foreach (var serviceId in model.SelectedAdditionalServiceIDs)
                    {
                        var service = _serviceManager.GetById(serviceId);
                        if (service != null)
                        {
                            service.Reservation = newReservation;
                            _serviceManager.AdditionalUpdate(service);
                        }
                    }
                }

                // Başarılı kayıt sonrası Index sayfasına yönlendir
                TempData["SuccessMessage"] = "Rezervasyon başarıyla oluşturuldu!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Hata durumunda listeleri tekrar doldur
                model.RoomTypes = _roomtypeManager.RoomTypeliste() ?? new List<RoomType>();
                model.AdditionalServices = _serviceManager.AdditionalServiceliste() ?? new List<AdditionalService>();
                model.AvailableRooms = _roomManager.Roomsliste().Where(r => r.IsAvaliable).ToList();
                
                // Hata mesajı göster
                ModelState.AddModelError("", "Kayıt oluşturulurken hata oluştu: " + ex.Message);
                return View(model);
            }
        }

        // Rezervasyon ücretini hesapla
        private int CalculateReservationFee(ReservationCreateViewModel model)
        {
            decimal totalFee = 0;

            // 1. Oda ücreti hesapla
            if (model.RoomTypeId > 0)
            {
                var roomType = _roomtypeManager.GetById(model.RoomTypeId);
                if (roomType != null && roomType.TypePrice.HasValue)
                {
                    // Gün sayısını hesapla
                    int days = (int)(model.ExitDate - model.EnterDate).TotalDays;
                    if (days <= 0) days = 1; // En az 1 gün

                    totalFee += roomType.TypePrice.Value * days;
                }
            }

            // 2. Ek hizmet ücretlerini ekle
            if (model.SelectedAdditionalServiceIDs != null && model.SelectedAdditionalServiceIDs.Any())
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

            return (int)totalFee;
        }

        // Rezervasyonları listelemek için
        public ActionResult Index()
        {
            var reservations = r.Reservationliste();
            return View(reservations);
        }
    }
}
