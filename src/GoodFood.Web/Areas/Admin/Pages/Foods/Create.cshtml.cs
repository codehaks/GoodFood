using System.ComponentModel.DataAnnotations;
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

        var categories = _foodCategoryService.GetAll();
        CategorySelectList = new SelectList(categories, "Id", "Name");
    }

    [BindProperty]
    public FoodInputModel FoodInput { get; set; }
    public SelectList CategorySelectList { get; set; }

    public async Task<IActionResult> OnPost()
    {

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var dto = FoodInput.Adapt<FoodCreateDto>();

        dto.ImageData = await GetImageDataAsync(FoodInput.ImageFile);

        try
        {
            await _foodService.CreateAsync(dto);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return Page();
        }


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
    [Length(1, 50, ErrorMessage = "")]
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }
    public required IFormFile ImageFile { get; set; }
}
