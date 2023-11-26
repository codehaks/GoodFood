using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using GoodFood.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Food> FindByIdAsync(int id)
    {
        var food = await _db.Foods.FindAsync(id);
        if (food is not null)
        {
            return food.Adapt<Food>();
        }

        throw new InvalidOperationException();
    }

    public async Task UpdateAsync(Food food)
    {
        var data = await _db.Foods.FindAsync(food.Id);

        if (data is null) { throw new InvalidOperationException(); }
        data.Name = food.Name;
        data.Description = food.Description;
        data.CategoryId = food.CategoryId;
        data.ImagePath = food.ImagePath;

        _db.Entry(data).State = EntityState.Modified;

    }

    public IList<Food> GetAll()
    {
        return _db.Foods
            .ProjectToType<Food>()
            .ToList();
    }

    public async Task<bool> ExistsByNameAsyncc(string name)
        => await _db.Foods.AnyAsync(f => f.Name == name);
}
