using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using GoodFood.Infrastructure.Persistence.Models;
namespace GoodFood.Infrastructure.Persistence.Repositories;
public class FoodRepository : IFoodRepository
{
    private readonly GoodFoodDbContext _db;

    public FoodRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public void Add(Food food)
    {
        var data = new FoodData()
        {
            Name = food.Name,
            Description = food.Description,
            CategoryId = food.CategoryId,
            ImagePath = food.ImagePath,
        };

        _db.Foods.Add(data);
    }

    public IList<Food> GetAll()
    {
        return _db.Foods
            .ProjectToType<Food>()
            .ToList();
    }
}
