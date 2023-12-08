using GoodFood.Application.Contracts;
using GoodFood.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace GoodFood.Web.Areas.Admin.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly IHubContext<OrderStatusHub> _hubContext;
    private readonly IOrderService _orderService;

    public DetailsModel(IOrderService orderService, IHubContext<OrderStatusHub> hubContext)
    {
        _orderService = orderService;
        _hubContext = hubContext;
    }

    [BindProperty]
    public Guid OrderId { get; set; }
    public OrderDetails? Order { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid orderId)
    {
        Order = await _orderService.GetOrderDetailsAsync(orderId);
        OrderId = orderId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _orderService.ReadyForPickupAsync(OrderId);
        var order = await _orderService.GetOrderDetailsAsync(OrderId);
        await _hubContext
            .Clients
            .User(order.UserId)
            .SendAsync("UpdateOrderStatus", order.Status.ToString());

        return RedirectToPage("index");
    }


}
