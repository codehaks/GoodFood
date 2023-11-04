using GoodFood.Application.Contracts;
using GoodFood.Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodFood.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GatewayController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<ApplicationUser> _userManager;
    public GatewayController(IOrderService orderService, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
    {
        _orderService = orderService;
        _emailSender = emailSender;
        _userManager = userManager;
    }

    public async Task<IActionResult> GetAsync([FromQuery] string orderId) // TransactionId, OrderId
    {
        // Validation
        // Confirm Gateway

        // Confirm Order

        await _orderService.ConfirmedAsync(Guid.Parse(orderId));

        var user = await _userManager.GetUserAsync(User);
        if (user is not null)
        {
            await _emailSender.SendEmailAsync(user.Email!, "ثبت سفارش", "سفارش با موفقیت ثبت شد");
        }

        // Redirect to Order Page
        return Ok();
    }
}
