using GoodFood.Application.Contracts;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using Mapster;

namespace GoodFood.Application.Services;
public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFoodImageStorageService _foodImageStorageService;
    private readonly IFoodImagePathService _foodImagePathService;

    public FoodService(IFoodRepository foodRepository, IUnitOfWork unitOfWork, IFoodImageStorageService foodImageStorageService, IFoodImagePathService foodImagePathService)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
        _foodImageStorageService = foodImageStorageService;
        _foodImagePathService = foodImagePathService;
    }

    public async Task<OperationResult> CreateAsync(FoodCreateDto dto)
    {
        // Validate DTO

        // Check duplicates
        var alreadyExists = await _unitOfWork.FoodRepository.ExistsByNameAsync(dto.Name);
        if (alreadyExists)
        {
            //  throw new InvalidOperationException("Food name already exists");
            return new OperationResult { Message = "Food name already exists", Success = false };
        }

        // Upload Image
        var fileName = Guid.NewGuid().ToString() + ".jpg";
        var path = _foodImagePathService.GetPath();
        var fullFileName = Path.Combine(path, fileName);

        if (dto.ImageData is not null)
        {
            await _foodImageStorageService.StoreAsync(dto.ImageData, fullFileName);
        }

        var food = new Food
        {
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            ImagePath = fullFileName
        };

        _unitOfWork.FoodRepository.Add(food);
        await _unitOfWork.CommitAsync();
        return new OperationResult { Message = "Food saved successfully.", Success = true };
    }

    public IList<FoodDto> FindAll()
    {
        return _foodRepository.GetAll()
            .Adapt<IList<FoodDto>>();
    }

    public async Task<FoodDetailsDto> FindByIdAsync(int id)
    {
        var food = await _unitOfWork.FoodRepository.FindByIdAsync(id);
        return food.Adapt<FoodDetailsDto>();
    }

    public async Task UpdateAsync(FoodEditDto dto)
    {

        // TODO: Store ImageData to file
        var fileName = Guid.NewGuid().ToString() + ".jpg";
        var path = _foodImagePathService.GetPath();
        var fullFileName = Path.Combine(path, fileName);
        if (dto.GetImageData() is not null)
        {
            await _foodImageStorageService.StoreAsync(dto.GetImageData(), fullFileName);
        }


        var food = new Food()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            ImagePath = fullFileName,
        };

        await _unitOfWork.FoodRepository.UpdateAsync(food);
        await _unitOfWork.CommitAsync();
    }


}

public class FoodDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}


public class OperationResult
{
    public bool Success { get; set; }
    public required string Message { get; set; }
}
