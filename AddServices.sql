-- Ek Servisler için SQL Script
-- Bu script, otel uygulaması için ek servisleri ekler

-- Mevcut ek servisleri temizle
DELETE FROM AdditionalService;

-- Identity sıfırlama
DBCC CHECKIDENT ('AdditionalService', RESEED, 0);

-- Ek Servisleri ekle
INSERT INTO AdditionalService (ServiceName, ServicePrice, ServiceDescription, ImageUrl, ReservationID)
VALUES 
('Spa & Masaj', 500, 'Profesyonel masörlerimiz tarafından uygulanan rahatlatıcı masaj hizmeti. Vücudunuzu ve zihninizi dinlendirin.', '/Content/Images/Services/spa-massage.jpg', NULL),
('Özel Şoför', 750, 'Şehir içi ve şehirlerarası ulaşım için özel şoför hizmeti. Konforlu araçlarımızla seyahat edin.', '/Content/Images/Services/private-driver.jpg', NULL),
('Restoran Rezervasyonu', 200, 'Otelimizin lüks restoranında özel masa rezervasyonu. Şef özel menüsü ile unutulmaz bir akşam yemeği deneyimi.', '/Content/Images/Services/restaurant.jpg', NULL),
('Oda Servisi', 100, 'Günün her saati odanıza özel yemek servisi. Menümüzden dilediğinizi seçin, odanıza getirelim.', '/Content/Images/Services/room-service.jpg', NULL),
('Çamaşır & Ütü', 150, 'Kıyafetleriniz için profesyonel çamaşır ve ütü hizmeti. Aynı gün teslim edilir.', '/Content/Images/Services/laundry.jpg', NULL),
('Havaalanı Transferi', 300, 'Havaalanından otele veya otelden havaalanına özel transfer hizmeti. Konforlu araçlarımızla seyahat edin.', '/Content/Images/Services/airport-transfer.jpg', NULL),
('Bebek Bakıcısı', 400, 'Profesyonel bebek bakıcısı hizmeti. Çocuklarınız güvende, siz keyfinize bakın.', '/Content/Images/Services/babysitter.jpg', NULL),
('Özel Tur Rehberi', 600, 'Şehri keşfetmek için özel tur rehberi hizmeti. Yerel rehberlerimizle şehrin gizli köşelerini keşfedin.', '/Content/Images/Services/tour-guide.jpg', NULL);

-- Script başarıyla tamamlandı mesajı
PRINT 'Ek servisler başarıyla eklendi.';
