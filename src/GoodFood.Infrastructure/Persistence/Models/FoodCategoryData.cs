using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence.Models;
public class FoodCategoryData
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<FoodData>? Foods { get; set; }

}
