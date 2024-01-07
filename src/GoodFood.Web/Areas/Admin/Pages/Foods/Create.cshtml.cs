using FluentValidation;
using FluentValidation.Results;
using GoodFood.Application.Contracts;
using GoodFood.Web.Filters;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoodFood.Web.Areas.Admin.Pages.Foods;

[ValidatePage]
[ValidateImage("FoodInput.ImageFile")]
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

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        var dto = FoodInput.Adapt<FoodCreateDto>();

        dto.ImageData = await GetImageDataAsync(FoodInput.ImageFile);

        try
        {
            var result = await _foodService.CreateAsync(dto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return Page();
            }
        }
        catch (Exception ex)
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

public class FoodInputValidator : AbstractValidator<FoodInputModel>
{
    public FoodInputValidator()
    {
        RuleFor(food => food.Name)
            .NotNull().WithMessage("الزامی است")
            .MaximumLength(50).WithMessage("نام طولانی است");


    }
}

public class FoodInputModel
{
    public string? Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
}

public static class Extensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError("FoodInput." + error.PropertyName, error.ErrorMessage);
        }
    }
}
