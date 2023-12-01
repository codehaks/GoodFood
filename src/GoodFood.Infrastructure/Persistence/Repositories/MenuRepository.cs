using GoodFood.Domain;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace GoodFood.Infrastructure.Persistence.Repositories;
public class MenuRepository : IMenuRepository
{
    private readonly GoodFoodDbContext _db;

    public MenuRepository(GoodFoodDbContext db)
    {
        _db = db;
    }

    public Menu GetMenu()
    {
        var lines = _db.MenuLines.Include(m => m.Food).Select(l => new MenuLine
        {
            Count = l.Count,
            FoodId = l.FoodId,
            Food = l.Food.Adapt<Food>(),
            Price = new Domain.Values.Money(l.Price),
            Details = l.Details
        }).AsNoTracking().ToList();

        return new Menu(lines);
    }

    public void Update(Menu menu)
    {
        var lines = _db.MenuLines;
        foreach (var line in lines)
        {
            lines.Remove(line);
        }

        foreach (var line in menu.Lines)
        {
            _db.MenuLines.Add(new Models.MenuLineData
            {
                Count = line.Count,
                FoodId = line.FoodId,
                Price = line.Price.Value,
                Details = line.Details
            });
        }
    }
}
