using GoodFood.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GoodFood.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly IFoodImagePathService _foodImagePathService;
    private readonly IFoodService _foodService;

    public FoodController(IFoodImagePathService foodImagePathService, IFoodService foodService)
    {
        _foodImagePathService = foodImagePathService;
        _foodService = foodService;
    }

    [Route("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var food = await _foodService.FindByIdAsync(id);
        if (string.IsNullOrEmpty(food.ImagePath))
        {
            return NotFound();
        }

        var path = _foodImagePathService.GetPath();
        var fullPath = Path.Combine(path, food.ImagePath);

        if (System.IO.File.Exists(fullPath))
        {
            var imageData = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(imageData, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        return NotFound();
    }
}
