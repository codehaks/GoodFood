using GoodFood.Application.Contracts;
using GoodFood.Application.Notfications;
using GoodFood.Infrastructure.Persistence.Models;
using MediatR;
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
    private readonly IMediator _mediator;
    public GatewayController(IOrderService orderService, IEmailSender emailSender, UserManager<ApplicationUser> userManager, IMediator mediator)
    {
        _orderService = orderService;
        _emailSender = emailSender;
        _userManager = userManager;
        _mediator = mediator;
    }

    [Route("{orderId:guid}")]
    public async Task<IActionResult> GetAsync(Guid orderId) // TransactionId, OrderId
    {
        // Validation
        // Confirm Gateway

        // Confirm Order

        await _orderService.ConfirmedAsync(orderId);

        _mediator.Publish(new OrderCreatedNotification { OrderId = orderId });

        //var user = await _userManager.GetUserAsync(User);
        //if (user is not null)
        //{
        //    await _emailSender.SendEmailAsync(user.Email!, "ثبت سفارش", "سفارش با موفقیت ثبت شد");
        //}

        // Redirect to Order Page
        return RedirectToPage("/Orders/Details", new { area = "user", OrderId = orderId });
    }
}
