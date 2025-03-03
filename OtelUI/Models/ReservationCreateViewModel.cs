using Entities.Concreate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OtelUI.Models
{
    public class ReservationCreateViewModel
    {
        // Reservation ile ilgili
        [Required(ErrorMessage = "Giriş tarihi zorunludur")]
        [Display(Name = "Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime EnterDate { get; set; }

        [Required(ErrorMessage = "Çıkış tarihi zorunludur")]
        [Display(Name = "Çıkış Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ExitDate { get; set; }

        [Display(Name = "Toplam Ücret")]
        public decimal? Fee { get; set; }

        [Required(ErrorMessage = "Kişi sayısı zorunludur")]
        [Display(Name = "Kişi Sayısı")]
        [Range(1, 10, ErrorMessage = "Kişi sayısı 1-10 arasında olmalıdır")]
        public int? PersonNumber { get; set; }

        // Customer ile ilgili
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        [Display(Name = "Adınız")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        [Display(Name = "Soyadınız")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur")]
        [Display(Name = "TC Kimlik No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır")]
        public string CustomerTc { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur")]
        [Display(Name = "Telefon Numarası")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string CustomerTel { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur")]
        [Display(Name = "E-posta Adresi")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string CustomermMail { get; set; }

        // AdditionalService ile ilgili
        [Display(Name = "Ek Hizmet İster misiniz?")]
        public bool HasAdditionalService { get; set; }
        
        public string ServiceName { get; set; }
        public decimal? ServicePrice { get; set; }
        public string ServiceDescription { get; set; }
        
        [Display(Name = "Seçilen Ek Hizmetler")]
        public List<int> SelectedAdditionalServiceIDs { get; set; }
  


        // Birden fazla ek hizmet seçimi için:
        public IEnumerable<AdditionalService> AdditionalServices { get; set; }

        // ROOM TYPE
        [Required(ErrorMessage = "Oda tipi seçimi zorunludur")]
        [Display(Name = "Oda Tipi")]
        public int RoomTypeId { get; set; }
        
        public IEnumerable<RoomType> RoomTypes { get; set; }
        
        // Oda seçimi için
        [Required(ErrorMessage = "Oda seçimi zorunludur")]
        [Display(Name = "Oda")]
        public int? SelectedRoomId { get; set; }
        
        public IEnumerable<Rooms> AvailableRooms { get; set; }

    }
}
