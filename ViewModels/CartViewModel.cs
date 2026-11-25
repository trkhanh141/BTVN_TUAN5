namespace OFFSTEP.ViewModels;
public class CartLineViewModel { public int ProductId { get; set; } public string Name { get; set; } public string ImagePath { get; set; } public decimal UnitPrice { get; set; } public int Quantity { get; set; } public decimal Subtotal => UnitPrice * Quantity; }
public class CartViewModel { public List<CartLineViewModel> Items { get; set; } = new(); public decimal Total => Items.Sum(x => x.Subtotal); public int Count => Items.Sum(x => x.Quantity); }
