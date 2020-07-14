using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientsManager.Data
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> AddTAsync(T obj);
        Task<int> DeleteTAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> searchCriteria);
        Task<T> GetOneByAsync(Expression<Func<T, bool>> searchCriteria);
        Task<int> UpdateTAsync(T obj);
    }
}