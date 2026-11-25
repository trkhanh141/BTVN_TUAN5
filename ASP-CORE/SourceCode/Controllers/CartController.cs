using Microsoft.AspNetCore.Mvc;
using OFFSTEP.Services;
namespace OFFSTEP.Controllers;
public class CartController : Controller
{
 private readonly CartService _cart; public CartController(CartService cart)=>_cart=cart;
 public async Task<IActionResult> Index()=>View(await _cart.GetAsync());
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Add(int id) { await _cart.AddAsync(id); return RedirectToAction(nameof(Index)); }
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Update(int productId,int quantity) { await _cart.SetQuantityAsync(productId,quantity); return RedirectToAction(nameof(Index)); }
 [HttpPost,ValidateAntiForgeryToken] public IActionResult Remove(int id) { _cart.Remove(id); return RedirectToAction(nameof(Index)); }
}
