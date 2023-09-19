using GoodFood.Application.Services;
using GoodFood.Domain.Entities;

namespace GoodFood.Application.Contracts;
public interface IFoodService
{
    IList<FoodDto> FindAll();
}