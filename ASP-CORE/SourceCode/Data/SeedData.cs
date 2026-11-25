using Microsoft.EntityFrameworkCore;
using OFFSTEP.Models;
namespace OFFSTEP.Data;
public static class SeedData
{
 public static async Task InitializeAsync(ApplicationDbContext db)
 {
  if (!await db.Categories.AnyAsync())
  {
   var categories = new[] {
    new Category { Name="Giày chạy bộ", Description="Êm nhẹ cho nhịp chạy mỗi ngày" },
    new Category { Name="Sneaker lifestyle", Description="Phong cách thường nhật, dễ phối đồ" },
    new Category { Name="Giày tập luyện", Description="Ổn định cho vận động đa năng" }
   };
   db.Categories.AddRange(categories); await db.SaveChangesAsync();
  }
  if (!await db.Products.AnyAsync())
  {
   var c = await db.Categories.OrderBy(x=>x.Id).ToListAsync();
   var products = new[] {
    new Product { Name="OFFSTEP Aero Run 01", Sku="OF-AR01", ImagePath="/images/OF-AR01.svg", Description="Thân lưới thoáng khí, đệm đàn hồi cho những buổi chạy hằng ngày.", Price=1290000, Stock=25, CategoryId=c[0].Id, IsFeatured=true },
    new Product { Name="OFFSTEP Pace Flex", Sku="OF-AR02", ImagePath="/images/OF-AR02.svg", Description="Đế bám tốt, trọng lượng nhẹ và linh hoạt.", Price=1490000, Stock=18, CategoryId=c[0].Id },
    new Product { Name="OFFSTEP Court Classic", Sku="OF-SL01", ImagePath="/images/OF-SL01.svg", Description="Thiết kế tối giản, phối màu trắng đen dễ ứng dụng.", Price=1190000, Stock=30, CategoryId=c[1].Id, IsFeatured=true },
    new Product { Name="OFFSTEP Street Mono", Sku="OF-SL02", ImagePath="/images/OF-SL02.svg", Description="Phom dáng hiện đại dành cho phong cách đường phố.", Price=1390000, Stock=20, CategoryId=c[1].Id },
    new Product { Name="OFFSTEP Train Core", Sku="OF-TL01", ImagePath="/images/OF-TL01.svg", Description="Đế ổn định, hỗ trợ các bài tập sức mạnh và cardio.", Price=1590000, Stock=15, CategoryId=c[2].Id, IsFeatured=true },
    new Product { Name="OFFSTEP Move Lite", Sku="OF-TL02", ImagePath="/images/OF-TL02.svg", Description="Êm chân, thoải mái khi di chuyển và tập luyện.", Price=1090000, Stock=22, CategoryId=c[2].Id }
   };
   db.Products.AddRange(products); await db.SaveChangesAsync();
  }
 }
}
