namespace GoodFood.Application.Contracts;

public record FoodCategoryDto(string Id, string Name);

public interface IFoodCategoryService
{

    public IList<FoodCategoryDto> GetAll();
}
