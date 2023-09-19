namespace GoodFood.Domain.Entities;

class OrderLine
{
    public Guid OrderId { get; set; }
    public int FoodId { get; set; }
    public MenuLine MenuLine { get; set; }
    public int Count { get; set; }
}
