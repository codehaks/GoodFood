using GoodFood.Application.Services;
using GoodFood.Domain.Entities;

namespace GoodFood.Application.Contracts;
public interface IFoodService
{
    Task CreateAsync(FoodCreateDto dto);
    IList<FoodDto> FindAll();
}

public class FoodCreateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }

    public byte[]? ImageData { get; set; }
}
