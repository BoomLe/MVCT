using System.Linq.Expressions;
using System;
namespace MVCT.IRepository
{
    public interface IRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null);
       Task< T> GetAsync(Expression<Func<T ,bool>> filter = null,string? includeProperties = null);
       Task CreatedAsync(T entity);
        
       Task RemoveAsync(T entity);
       Task SaveAsync();
    }
    
}