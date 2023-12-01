using System.ComponentModel.DataAnnotations;
using GoodFood.Domain.Entities;

namespace GoodFood.Infrastructure.Persistence.Models;
public class MenuLineData
{
    [Key]
    public int Id { get; set; }
    public int FoodId { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }

    public FoodData? Food { get; set; }
    public required MenuLineDetails Details { get; set; }
}
