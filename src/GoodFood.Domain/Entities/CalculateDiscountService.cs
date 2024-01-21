using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

public class CalculateDiscountService : ICalculateDiscountService
{
    public Money CalculateDiscount(Order order)
    {
        if (order.TotalAmount.Value > new Money(500_000).Value)
        {
            return new Money(order.TotalAmount.Value * 0.05M);
        }
        else if (order.TotalAmount.Value > new Money(1_000_000).Value)
        {
            return new Money(order.TotalAmount.Value * 0.05M);
        }
        else
        {
            return new Money(0);
        }
    }
}

