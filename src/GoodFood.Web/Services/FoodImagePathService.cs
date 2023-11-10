using GoodFood.Application.Services;

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
        return Path.Combine(_environment.ContentRootPath, "Files", "FoodImages");
    }
}
