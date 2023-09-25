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

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
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
