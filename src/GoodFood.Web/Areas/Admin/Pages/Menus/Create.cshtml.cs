using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace GoodFood.Web.Areas.Admin.Pages.Menus;

public class CreateModel : PageModel
{
    private readonly IMenuService _menuService;
    private readonly IFoodService _foodService;
    public CreateModel(IMenuService menuService, IFoodService foodService)
    {
        _menuService = menuService;
        _foodService = foodService;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _menuService.AddLine(new MenuLineCreateDto
        {
            Count = Input.Count,
            FoodId = Input.FoodId,
            Price = Input.Price
        });

        return RedirectToPage("./index");
    }

    public SelectList FoodSelectList { get; set; }
    public void OnGet()
    {
        var foods=_foodService.FindAll();
        FoodSelectList = new SelectList(foods, "Id", "Name");
    }


    public class InputModel
    {
        [Required(ErrorMessage = "کد غذا الزامی است")]
        [Range(1, int.MaxValue,ErrorMessage ="کد غذا نا معتبر است")]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "تعداد غذا الزامی است")]
        [Range(0,1000, ErrorMessage = " تعداد غذا نا معتبر است " )]
        public int Count { get; set; }

        [Required(ErrorMessage = "قیمت غذا الزامی است")]
        [Range(1000, 1_000_000, ErrorMessage = "قیمت غذا نا معتبر است")]
        public decimal Price { get; set; }
    }
}
