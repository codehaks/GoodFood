using GoodFood.Application.Contracts;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Application.Services;
public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FoodService(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
    {
        _foodRepository = foodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(FoodCreateDto dto)
    {

        // TODO: Store ImageData to file
        // TODO: Get ImageUrl

        var food = new Food
        {
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            ImagePath = ""
        };


        //food.ImageData = dto.ImageData;

        _unitOfWork.FoodRepository.Add(food);
        await _unitOfWork.CommitAsync();
    }

    public IList<FoodDto> FindAll()
    {
        return _foodRepository.GetAll()
            .Adapt<IList<FoodDto>>();
    }
}

public class FoodDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
