# TastePoint v2 - Architecture Blueprint

## 🏗️ Clean Architecture Implementation

### Layer Structure
```
src/TastePoint/
├── Domain/                     # Core business logic
│   ├── Aggregates/            # Order, Menu, Table, Customer
│   │   ├── Order/             # Order aggregate root and entities
│   │   ├── Menu/              # Menu aggregate root and entities
│   │   ├── Table/             # Table aggregate root
│   │   └── Customer/          # Customer aggregate root
│   ├── ValueObjects/          # Money, Quantity, TableNumber, etc.
│   ├── Events/                # Domain events
│   ├── Services/              # Domain services
│   ├── Exceptions/            # Domain exceptions
│   └── Common/                # Base classes and interfaces
├── Application/               # Application logic
│   ├── Commands/              # Command handlers (CQRS)
│   │   ├── Orders/            # Order-related commands
│   │   ├── Tables/            # Table-related commands
│   │   └── Menu/              # Menu-related commands
│   ├── Queries/               # Query handlers (CQRS)
│   │   ├── Orders/            # Order queries
│   │   ├── Tables/            # Table queries
│   │   └── Menu/              # Menu queries
│   ├── Events/                # Application event handlers
│   ├── Policies/              # Business policies
│   ├── DTOs/                  # Data transfer objects
│   └── Contracts/             # Application service interfaces
├── Infrastructure/            # External concerns
│   ├── Persistence/           # EF Core, repositories
│   │   ├── Contexts/          # DbContext implementations
│   │   ├── Repositories/      # Repository implementations
│   │   ├── Configurations/    # EF Core entity configurations
│   │   └── Migrations/        # Database migrations
│   ├── Events/                # Event store implementation
│   ├── Services/              # External service implementations
│   ├── Email/                 # Email service implementation
│   ├── Files/                 # File storage implementation
│   └── Configuration/         # Infrastructure setup
└── Presentation/              # UI and API
    ├── Web/                   # Blazor Server components
    │   ├── Components/        # Reusable UI components
    │   ├── Pages/             # Page components
    │   ├── Shared/            # Shared layouts and components
    │   └── wwwroot/           # Static files
    ├── API/                   # Minimal APIs
    │   ├── Orders/            # Order endpoints
    │   ├── Tables/            # Table endpoints
    │   └── Menu/              # Menu endpoints
    └── Admin/                 # Admin dashboard
        ├── Components/        # Admin-specific components
        └── Pages/             # Admin pages
```

## 🔄 Hexagonal Architecture Ports & Adapters

### Primary Ports (Driven - Application Core)

#### Application Services (Use Cases)
```csharp
// Order Management Use Cases
public interface IOrderApplicationService
{
    Task<OrderId> PlaceOrderAsync(PlaceOrderCommand command);
    Task ConfirmOrderAsync(ConfirmOrderCommand command);
    Task StartPreparationAsync(StartPreparationCommand command);
    Task CompleteOrderAsync(CompleteOrderCommand command);
    Task CancelOrderAsync(CancelOrderCommand command);
}

// Table Management Use Cases
public interface ITableApplicationService
{
    Task<TableId> CreateTableAsync(CreateTableCommand command);
    Task AssignOrderToTableAsync(AssignOrderToTableCommand command);
    Task CompleteTableServiceAsync(CompleteTableServiceCommand command);
    Task ReserveTableAsync(ReserveTableCommand command);
}

// Menu Management Use Cases
public interface IMenuApplicationService
{
    Task<MenuItemId> AddMenuItemAsync(AddMenuItemCommand command);
    Task UpdateMenuItemPriceAsync(UpdateMenuItemPriceCommand command);
    Task SetMenuItemAvailabilityAsync(SetMenuItemAvailabilityCommand command);
    Task RemoveMenuItemAsync(RemoveMenuItemCommand command);
}

// Query Services
public interface IOrderQueryService
{
    Task<OrderDetails> GetOrderDetailsAsync(OrderId orderId);
    Task<IEnumerable<OrderSummary>> GetActiveOrdersAsync();
    Task<IEnumerable<OrderSummary>> GetOrdersByTableAsync(TableId tableId);
    Task<OrderStatistics> GetOrderStatisticsAsync(DateTime from, DateTime to);
}

public interface ITableQueryService
{
    Task<IEnumerable<TableStatus>> GetAllTablesAsync();
    Task<IEnumerable<TableStatus>> GetAvailableTablesAsync();
    Task<TableDetails> GetTableDetailsAsync(TableId tableId);
}

public interface IMenuQueryService
{
    Task<IEnumerable<MenuItemDto>> GetAvailableItemsAsync();
    Task<IEnumerable<MenuItemDto>> GetMenuItemsByCategoryAsync(MenuCategoryId categoryId);
    Task<MenuItemDetails> GetMenuItemAsync(MenuItemId id);
    Task<IEnumerable<MenuCategoryDto>> GetCategoriesAsync();
}
```

### Secondary Ports (Driving - Infrastructure)

