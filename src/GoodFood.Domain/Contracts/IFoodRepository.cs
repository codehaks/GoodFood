using GoodFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Domain.Contracts;
public interface IFoodRepository
{
    void Add(Food food);
    Task<bool> ExistsByNameAsyncc(string name);
    Task<Food> FindByIdAsync(int id);
    IList<Food> GetAll();
    Task UpdateAsync(Food food);
}
