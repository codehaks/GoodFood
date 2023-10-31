using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.Admin.Pages.Orders;

public class DetailsModel(IOrderService orderService) : PageModel
{
    [BindProperty]
    public Guid OrderId { get; set; }
    public OrderDetails? Order { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid orderId)
    {
        Order = await orderService.GetOrderDetailsAsync(orderId);
        OrderId = orderId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await orderService.ReadyForPickupAsync(OrderId);
        return RedirectToPage("index");
    }


}
