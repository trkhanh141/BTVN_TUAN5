using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OFFSTEP.Data;
namespace OFFSTEP.Controllers;
public class StoreController : Controller
{
 private readonly ApplicationDbContext _db; public StoreController(ApplicationDbContext db)=>_db=db;
 public async Task<IActionResult> Index(int? categoryId,string q) { var query=_db.Products.Include(x=>x.Category).Where(x=>x.IsActive); if(categoryId.HasValue) query=query.Where(x=>x.CategoryId==categoryId); if(!string.IsNullOrWhiteSpace(q)) query=query.Where(x=>x.Name.Contains(q)||x.Sku.Contains(q)); ViewBag.Categories=await _db.Categories.OrderBy(x=>x.Name).ToListAsync(); ViewBag.CategoryId=categoryId; ViewBag.Query=q; return View(await query.OrderByDescending(x=>x.IsFeatured).ThenBy(x=>x.Name).ToListAsync()); }
 public async Task<IActionResult> Details(int id) { var p=await _db.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id&&x.IsActive); return p==null?NotFound():View(p); }
 public IActionResult Error()=>View();
}
