# TastePoint v2 - Domain Analysis

## 🏛️ Core Domain Model

### Bounded Contexts
1. **Restaurant Management**
   - Menu Catalog
   - Inventory Management
   - Staff Management

2. **Order Processing**
   - Order Lifecycle
   - Kitchen Workflow
   - Payment Processing

3. **Table Service**
   - Table Management
   - Reservation System
   - Dine-in Service

4. **Customer Experience**
   - Customer Profiles
   - Order History
   - Loyalty Program

## 🎯 Rich Aggregates

### Order Aggregate (Root)
```csharp
public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = new();
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public TableId? TableId { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Money TotalAmount => CalculateTotal();
    public DateTime PlacedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    // Factory method
    public static Order CreateForTable(CustomerId customerId, TableId tableId)
    {
        var order = new Order
        {
            Id = OrderId.NewId(),
            CustomerId = customerId,
            TableId = tableId,
            Status = OrderStatus.Draft,
            PlacedAt = DateTime.UtcNow
        };
        
        return order;
    }
    
    public void AddItem(MenuItemId menuItemId, Quantity quantity, SpecialInstructions instructions)
    {
        // Business rules validation
        if (Status != OrderStatus.Draft)
            throw new OrderAlreadyPlacedException();
            
        if (quantity.Value <= 0)
            throw new InvalidQuantityException();
            
        var existingItem = _items.FirstOrDefault(i => i.MenuItemId == menuItemId);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity.Add(quantity));
        }
        else
        {
            var item = new OrderItem(menuItemId, quantity, instructions);
            _items.Add(item);
        }
        
        AddDomainEvent(new OrderItemAddedEvent(Id, menuItemId, quantity));
    }
    
    public void PlaceOrder()
    {
        if (!_items.Any())
            throw new EmptyOrderException();
            
        if (Status != OrderStatus.Draft)
            throw new OrderAlreadyPlacedException();
            
        Status = OrderStatus.Placed;
        PlacedAt = DateTime.UtcNow;
        AddDomainEvent(new OrderPlacedEvent(Id, CustomerId, TableId, TotalAmount));
    }
    
    public void ConfirmOrder()
    {
        if (Status != OrderStatus.Placed)
            throw new InvalidOrderStatusTransitionException(Status, OrderStatus.Confirmed);
            
        Status = OrderStatus.Confirmed;
        AddDomainEvent(new OrderConfirmedEvent(Id, EstimatePreparationTime()));
    }
    
    public void StartPreparation()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOrderStatusTransitionException(Status, OrderStatus.InPreparation);
            
        Status = OrderStatus.InPreparation;
        AddDomainEvent(new OrderPreparationStartedEvent(Id));
    }
    
    public void CompleteOrder()
    {
        if (Status != OrderStatus.InPreparation)
            throw new InvalidOrderStatusTransitionException(Status, OrderStatus.Completed);
            
        Status = OrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        AddDomainEvent(new OrderCompletedEvent(Id, CompletedAt.Value));
    }
    
    private Money CalculateTotal()
    {
        return new Money(_items.Sum(item => item.LineTotal.Value));
    }
    
    private TimeSpan EstimatePreparationTime()
    {
        // Business logic for preparation time estimation
        var baseTime = TimeSpan.FromMinutes(15);
        var itemTime = TimeSpan.FromMinutes(_items.Count * 3);
        return baseTime.Add(itemTime);
    }
}

public class OrderItem : Entity<OrderItemId>
{
    public MenuItemId MenuItemId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public SpecialInstructions Instructions { get; private set; }
    public Money LineTotal => new Money(UnitPrice.Value * Quantity.Value);
    
    public OrderItem(MenuItemId menuItemId, Quantity quantity, SpecialInstructions instructions)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
        Instructions = instructions;
        // UnitPrice would be set from menu item lookup
    }
    
    public void UpdateQuantity(Quantity newQuantity)
    {
        if (newQuantity.Value <= 0)
            throw new InvalidQuantityException();
            
        Quantity = newQuantity;
    }
}
```