#### Repository Ports
```csharp
// Domain Repository Contracts
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(OrderId id);
    Task<Order?> GetByIdOrDefaultAsync(OrderId id);
    Task SaveAsync(Order order);
    Task<IEnumerable<Order>> GetActiveOrdersAsync();
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(CustomerId customerId);
    Task<IEnumerable<Order>> GetOrdersByTableAsync(TableId tableId);
}

public interface ITableRepository
{
    Task<Table> GetByIdAsync(TableId id);
    Task<Table?> GetByIdOrDefaultAsync(TableId id);
    Task SaveAsync(Table table);
    Task<IEnumerable<Table>> GetAllAsync();
    Task<IEnumerable<Table>> GetAvailableTablesAsync();
    Task<Table?> GetByNumberAsync(TableNumber number);
}

public interface IMenuRepository
{
    Task<Menu> GetByIdAsync(MenuId id);
    Task SaveAsync(Menu menu);
    Task<MenuItem> GetMenuItemAsync(MenuItemId id);
    Task<IEnumerable<MenuItem>> GetAvailableItemsAsync();
    Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(MenuCategoryId categoryId);
}

// Unit of Work for Transaction Management
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

#### Event Store Port
```csharp
public interface IEventStore
{
    Task SaveEventsAsync(AggregateId aggregateId, IEnumerable<IDomainEvent> events, int expectedVersion);
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(AggregateId aggregateId);
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(AggregateId aggregateId, int fromVersion);
    Task<T> GetAggregateAsync<T>(AggregateId aggregateId) where T : AggregateRoot;
}

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : class, IDomainEvent;
    Task PublishAsync(IEnumerable<IDomainEvent> events);
}
```

#### External Service Ports
```csharp
// Notification Service Port
public interface INotificationService
{
    Task NotifyKitchenAsync(OrderPlacedEvent orderEvent);
    Task NotifyCustomerAsync(OrderReadyEvent orderEvent);
    Task SendOrderConfirmationAsync(OrderId orderId, EmailAddress customerEmail);
    Task SendTableReadyNotificationAsync(TableId tableId, PhoneNumber customerPhone);
}

// Email Service Port
public interface IEmailService
{
    Task SendAsync(EmailAddress to, EmailSubject subject, EmailBody body);
    Task SendTemplateAsync(EmailAddress to, EmailTemplate template, object model);
}

// File Storage Port
public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string contentType);
    Task<Stream> GetFileAsync(string fileName);
    Task DeleteFileAsync(string fileName);
    Task<bool> FileExistsAsync(string fileName);
}
```

## 🎯 CQRS Implementation

### Command Side (Write Operations)

#### Command Definitions
```csharp
// Order Commands
public record PlaceOrderCommand(
    CustomerId CustomerId,
    TableId? TableId,
    IEnumerable<OrderItemCommand> Items)
{
    public record OrderItemCommand(
        MenuItemId MenuItemId,
        int Quantity,
        string? SpecialInstructions);
}

public record ConfirmOrderCommand(OrderId OrderId);
public record StartPreparationCommand(OrderId OrderId);
public record CompleteOrderCommand(OrderId OrderId);
public record CancelOrderCommand(OrderId OrderId, string Reason);

// Table Commands
public record CreateTableCommand(int TableNumber, int SeatingCapacity);
public record AssignOrderToTableCommand(TableId TableId, OrderId OrderId);
public record CompleteTableServiceCommand(TableId TableId);
public record ReserveTableCommand(TableId TableId, ReservationId ReservationId, DateTime ReservationTime);

// Menu Commands
public record AddMenuItemCommand(
    string Name,
    string Description,
    decimal Price,
    MenuCategoryId CategoryId,
    int EstimatedPreparationMinutes);

