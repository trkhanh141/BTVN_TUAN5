-- Optional SQL seed. The application also seeds these records in Data/SeedData.cs.
-- Run only after EF Core migrations. Guards prevent duplicate category/SKU inserts.
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Giày chạy bộ')
    INSERT INTO dbo.Categories (Name, Description) VALUES (N'Giày chạy bộ', N'Êm nhẹ cho nhịp chạy mỗi ngày');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Sneaker lifestyle')
    INSERT INTO dbo.Categories (Name, Description) VALUES (N'Sneaker lifestyle', N'Phong cách thường nhật, dễ phối đồ');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Giày tập luyện')
    INSERT INTO dbo.Categories (Name, Description) VALUES (N'Giày tập luyện', N'Ổn định cho vận động đa năng');

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-AR01')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Aero Run 01',N'OF-AR01',N'Thân lưới thoáng khí, đệm đàn hồi cho những buổi chạy hằng ngày.',1290000,25,N'/images/OF-AR01.svg',Id,1,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Giày chạy bộ';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-AR02')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Pace Flex',N'OF-AR02',N'Đế bám tốt, trọng lượng nhẹ và linh hoạt.',1490000,18,N'/images/OF-AR02.svg',Id,0,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Giày chạy bộ';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-SL01')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Court Classic',N'OF-SL01',N'Thiết kế tối giản, phối màu trắng đen dễ ứng dụng.',1190000,30,N'/images/OF-SL01.svg',Id,1,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Sneaker lifestyle';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-SL02')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Street Mono',N'OF-SL02',N'Phom dáng hiện đại dành cho phong cách đường phố.',1390000,20,N'/images/OF-SL02.svg',Id,0,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Sneaker lifestyle';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-TL01')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Train Core',N'OF-TL01',N'Đế ổn định, hỗ trợ các bài tập sức mạnh và cardio.',1590000,15,N'/images/OF-TL01.svg',Id,1,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Giày tập luyện';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'OF-TL02')
 INSERT INTO dbo.Products (Name,Sku,Description,Price,Stock,ImagePath,CategoryId,IsFeatured,IsActive,CreatedAt)
 SELECT N'OFFSTEP Move Lite',N'OF-TL02',N'Êm chân, thoải mái khi di chuyển và tập luyện.',1090000,22,N'/images/OF-TL02.svg',Id,0,1,SYSUTCDATETIME() FROM dbo.Categories WHERE Name=N'Giày tập luyện';
