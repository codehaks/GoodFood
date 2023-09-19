using GoodFood.Application.Contracts;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Entities;
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
            .Select(f => new FoodDto { Id = f.Id, Name = f.Name })
            .ToList();

    }
}

public class FoodDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}