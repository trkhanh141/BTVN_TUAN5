using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OFFSTEP.Models;
public class Order
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string CustomerName { get; set; }
    [Required, EmailAddress, StringLength(180)] public string Email { get; set; }
    [Required, StringLength(30)] public string Phone { get; set; }
    [Required, StringLength(400)] public string Address { get; set; }
    [StringLength(1000)] public string Note { get; set; }
    [Column(TypeName="decimal(18,2)")] public decimal Total { get; set; }
    [Required, StringLength(30)] public string Status { get; set; } = "Mới tiếp nhận";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
