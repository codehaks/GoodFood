using GoodFood.Application.Contracts;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using Mapster;

namespace GoodFood.Application.Services;
public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddLine(MenuLineCreateDto menuLine)
    {
        // Fluent Validation

        var food = await _unitOfWork.FoodRepository.FindByIdAsync(menuLine.FoodId);

        var line = new MenuLine
        {
            Count = menuLine.Count,
            FoodId = menuLine.FoodId,
            Price = new Domain.Values.Money(menuLine.Price),
            Food = food,
            Details = new MenuLineDetails { Food = food }
        };

        var menu = _menuRepository.GetMenu();
        menu.Add(line);

        _unitOfWork.MenuRepository.Update(menu);
        await _unitOfWork.CommitAsync();

        // 


    }
    public IList<MenuLineDto> GetAll()
    {
        var menu = _menuRepository.GetMenu();
        return menu.Lines.Select(l => new MenuLineDto
        {
            Count = l.Count,
            FoodId = l.FoodId,
            Price = l.Price.Value,
            FoodName = l.Food.Name,
            Details = l.Details
        }).ToList();
    }

    public IList<MenuLineDto> GetAllFoods(int categoryId)
    {
        var menu = _menuRepository.GetMenu();
        return menu.Lines.Select(l => new MenuLineDto
        {
            Count = l.Count,
            FoodId = l.FoodId,
            Price = l.Price.Value,
            FoodName = l.Food.Name,
            Details = l.Details
        }).ToList();
    }
}

public record MyMenuLineDto(int FoodId, int Count, decimal Price);

public class MenuLineDto
{
    public string? FoodName { get; set; }
    public required int FoodId { get; init; }
    public int Count { get; init; }
    public decimal Price { get; init; }

    public required MenuLineDetails Details { get; set; }
}

public class MenuLineCreateDto
{
    public string? FoodName { get; set; }
    public required int FoodId { get; init; }
    public int Count { get; init; }
    public decimal Price { get; init; }
}
