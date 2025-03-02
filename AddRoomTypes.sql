-- RoomType tablosuna yeni alanlar eklemek için ALTER TABLE komutu
-- Eğer migration yapılmadıysa bu komutları çalıştırın
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypes]') AND name = 'Description')
BEGIN
    ALTER TABLE [dbo].[RoomTypes] ADD [Description] NVARCHAR(500) NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[RoomTypes]') AND name = 'ImageUrl')
BEGIN
    ALTER TABLE [dbo].[RoomTypes] ADD [ImageUrl] NVARCHAR(255) NULL;
END

-- Mevcut verileri temizle (isteğe bağlı)
-- DELETE FROM [dbo].[RoomTypes];

-- Oda tiplerini ekle
-- Kral Dairesi
INSERT INTO [dbo].[RoomTypes] (
    [TypeName], 
    [TypePrice], 
    [Capacity], 
    [Features], 
    [Description], 
    [ImageUrl]
)
VALUES (
    N'Kral Dairesi', 
    5000, 
    4, 
    N'Jakuzi, Özel Balkon, Minibar, 65" TV, Klima, Ücretsiz WiFi, Kahvaltı Dahil, Özel Şef Hizmeti', 
    N'Lüks ve konforun bir arada sunulduğu Kral Dairemiz, 120m² genişliğinde olup, şehir manzaralı geniş bir terasa sahiptir. Özel jakuzili banyosu, king size yatağı ve lüks oturma alanıyla unutulmaz bir konaklama deneyimi sunar.', 
    N'/Content/Images/RoomTypes/kral-dairesi.jpg'
);

-- Suit Oda
INSERT INTO [dbo].[RoomTypes] (
    [TypeName], 
    [TypePrice], 
    [Capacity], 
    [Features], 
    [Description], 
    [ImageUrl]
)
VALUES (
    N'Suit Oda', 
    3000, 
    3, 
    N'Oturma Alanı, Minibar, 55" TV, Klima, Ücretsiz WiFi, Kahvaltı Dahil', 
    N'75m² büyüklüğündeki Suit Odalarımız, ayrı bir oturma alanı ve çalışma masası içerir. Modern dekorasyonu ve konforlu yatağıyla hem iş hem de tatil amaçlı konaklamalar için idealdir.', 
    N'/Content/Images/RoomTypes/suit-oda.jpg'
);

-- Çift Kişilik Oda
INSERT INTO [dbo].[RoomTypes] (
    [TypeName], 
    [TypePrice], 
    [Capacity], 
    [Features], 
    [Description], 
    [ImageUrl]
)
VALUES (
    N'Çift Kişilik Oda', 
    1500, 
    2, 
    N'Queen Size Yatak, 43" TV, Klima, Ücretsiz WiFi, Kahvaltı Dahil', 
    N'40m² büyüklüğündeki Çift Kişilik Odalarımız, konforlu queen size yatağı ve modern banyosuyla çiftler için ideal bir konaklama seçeneğidir.', 
    N'/Content/Images/RoomTypes/cift-kisilik-oda.jpg'
);

-- Tek Kişilik Oda
INSERT INTO [dbo].[RoomTypes] (
    [TypeName], 
    [TypePrice], 
    [Capacity], 
    [Features], 
    [Description], 
    [ImageUrl]
)
VALUES (
    N'Tek Kişilik Oda', 
    1000, 
    1, 
    N'Tek Kişilik Yatak, 32" TV, Klima, Ücretsiz WiFi, Kahvaltı Dahil', 
    N'25m² büyüklüğündeki Tek Kişilik Odalarımız, tek kişilik konaklamalar için ekonomik ve konforlu bir seçenektir.', 
    N'/Content/Images/RoomTypes/tek-kisilik-oda.jpg'
);

-- Örnek odalar ekleme (her oda tipi için birkaç oda)
-- Kral Dairesi için odalar
DECLARE @KralDairesiID INT = (SELECT TOP 1 RoomTypeID FROM [dbo].[RoomTypes] WHERE TypeName = N'Kral Dairesi');

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Rooms]') AND name = 'RoomNo')
BEGIN
    ALTER TABLE [dbo].[Rooms] ADD [RoomNo] INT;
END

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Rooms]') AND name = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[Rooms] DROP COLUMN [IsActive];
END

INSERT INTO [dbo].[Rooms] ([RoomNo], [RoomTypeId], [IsAvaliable])
VALUES 
(101, @KralDairesiID, 1),
(102, @KralDairesiID, 1);

-- Suit Oda için odalar
DECLARE @SuitOdaID INT = (SELECT TOP 1 RoomTypeID FROM [dbo].[RoomTypes] WHERE TypeName = N'Suit Oda');

INSERT INTO [dbo].[Rooms] ([RoomNo], [RoomTypeId], [IsAvaliable])
VALUES 
(201, @SuitOdaID, 1),
(202, @SuitOdaID, 1),
(203, @SuitOdaID, 1),
(204, @SuitOdaID, 1);

-- Çift Kişilik Oda için odalar
DECLARE @CiftKisilikOdaID INT = (SELECT TOP 1 RoomTypeID FROM [dbo].[RoomTypes] WHERE TypeName = N'Çift Kişilik Oda');

INSERT INTO [dbo].[Rooms] ([RoomNo], [RoomTypeId], [IsAvaliable])
VALUES 
(301, @CiftKisilikOdaID, 1),
(302, @CiftKisilikOdaID, 1),
(303, @CiftKisilikOdaID, 1),
(304, @CiftKisilikOdaID, 1),
(305, @CiftKisilikOdaID, 1),
(306, @CiftKisilikOdaID, 1);

-- Tek Kişilik Oda için odalar
DECLARE @TekKisilikOdaID INT = (SELECT TOP 1 RoomTypeID FROM [dbo].[RoomTypes] WHERE TypeName = N'Tek Kişilik Oda');

INSERT INTO [dbo].[Rooms] ([RoomNo], [RoomTypeId], [IsAvaliable])
VALUES 
(401, @TekKisilikOdaID, 1),
(402, @TekKisilikOdaID, 1),
(403, @TekKisilikOdaID, 1),
(404, @TekKisilikOdaID, 1),
(405, @TekKisilikOdaID, 1),
(406, @TekKisilikOdaID, 1),
(407, @TekKisilikOdaID, 1),
(408, @TekKisilikOdaID, 1);
