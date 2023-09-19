using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

class MenuLine
{
    public int FoodId { get; set; }
    public int Count { get; set; }
    public Money Price { get; set; }
}
