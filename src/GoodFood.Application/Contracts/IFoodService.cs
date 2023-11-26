using GoodFood.Application.Services;
using GoodFood.Domain.Entities;

namespace GoodFood.Application.Contracts;
public interface IFoodService
{
    Task<OperationResult> CreateAsync(FoodCreateDto dto);
    IList<FoodDto> FindAll();
    Task<FoodDetailsDto> FindByIdAsync(int id);
    Task UpdateAsync(FoodEditDto dto);
}

public class FoodDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }

    public byte[]? ImageData { get; set; }
    public string? ImagePath { get; set; }
}

public class FoodEditDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }

    private byte[]? _imageData;

    public byte[]? GetImageData()
    {
        return _imageData;
    }

    public void SetImageData(byte[] value)
    {
        _imageData = value;
    }
}

public class FoodCreateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int CategoryId { get; set; }

    public byte[]? ImageData { get; set; }
}
