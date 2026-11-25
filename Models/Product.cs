using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OFFSTEP.Models;
public class Product
{
    public int Id { get; set; }
    [Required, StringLength(160)] public string Name { get; set; }
    [Required, StringLength(80)] public string Sku { get; set; }
    [StringLength(2000)] public string Description { get; set; }
    [Column(TypeName="decimal(18,2)"), Range(0, 100000000)] public decimal Price { get; set; }
    [Range(0, 10000)] public int Stock { get; set; }
    [StringLength(300)] public string ImagePath { get; set; } = "/images/no-image.svg";
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