### Table Aggregate (Root)
```csharp
public class Table : AggregateRoot<TableId>
{
    public TableNumber Number { get; private set; }
    public SeatingCapacity Capacity { get; private set; }
    public TableStatus Status { get; private set; }
    public OrderId? CurrentOrderId { get; private set; }
    public DateTime? OccupiedSince { get; private set; }
    public ReservationId? ReservationId { get; private set; }
    
    public static Table Create(TableNumber number, SeatingCapacity capacity)
    {
        return new Table
        {
            Id = TableId.NewId(),
            Number = number,
            Capacity = capacity,
            Status = TableStatus.Available
        };
    }
    
    public void AssignOrder(OrderId orderId)
    {
        if (Status != TableStatus.Available)
            throw new TableNotAvailableException();
            
        CurrentOrderId = orderId;
        Status = TableStatus.Occupied;
        OccupiedSince = DateTime.UtcNow;
        AddDomainEvent(new TableOccupiedEvent(Id, orderId, OccupiedSince.Value));
    }
    
    public void CompleteService()
    {
        if (Status != TableStatus.Occupied)
            throw new TableNotOccupiedException();
            
        CurrentOrderId = null;
        ReservationId = null;
        Status = TableStatus.Available;
        OccupiedSince = null;
        AddDomainEvent(new TableBecameAvailableEvent(Id));
    }
    
    public void Reserve(ReservationId reservationId, DateTime reservationTime)
    {
        if (Status != TableStatus.Available)
            throw new TableNotAvailableException();
            
        ReservationId = reservationId;
        Status = TableStatus.Reserved;
        AddDomainEvent(new TableReservedEvent(Id, reservationId, reservationTime));
    }
}
```

### Menu Aggregate (Root)
```csharp
public class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuItem> _items = new();
    private readonly List<MenuCategory> _categories = new();
    
    public RestaurantId RestaurantId { get; private set; }
    public MenuName Name { get; private set; }
    public MenuStatus Status { get; private set; }
    public DateTime LastUpdated { get; private set; }
    
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();
    public IReadOnlyList<MenuCategory> Categories => _categories.AsReadOnly();
    
    public void AddMenuItem(MenuItemName name, MenuItemDescription description, 
                           Money price, MenuCategoryId categoryId)
    {
        var item = new MenuItem(MenuItemId.NewId(), name, description, price, categoryId);
        _items.Add(item);
        LastUpdated = DateTime.UtcNow;
        AddDomainEvent(new MenuItemAddedEvent(Id, item.Id, name));
    }
    
    public void UpdateItemPrice(MenuItemId itemId, Money newPrice)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new MenuItemNotFoundException(itemId);
            
        item.UpdatePrice(newPrice);
        LastUpdated = DateTime.UtcNow;
        AddDomainEvent(new MenuItemPriceUpdatedEvent(Id, itemId, newPrice));
    }
    
    public void SetItemAvailability(MenuItemId itemId, bool isAvailable)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
            throw new MenuItemNotFoundException(itemId);
            
        item.SetAvailability(isAvailable);
        AddDomainEvent(new MenuItemAvailabilityChangedEvent(Id, itemId, isAvailable));
    }
}

public class MenuItem : Entity<MenuItemId>
{
    public MenuItemName Name { get; private set; }
    public MenuItemDescription Description { get; private set; }
    public Money Price { get; private set; }
    public MenuCategoryId CategoryId { get; private set; }
    public bool IsAvailable { get; private set; }
    public PreparationTime EstimatedPreparationTime { get; private set; }
    
    public MenuItem(MenuItemId id, MenuItemName name, MenuItemDescription description, 
                    Money price, MenuCategoryId categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        IsAvailable = true;
        EstimatedPreparationTime = PreparationTime.Default();
    }
    
    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Value < 0)
            throw new InvalidPriceException();
            
        Price = newPrice;
    }
    
    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}
```

## 💎 Value Objects

### Money Value Object
```csharp
public class Money : ValueObject
{
    public decimal Value { get; }
    public Currency Currency { get; }
    
    public Money(decimal value, Currency currency = Currency.USD)
    {
        if (value < 0)
            throw new ArgumentException("Money cannot be negative", nameof(value));
            
        Value = value;
        Currency = currency;
    }
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new CurrencyMismatchException();
            
        return new Money(Value + other.Value, Currency);
    }
    
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new CurrencyMismatchException();
            
        return new Money(Value - other.Value, Currency);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
    
    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    
    public override string ToString() => $"{Value:C} {Currency}";
}
```

