using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.Admin.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public IList<OrderInfo>? OrderList { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        OrderList = await _orderService.GetAllAsync();
        return Page();

    }
}
