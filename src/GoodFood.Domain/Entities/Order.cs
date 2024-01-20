using GoodFood.Domain.Values;

namespace GoodFood.Domain.Entities;

public enum OrderStatus
{
    Pending,        // The order has been placed but not yet confirmed.
    Confirmed,      // The order has been confirmed by the restaurant.
    Preparing,      // The restaurant is preparing the order.
    ReadyForPickup, // The order is ready for pickup by the customer.
    OutForDelivery, // The order is out for delivery to the customer.
    Delivered,      // The order has been successfully delivered.
    Cancelled,      // The order has been cancelled.
    Failed,         // The order could not be fulfilled and has failed.
}

public interface ICalculateDiscountService
{
    Money CalculateDiscount(Order order);
}

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

public class Order
{
    private Order(Customer customer)
    {
        Customer = customer;
        Lines = [];
        Status = OrderStatus.Pending;
        LastUpdate = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
    public Order(Customer customer, OrderStatus status, DateTime lastUpdate)
    {
        Customer = customer;
        Lines = [];
        Status = status;
        LastUpdate = lastUpdate;

    }

    public Guid Id { get; set; }

    public Money? DiscountAmount { get; private set; }

    public void ApplyDiscount(Money discount)
    {
        DiscountAmount = discount;
    }

    public Customer Customer { get; init; }
    public IList<OrderLine> Lines { get; private set; }

    public Money TotalAmount { get; private set; } = new Money(0);

    public OrderStatus Status { get; private set; }
    public DateTime LastUpdate { get; private set; }

    public static Order FromCart(Cart cart)
    {
        var order = new Order(new Customer
        {
            UserId = cart.Customer.UserId,
            UserName = cart.Customer.UserName,
        });

        order.Status = OrderStatus.Pending;
        order.Lines = new List<OrderLine>();

        foreach (var item in cart.Lines)
        {
            order.AddLine(new OrderLine
            {
                FoodId = item.FoodId,
                FoodPrice = new Money(item.Price),
                Quantity = item.Quantity
            });
        }

        order.LastUpdate = DateTime.UtcNow;
        return order;
    }

    public void AddLine(OrderLine line)
    {
        Lines.Add(line);
        UpdateAmount();
    }

    public void Place()
    {
        var discountService = new CalculateDiscountService();
        var discount = discountService.CalculateDiscount(this);
        ApplyDiscount(discount);

        //
    }
    public void Confirm()
    {
        Status = OrderStatus.Confirmed;
        LastUpdate = DateTime.UtcNow;
    }

    private void UpdateAmount()
    {
        var lineAmount = new Money(Lines.Sum(l => l.LineTotal.Value));
        if (DiscountAmount != null)
        {
            TotalAmount = new Money(lineAmount.Value - DiscountAmount.Value);
        }
        else
        {
            TotalAmount = new Money(lineAmount.Value);
        }


    }

    public void ReadyForPickup()
    {
        Status = OrderStatus.ReadyForPickup;
        LastUpdate = DateTime.UtcNow;
    }
}

