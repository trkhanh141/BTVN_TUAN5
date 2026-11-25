using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OFFSTEP.Data;
using OFFSTEP.Models;
namespace OFFSTEP.Controllers;
[Authorize(Roles="Admin")]
public class AdminProductsController : Controller
{
 private readonly ApplicationDbContext _db; private readonly IWebHostEnvironment _env; public AdminProductsController(ApplicationDbContext db,IWebHostEnvironment env){_db=db;_env=env;}
 public async Task<IActionResult> Index()=>View(await _db.Products.Include(x=>x.Category).OrderByDescending(x=>x.CreatedAt).ToListAsync());
 public async Task<IActionResult> Create(){await LoadCategories();return View(new Product());}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(Product model,IFormFile image){if(image!=null) model.ImagePath=await SaveImage(image)??"/images/no-image.svg"; if(!ModelState.IsValid){await LoadCategories();return View(model);} _db.Products.Add(model);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
 public async Task<IActionResult> Edit(int id){var p=await _db.Products.FindAsync(id);if(p==null)return NotFound();await LoadCategories();return View(p);}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,Product model,IFormFile image){if(id!=model.Id)return NotFound();var current=await _db.Products.FindAsync(id);if(current==null)return NotFound(); if(!ModelState.IsValid){await LoadCategories();return View(model);} current.Name=model.Name;current.Sku=model.Sku;current.Description=model.Description;current.Price=model.Price;current.Stock=model.Stock;current.CategoryId=model.CategoryId;current.IsFeatured=model.IsFeatured;current.IsActive=model.IsActive; if(image!=null){var path=await SaveImage(image);if(path!=null)current.ImagePath=path;} await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
 public async Task<IActionResult> Delete(int id){var p=await _db.Products.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);return p==null?NotFound():View(p);}
 [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var p=await _db.Products.FindAsync(id);if(p!=null){p.IsActive=false;await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
 private async Task LoadCategories(){ViewBag.CategoryId=new SelectList(await _db.Categories.OrderBy(x=>x.Name).ToListAsync(),"Id","Name");}
 private async Task<string> SaveImage(IFormFile file){var ext=Path.GetExtension(file.FileName).ToLowerInvariant();if(file.Length==0||file.Length>5*1024*1024||!new[]{".jpg",".jpeg",".png",".webp"}.Contains(ext))return null;var folder=Path.Combine(_env.WebRootPath,"uploads");Directory.CreateDirectory(folder);var name=Guid.NewGuid().ToString("N")+ext;await using var fs=System.IO.File.Create(Path.Combine(folder,name));await file.CopyToAsync(fs);return "/uploads/"+name;}
}