public record UpdateMenuItemPriceCommand(MenuItemId MenuItemId, decimal NewPrice);
public record SetMenuItemAvailabilityCommand(MenuItemId MenuItemId, bool IsAvailable);
```

#### Command Handlers
```csharp
public class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand, OrderId>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IUnitOfWork _unitOfWork;
    
    public PlaceOrderCommandHandler(
        IOrderRepository orderRepository,
        ITableRepository tableRepository,
        IMenuRepository menuRepository,
        IEventPublisher eventPublisher,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _tableRepository = tableRepository;
        _menuRepository = menuRepository;
        _eventPublisher = eventPublisher;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<OrderId> HandleAsync(PlaceOrderCommand command)
    {
        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            // Create order aggregate
            var order = Order.CreateForTable(command.CustomerId, command.TableId);
            
            // Add items to order
            foreach (var item in command.Items)
            {
                // Validate menu item exists and is available
                var menuItem = await _menuRepository.GetMenuItemAsync(item.MenuItemId);
                if (!menuItem.IsAvailable)
                    throw new MenuItemNotAvailableException(item.MenuItemId);
                
                order.AddItem(
                    item.MenuItemId,
                    new Quantity(item.Quantity),
                    new SpecialInstructions(item.SpecialInstructions ?? string.Empty));
            }
            
            // Place the order
            order.PlaceOrder();
            
            // Assign table if specified
            if (command.TableId.HasValue)
            {
                var table = await _tableRepository.GetByIdAsync(command.TableId.Value);
                table.AssignOrder(order.Id);
                await _tableRepository.SaveAsync(table);
            }
            
            // Save order
            await _orderRepository.SaveAsync(order);
            
            // Publish domain events
            await _eventPublisher.PublishAsync(order.DomainEvents);
            
            await _unitOfWork.CommitTransactionAsync();
            
            return order.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

public class ConfirmOrderCommandHandler : ICommandHandler<ConfirmOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task HandleAsync(ConfirmOrderCommand command)
    {
        var order = await _orderRepository.GetByIdAsync(command.OrderId);
        
        order.ConfirmOrder();
        
        await _orderRepository.SaveAsync(order);
        await _eventPublisher.PublishAsync(order.DomainEvents);
        await _unitOfWork.SaveChangesAsync();
    }
}
```

### Query Side (Read Operations)

#### Query Definitions
```csharp
// Order Queries
public record GetOrderDetailsQuery(OrderId OrderId);
public record GetActiveOrdersQuery();
public record GetOrdersByTableQuery(TableId TableId);
public record GetOrderStatisticsQuery(DateTime From, DateTime To);

// Table Queries
public record GetAllTablesQuery();
public record GetAvailableTablesQuery();
public record GetTableDetailsQuery(TableId TableId);

// Menu Queries
public record GetAvailableMenuItemsQuery();
public record GetMenuItemsByCategoryQuery(MenuCategoryId CategoryId);
public record GetMenuItemDetailsQuery(MenuItemId MenuItemId);
```

#### Query Handlers
```csharp
public class GetOrderDetailsQueryHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetails>
{
    private readonly IOrderReadModelRepository _readRepository;
    
    public GetOrderDetailsQueryHandler(IOrderReadModelRepository readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<OrderDetails> HandleAsync(GetOrderDetailsQuery query)
    {
        return await _readRepository.GetOrderDetailsAsync(query.OrderId);
    }
}

public class GetActiveOrdersQueryHandler : IQueryHandler<GetActiveOrdersQuery, IEnumerable<OrderSummary>>
{
    private readonly IOrderReadModelRepository _readRepository;
    
    public async Task<IEnumerable<OrderSummary>> HandleAsync(GetActiveOrdersQuery query)
    {
        return await _readRepository.GetActiveOrdersAsync();
    }
}
```

#### Read Models
```csharp
// Read Models for Queries
public class OrderDetails
{
    public OrderId OrderId { get; set; }
    public CustomerId CustomerId { get; set; }
    public string CustomerName { get; set; }
    public TableId? TableId { get; set; }
    public int? TableNumber { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public IEnumerable<OrderItemDetails> Items { get; set; } = [];
}

public class OrderItemDetails
{
    public MenuItemId MenuItemId { get; set; }
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class OrderSummary
{
    public OrderId OrderId { get; set; }
    public CustomerId CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int? TableNumber { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PlacedAt { get; set; }
    public TimeSpan? EstimatedPreparationTime { get; set; }
}
```

## 🎭 Event-Driven Architecture

### Event Handling Pipeline
```csharp
// Domain Event Handler Pipeline
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;
    
    public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var @event in events)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = _serviceProvider.GetServices(handlerType);
            
            foreach (var handler in handlers)
            {
                try
                {
                    await (Task)handlerType.GetMethod("HandleAsync")!.Invoke(handler, [@event])!;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling domain event {EventType}", @event.GetType().Name);
                    throw;
                }
            }
        }
    }
}

// Example Event Handlers
public class OrderPlacedEventHandler : IDomainEventHandler<OrderPlacedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly IKitchenDisplayService _kitchenDisplay;
    
    public async Task HandleAsync(OrderPlacedEvent @event)
    {
        // Notify kitchen
        await _notificationService.NotifyKitchenAsync(@event);
        
        // Update kitchen display
        await _kitchenDisplay.AddOrderToQueueAsync(@event.OrderId);
        
        // Send customer confirmation
        // This could be handled by a separate handler for SRP
    }
}
```

## 🔧 Dependency Injection Configuration

### Service Registration
```csharp
// Program.cs - Dependency Registration
public static class ServiceConfiguration
{
    public static IServiceCollection AddTastePointServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Domain Services
        services.AddScoped<IPricingService, PricingService>();
        services.AddScoped<IKitchenWorkflowService, KitchenWorkflowService>();
        
        // Application Services
        services.AddScoped<IOrderApplicationService, OrderApplicationService>();
        services.AddScoped<ITableApplicationService, TableApplicationService>();
        services.AddScoped<IMenuApplicationService, MenuApplicationService>();
        
        // Query Services
        services.AddScoped<IOrderQueryService, OrderQueryService>();
        services.AddScoped<ITableQueryService, TableQueryService>();
        services.AddScoped<IMenuQueryService, MenuQueryService>();
        
        // Infrastructure Services
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Event Handling
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        
        // External Services
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileStorageService, FileStorageService>();
        
        // MediatR for CQRS
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PlaceOrderCommandHandler).Assembly));
        
        return services;
    }
}
```

This architecture blueprint provides a comprehensive foundation for building TastePoint v2 with proper separation of concerns, testability, and maintainability while following Clean Architecture and DDD principles.
