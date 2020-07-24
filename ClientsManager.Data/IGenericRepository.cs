using ClientsManager.Models;
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
        Task<IEnumerable<T>> GetAllPagedAsync(Expression<Func<T, object>> orderCriteria, QueryStringParameters parameters);
        Task<T> GetOneByAsync(Expression<Func<T, bool>> searchCriteria);
        Task<T> GetOneByWithRelatedDataAsync(Expression<Func<T, bool>> searchCriteria, Expression<Func<T, object>> includeCriteria);
        Task<int> UpdateTAsync(T obj);
    }
}