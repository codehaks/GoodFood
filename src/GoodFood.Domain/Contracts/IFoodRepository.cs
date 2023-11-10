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
    IList<Food> GetAll();
}
