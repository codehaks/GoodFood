using GoodFood.Domain.Common;

namespace GoodFood.Domain.Values;

public class Money : ValueObject
{
    public Money(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException("Money can not be less than 0");
        }

        Value = value;
    }

    public decimal Value { get; }

    public Money ConvertToTomans()
    {
        return new Money(Value / 10);
    }

    public Money ConvertToDollars(decimal rate)
    {

        return new Money(Math.Ceiling(Value * rate));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Money operator +(Money a, Money b)
    {
        return new Money(a.Value + b.Value);
    }
    public static Money operator -(Money a, Money b)
    {
        return new Money(a.Value - b.Value);
    }
}
