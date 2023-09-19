using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

class Order
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public IList<OrderLine> Lines { get; private set; }

    public Money TotalPrice { get; private set; }

    public void AddLine(OrderLine line)
    {
        Lines.Add(line);
        UpdatePrice();
    }

    void UpdatePrice()
    {
        // d
        TotalPrice = new Money(100);
    }


}
