using System.ComponentModel.DataAnnotations;
namespace OFFSTEP.ViewModels;
public class CheckoutViewModel
{
 [Required, StringLength(120), Display(Name="Họ và tên") ] public string CustomerName { get; set; }
 [Required, EmailAddress, StringLength(180), Display(Name="Email") ] public string Email { get; set; }
 [Required, Phone, StringLength(30), Display(Name="Số điện thoại") ] public string Phone { get; set; }
 [Required, StringLength(400), Display(Name="Địa chỉ nhận hàng") ] public string Address { get; set; }
 [StringLength(1000), Display(Name="Ghi chú") ] public string Note { get; set; }
}
