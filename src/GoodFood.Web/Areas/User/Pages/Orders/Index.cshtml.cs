using GoodFood.Application.Contracts;
using GoodFood.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.User.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IList<OrderInfo> Orders { get; set; }

    public async Task<IActionResult> OnGet()
    {
        Orders = await _orderService.GetAllByUserIdAsync(User.GetUserId());
        return Page();
    }
}
