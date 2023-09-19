using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Areas.Admin.Pages.Foods;

public class IndexModel : PageModel
{
    private readonly IFoodService _foodService;

    public IndexModel(IFoodService foodService)
    {
        _foodService = foodService;
    }

    public IList<FoodDto> FoodList { get; set; }

    public void OnGet()
    {
        FoodList=_foodService.FindAll();
    }
}
