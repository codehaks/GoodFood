using GoodFood.Application.Contracts;
using GoodFood.Infrastructure.Persistence;
using Mapster;

namespace GoodFood.Infrastructure.Services;
public class FoodCategoryService(GoodFoodDbContext db) : IFoodCategoryService
{
    public IList<FoodCategoryDto> GetAll()
    {
        return db.FoodCategories.ProjectToType<FoodCategoryDto>().ToList();
    }
}
