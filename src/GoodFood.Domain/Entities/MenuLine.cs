using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

public class MenuLine
{
    public int FoodId { get; set; }
    public required Food Food { get; set; }
    public int Count { get; set; }
    public required Money Price { get; set; }

    public required MenuLineDetails Details { get; set; }
}

public class MenuLineDetails
{
    public required Food Food { get; set; }
}
