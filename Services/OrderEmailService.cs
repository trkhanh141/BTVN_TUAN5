using System.Net;
using System.Net.Mail;
using OFFSTEP.Models;

namespace OFFSTEP.Services;

public class OrderEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<OrderEmailService> _logger;
    public OrderEmailService(IConfiguration configuration, ILogger<OrderEmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendConfirmationAsync(Order order)
    {
        var address = _configuration["Email:Address"];
        var password = _configuration["Email:AppPassword"];
        if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(password)) return false;

        static string E(string value) => WebUtility.HtmlEncode(value ?? string.Empty);
        var rows = string.Join("", order.Items.Select(i =>
            $"<li>{i.Quantity} × {E(i.Product?.Name)} — {(i.UnitPrice * i.Quantity):N0} ₫</li>"));
        var body = $"<h2>OFFSTEP - Xác nhận đơn hàng #{order.Id}</h2><p>Xin chào {E(order.CustomerName)}, OFFSTEP đã nhận được đơn hàng của bạn.</p><ul>{rows}</ul><p><strong>Tổng cộng: {order.Total:N0} ₫</strong></p><p>Địa chỉ giao hàng: {E(order.Address)}</p>";
        try
        {
            using var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(address, password)
            };
            using var message = new MailMessage(address, order.Email, $"OFFSTEP - Đơn hàng #{order.Id}", body)
            {
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8
            };
            await client.SendMailAsync(message);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Order {OrderId} was saved, but confirmation email could not be sent.", order.Id);
            return false;
        }
    }
}
