using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.User.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly IOrderService _orderService;

    public DetailsModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Guid OrderId { get; set; }

    public OrderDetails? Order { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid orderId)
    {
        Order = await _orderService.GetOrderDetailsAsync(orderId);
        OrderId = orderId;

        return Page();
    }
}
