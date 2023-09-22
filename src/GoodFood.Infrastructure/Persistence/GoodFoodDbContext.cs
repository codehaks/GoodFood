using GoodFood.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence;
public class GoodFoodDbContext:DbContext
{
    public GoodFoodDbContext(DbContextOptions<GoodFoodDbContext> options)
    : base(options)
    {
    }

    public DbSet<FoodData> Foods { get; set; }
    public DbSet<MenuLineData> MenuLines { get; set; }  
}
