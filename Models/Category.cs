using System.ComponentModel.DataAnnotations;
namespace OFFSTEP.Models;
public class Category
{
    public int Id { get; set; }
    [Required, StringLength(80)] public string Name { get; set; }
    [StringLength(300)] public string Description { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
