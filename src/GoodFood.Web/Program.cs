using System.Globalization;
using GoodFood.Application.Contracts;
using GoodFood.Application.Services;
using GoodFood.Domain.Contracts;
using GoodFood.Infrastructure.Persistence;
using GoodFood.Infrastructure.Persistence.Models;
using GoodFood.Infrastructure.Persistence.Repositories;
using GoodFood.Infrastructure.Services;
using GoodFood.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(WriteLogs());

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
{
    //options.Conventions.AuthorizeAreaFolder("user", "/");
    //options.Conventions.AuthorizeAreaFolder("admin", "/", "RequireAdminRole");
});

builder.Services.AddDbContext<GoodFoodDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddScoped<IMenuService, MenuService>();

builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.Run();

static Action<HostBuilderContext, LoggerConfiguration> WriteLogs()
    => (webHostBuilderContext, logger) =>
    {
        logger.ReadFrom.Configuration(webHostBuilderContext.Configuration);

        if (webHostBuilderContext.HostingEnvironment.IsProduction())
        {
            var connectionString = webHostBuilderContext.Configuration.GetConnectionString("Log") ?? "";

            var cultureInfo = CultureInfo.CurrentCulture;
            //logger.WriteTo.PostgreSQL(connectionString, "Logs", needAutoCreateTable: true, formatProvider: cultureInfo);
        }
    };
