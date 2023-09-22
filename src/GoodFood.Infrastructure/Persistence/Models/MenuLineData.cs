using GoodFood.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Infrastructure.Persistence.Models;
public class MenuLineData
{
    [Key]
    public int FoodId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
