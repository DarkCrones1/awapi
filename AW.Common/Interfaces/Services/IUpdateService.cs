using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Services;
public interface IUpdateService<T> where T : IBaseQueryable
{
    Task Update(T entity);
}