### Other Key Value Objects
```csharp
public class TableNumber : ValueObject
{
    public int Value { get; }
    
    public TableNumber(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Table number must be positive");
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public class Quantity : ValueObject
{
    public int Value { get; }
    
    public Quantity(int value)
    {
        if (value < 0)
            throw new ArgumentException("Quantity cannot be negative");
        Value = value;
    }
    
    public Quantity Add(Quantity other) => new Quantity(Value + other.Value);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public class SpecialInstructions : ValueObject
{
    public string Value { get; }
    
    public SpecialInstructions(string value)
    {
        Value = value?.Trim() ?? string.Empty;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
```

## 🎭 Domain Events

### Order Events
```csharp
public class OrderPlacedEvent : IDomainEvent
{
    public OrderId OrderId { get; }
    public CustomerId CustomerId { get; }
    public TableId? TableId { get; }
    public Money TotalAmount { get; }
    public DateTime OccurredAt { get; }
    
    public OrderPlacedEvent(OrderId orderId, CustomerId customerId, 
                           TableId? tableId, Money totalAmount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TableId = tableId;
        TotalAmount = totalAmount;
        OccurredAt = DateTime.UtcNow;
    }
}

public class OrderConfirmedEvent : IDomainEvent
{
    public OrderId OrderId { get; }
    public TimeSpan EstimatedPreparationTime { get; }
    public DateTime OccurredAt { get; }
    
    public OrderConfirmedEvent(OrderId orderId, TimeSpan estimatedPreparationTime)
    {
        OrderId = orderId;
        EstimatedPreparationTime = estimatedPreparationTime;
        OccurredAt = DateTime.UtcNow;
    }
}
```

## 🏗️ Domain Services

### Pricing Service
```csharp
public class PricingService : IDomainService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IDiscountPolicy _discountPolicy;
    
    public async Task<Money> CalculateOrderTotalAsync(Order order)
    {
        var subtotal = Money.Zero();
        
        foreach (var item in order.Items)
        {
            var menuItem = await _menuRepository.GetMenuItemAsync(item.MenuItemId);
            var itemTotal = menuItem.Price.Multiply(item.Quantity.Value);
            subtotal = subtotal.Add(itemTotal);
        }
        
        var discount = await _discountPolicy.CalculateDiscountAsync(order, subtotal);
        return subtotal.Subtract(discount);
    }
}
```

### Kitchen Workflow Service
```csharp
public class KitchenWorkflowService : IDomainService
{
    public TimeSpan EstimatePreparationTime(Order order)
    {
        var baseTime = TimeSpan.FromMinutes(5); // Base prep time
        var itemTime = TimeSpan.FromMinutes(order.Items.Count * 2); // Per item
        var complexityTime = CalculateComplexityTime(order.Items);
        
        return baseTime.Add(itemTime).Add(complexityTime);
    }
    
    private TimeSpan CalculateComplexityTime(IEnumerable<OrderItem> items)
    {
        // Business logic for complexity calculation
        return TimeSpan.FromMinutes(items.Count(i => i.HasSpecialInstructions) * 3);
    }
}
```

## 🎯 Ubiquitous Language

### Restaurant Domain Terms
- **Order** - A customer's request for food items
- **Table** - Physical seating location in restaurant
- **Menu Item** - Food or beverage available for order
- **Kitchen** - Food preparation area and workflow
- **Service** - The complete customer experience cycle
- **Reservation** - Advanced table booking
- **Preparation Time** - Time needed to prepare an order
- **Special Instructions** - Customer customization requests

### Business Rules
1. **Orders can only be placed for available menu items**
2. **Tables must be available before assignment**
3. **Order total includes all applicable taxes and fees**
4. **Kitchen preparation follows FIFO (First In, First Out)**
5. **Table turnover must be optimized for revenue**

This domain model provides a rich, expressive foundation that truly captures the restaurant's business logic and operational needs.
