using GoodFood.Application.Contracts;
using GoodFood.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.User.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public IndexModel(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        public IList<OrderInfo> RecentOrders { get; set; } = new List<OrderInfo>();
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public int CartItemsCount { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userId = User.GetUserId();
            
            // Get user's orders
            var allOrders = await _orderService.GetAllByUserIdAsync(userId);
            TotalOrders = allOrders.Count;
            RecentOrders = allOrders.OrderByDescending(o => o.LastUpdate).Take(5).ToList();
            
            // Calculate total spent (this would need a proper calculation based on order details)
            TotalSpent = TotalOrders * 85000; // Placeholder calculation
            
            // Get cart info
            var cart = await _cartService.GetByUserIdAsync(User.GetUserInfo());
            CartItemsCount = cart?.Sum(l => l.Quantity) ?? 0;
            
            return Page();
        }
    }
}
