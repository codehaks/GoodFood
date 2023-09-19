using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence.Models;
public class FoodData
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public required string Description { get; set; }

    public int CategoryId { get; set; }
    public FoodCategoryData? Category { get; set; }

}
