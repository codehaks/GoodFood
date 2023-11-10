using GoodFood.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodFood.Web.Areas.Admin.Pages.Foods;

public class CreateModel : PageModel
{
    private readonly IFoodService _foodService;
    private readonly IFoodCategoryService _foodCategoryService;

    public CreateModel(IFoodService foodService, IFoodCategoryService foodCategoryService)
    {
        _foodService = foodService;
        _foodCategoryService = foodCategoryService;
    }

    [BindProperty]
    public FoodInputModel FoodInput { get; set; }
    public SelectList CategorySelectList { get; set; }
    public void OnGet()
    {
        var categories = _foodCategoryService.GetAll();
        CategorySelectList = new SelectList(categories, "Id", "Name");
    }

    public async Task<IActionResult> OnPost()
    {

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var dto = FoodInput.Adapt<FoodCreateDto>();

        dto.ImageData = await GetImageDataAsync(FoodInput.ImageFile);

        await _foodService.CreateAsync(dto);

        return RedirectToPage("./index");
    }

    private static async Task<byte[]> GetImageDataAsync(IFormFile imageFile)
    {
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}

public class FoodInputModel
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }
    public required IFormFile ImageFile { get; set; }
}
