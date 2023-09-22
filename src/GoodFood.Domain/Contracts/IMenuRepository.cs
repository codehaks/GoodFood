using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Domain.Contracts;
public interface IMenuRepository
{
    Menu GetMenu();
}
