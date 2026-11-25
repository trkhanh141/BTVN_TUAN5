using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OFFSTEP.Data;
using OFFSTEP.Models;
using OFFSTEP.Services;
using OFFSTEP.ViewModels;
namespace OFFSTEP.Controllers;
public class CheckoutController : Controller
{
 private readonly ApplicationDbContext _db; private readonly CartService _cart; private readonly OrderEmailService _email;
 public CheckoutController(ApplicationDbContext db,CartService cart,OrderEmailService email){_db=db;_cart=cart;_email=email;}
 [HttpGet] public async Task<IActionResult> Index(){ var cart=await _cart.GetAsync(); if(!cart.Items.Any()) return RedirectToAction("Index","Cart"); return View(new CheckoutViewModel()); }
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Index(CheckoutViewModel model){ var cart=await _cart.GetAsync(); if(!cart.Items.Any()) ModelState.AddModelError("","Giỏ hàng đang trống."); if(!ModelState.IsValid) return View(model);
  await using var tx=await _db.Database.BeginTransactionAsync();
  foreach(var line in cart.Items){ var p=await _db.Products.FirstOrDefaultAsync(x=>x.Id==line.ProductId); if(p==null||p.Stock<line.Quantity){ModelState.AddModelError("",$"Sản phẩm {line.Name} không đủ tồn kho."); await tx.RollbackAsync(); return View(model);} }
  var order=new Order{CustomerName=model.CustomerName,Email=model.Email,Phone=model.Phone,Address=model.Address,Note=model.Note,Total=cart.Total,CreatedAt=DateTime.UtcNow};
  foreach(var line in cart.Items){ var p=await _db.Products.FindAsync(line.ProductId); p.Stock-=line.Quantity; order.Items.Add(new OrderItem{ProductId=p.Id,Quantity=line.Quantity,UnitPrice=p.Price}); }
  _db.Orders.Add(order); await _db.SaveChangesAsync(); await tx.CommitAsync();
  await _db.Entry(order).Collection(x=>x.Items).Query().Include(x=>x.Product).LoadAsync();
  await _email.SendConfirmationAsync(order);
  _cart.Clear(); return RedirectToAction(nameof(Completed),new{id=order.Id}); }
 public async Task<IActionResult> Completed(int id){var order=await _db.Orders.Include(x=>x.Items).ThenInclude(x=>x.Product).FirstOrDefaultAsync(x=>x.Id==id);return order==null?NotFound():View(order);}
}
