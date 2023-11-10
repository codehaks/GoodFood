using GoodFood.Application.Contracts;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodFood.Web.Areas.Admin.Pages.Foods;

public class EditModel : PageModel
{
    private readonly IFoodCategoryService _foodCategoryService;
    private readonly IFoodService _foodService;

    public EditModel(IFoodCategoryService foodCategoryService, IFoodService foodService)
    {
        _foodCategoryService = foodCategoryService;
        _foodService = foodService;
    }

    [BindProperty]
    public FoodEditInputModel FoodInput { get; set; } = default!;

    public SelectList CategorySelectList { get; set; }

    public async Task OnGetAsync(int foodId)
    {
        var categories = _foodCategoryService.GetAll();
        CategorySelectList = new SelectList(categories, "Id", "Name");

        var details = await _foodService.FindByIdAsync(foodId);

        FoodInput = details.Adapt<FoodEditInputModel>();
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {

            var dto = FoodInput.Adapt<FoodEditDto>();
            if (FoodInput.ImageFile is not null)
            {
                dto.SetImageData(await GetImageDataAsync(FoodInput.ImageFile));
            }
            await _foodService.UpdateAsync(dto);

            return RedirectToPage("./Index");
        }

        return Page();
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

public class FoodEditInputModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
}

