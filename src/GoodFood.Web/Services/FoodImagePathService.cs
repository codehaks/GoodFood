using GoodFood.Application.Contracts;

namespace GoodFood.Web.Services;

public class FoodImagePathService : IFoodImagePathService
{
    private readonly IWebHostEnvironment _environment;

    public FoodImagePathService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public string GetPath()
    {
        var _foodImagesPath = Path.Combine(_environment.ContentRootPath, "Files", "FoodImages");
        if (!Directory.Exists(_foodImagesPath))
        {
            Directory.CreateDirectory(_foodImagesPath);
        }
        return _foodImagesPath;
    }
}
