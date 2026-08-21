using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IGenericRepositry <T> where T : class
    {
      public Task<IReadOnlyList<T>> GetAllAsync();
      public Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
      public Task AddAsync(T entity);
      public Task UpdateAsync(T entity);
      public Task DeleteAsync(int id);
       public Task<T> GetByIdAsync(int id);
       public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);




    }
}
