using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using OFFSTEP.Data;
using OFFSTEP.ViewModels;
namespace OFFSTEP.Services;
public class CartService
{
 private const string Key="OFFSTEP_CART"; private readonly IHttpContextAccessor _http; private readonly ApplicationDbContext _db;
 public CartService(IHttpContextAccessor http, ApplicationDbContext db) { _http=http; _db=db; }
 private Dictionary<int,int> Read() { var json=_http.HttpContext.Session.GetString(Key); return string.IsNullOrEmpty(json) ? new() : JsonSerializer.Deserialize<Dictionary<int,int>>(json) ?? new(); }
 private void Write(Dictionary<int,int> cart) => _http.HttpContext.Session.SetString(Key,JsonSerializer.Serialize(cart));
 public async Task AddAsync(int id) { var p=await _db.Products.FirstOrDefaultAsync(x=>x.Id==id && x.IsActive); if(p==null) return; var c=Read(); c[id]=Math.Min(c.GetValueOrDefault(id)+1,Math.Max(1,p.Stock)); Write(c); }
 public async Task<CartViewModel> GetAsync() { var c=Read(); var products=await _db.Products.Where(x=>c.Keys.Contains(x.Id)).ToListAsync(); return new CartViewModel { Items=products.Select(p=>new CartLineViewModel { ProductId=p.Id,Name=p.Name,ImagePath=p.ImagePath,UnitPrice=p.Price,Quantity=Math.Min(c[p.Id],Math.Max(1,p.Stock)) }).ToList() }; }
 public async Task SetQuantityAsync(int id,int quantity) { var c=Read(); var p=await _db.Products.FindAsync(id); if(p==null || quantity<=0) c.Remove(id); else c[id]=Math.Min(quantity,p.Stock); Write(c); }
 public void Remove(int id) { var c=Read(); c.Remove(id); Write(c); }
 public void Clear() => _http.HttpContext.Session.Remove(Key);
}
