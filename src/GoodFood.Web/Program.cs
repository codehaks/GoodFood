using System.Globalization;
using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using GoodFood.Domain.Contracts;
using GoodFood.Domain.Values;
using GoodFood.Infrastructure.Persistence;
using GoodFood.Infrastructure.Persistence.Repositories;
using GoodFood.Infrastructure.Services;
using GoodFood.Web.Common;
using GoodFood.Web.Hubs;
using GoodFood.Web.Services;
using Mapster;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NetMQ.Sockets;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(WriteLogs());

#pragma warning disable CA1305 // Specify IFormatProvider
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
#pragma warning restore CA1305 // Specify IFormatProvider

// Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GoodFood.Application.AssembelyHolder>();
    cfg.RegisterServicesFromAssemblyContaining<GoodFood.Infrastructure.AssembelyHolder>();
    cfg.NotificationPublisher = new TaskWhenAllPublisher();

});

builder.Services.AddSingleton<PushSocket>(provider =>
{
    var pushSocket = new PushSocket();
    pushSocket.Connect("tcp://127.0.0.1:5556");
    return pushSocket;
});

builder.Services.AddHostedService<RemoveExpiredCartsWorker>();
builder.Services.AddHostedService<EmailSenderWorker>();

builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
{
    //options.Conventions.AuthorizeAreaFolder("user", "/");
    //options.Conventions.AuthorizeAreaFolder("admin", "/", "RequireAdminRole");
});

builder.Services.AddDbContext<GoodFoodDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<GoodFoodDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

});

builder.Services.AddSingleton<IEmailQueueService, EmailQueueService>();
//builder.Services.AddScoped<ValidateDuplicateFoodNameAttribute>();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<BannedWordChecker>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IFoodService, FoodService>();

builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFoodImagePathService, FoodImagePathService>();
builder.Services.AddScoped<IFoodImageStorageService, FoodImageStorageService>();

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Mappings
TypeAdapterConfig<Money, decimal>.NewConfig().MapWith((src) => src.Value);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapRazorPages();

app.MapHub<OrderStatusHub>("/orderstatushub");

app.Run();

static Action<HostBuilderContext, LoggerConfiguration> WriteLogs()
    => (webHostBuilderContext, logger) =>
    {
        logger.ReadFrom.Configuration(webHostBuilderContext.Configuration);
        var cultureInfo = CultureInfo.CurrentCulture;

        if (webHostBuilderContext.HostingEnvironment.IsProduction())
        {
            var connectionString = webHostBuilderContext.Configuration.GetConnectionString("Log") ?? "";
            //logger.WriteTo.PostgreSQL(connectionString, "Logs", needAutoCreateTable: true, formatProvider: cultureInfo);
            logger.WriteTo.Console();
        }
       
    };
