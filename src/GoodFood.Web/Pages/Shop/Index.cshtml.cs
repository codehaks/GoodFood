using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodFood.Web.Pages.Shop;

public class IndexModel : PageModel
{
    private readonly IFoodCategoryService _foodCategoryService;
    private readonly IMenuService _menuService;

    public IndexModel(IFoodCategoryService foodCategoryService, IMenuService menuService)
    {
        _foodCategoryService = foodCategoryService;
        _menuService = menuService;
    }
    public IList<FoodCategoryDto> FoodCategories { get; set; }
    public IList<MenuLineDto> MenuFoods { get; set; }
    public void OnGet(int categoryId = 0)
    {
        FoodCategories = _foodCategoryService.GetAll();
        MenuFoods= _menuService.GetAllFoods(categoryId);
    }
}
