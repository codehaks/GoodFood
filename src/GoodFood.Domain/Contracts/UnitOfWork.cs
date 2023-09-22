using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFood.Domain.Contracts;
public interface IUnitOfWork
{
    public IMenuRepository MenuRepository { get; }
    Task CommitAsync();
}
