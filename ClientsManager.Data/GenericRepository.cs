using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientsManager.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// DbContext for DI
        /// </summary>
        private ClientsManagerDBContext _context;

        /// <summary>
        /// DbContext DI constructor injected
        /// </summary>
        /// <param name="context">ClientsManagerDBContext</param>
        public GenericRepository(ClientsManagerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all objects of type T
        /// </summary>
        /// <returns> Task<IEnumerable<T>> </returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Counts all the objects of type T
        /// </summary>
        /// <returns>Task<int> - A Task containing the number of objects of type T in the DB</returns>
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        /// <summary>
        /// Gets all objects of type T based on a search criteria
        /// </summary>
        /// <param name="searchCriteria">A lambda expression containing the search criteria</param>
        /// <returns>Task<IEnumerable<T>> - A list of objects of type T</returns>
        public async Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> searchCriteria)
        {
            return await _context.Set<T>().Where(searchCriteria).ToListAsync();
        }

        /// <summary>
        /// Gets one object of type T based on a search criteria
        /// </summary>
        /// <param name="searchCriteria">A lambda expression containing the search criteria</param>
        /// <returns>Task<IEnumerable<T>> - A list of objects of type T</returns>
        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> searchCriteria)
        {
            return await _context.Set<T>().Where(searchCriteria).FirstOrDefaultAsync();
        }


        public async Task<T> GetOneByWithRelatedDataAsync(Expression<Func<T, bool>> searchCriteria, Expression<Func<T, object>> includeCriteria)
        {
            return await _context.Set<T>()
                                        .Where(searchCriteria)
                                        .Include(includeCriteria)
                                        .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds an object of type T to the DB
        /// </summary>
        /// <param name="T">the object of type T to add</param>
        /// <returns>Task<int> - A Task object containing the number of entries added to the DB</returns>
        public async Task<int> AddTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("The object is mandatory");
            }

            _context.Set<T>().Add(obj);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing object of type T
        /// </summary>
        /// <param name="T">the object of type T to update</param>
        /// <returns>Task<int> - A Task object containing the number of entries updated at the DB</returns>
        public async Task<int> UpdateTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("The object is mandatory");
            }

            _context.Set<T>().Update(obj);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an existing object of type T
        /// </summary>
        /// <param name="obj">The obj of type T to remove</param>
        /// <returns>Task<int> - A Task object containing the number of entries deleted from the DB</returns>
        public async Task<int> DeleteTAsync(T obj)
        {
            if (obj is null)
            {
                throw new Exception("The object is mandatory");
            }

            _context.Set<T>().Remove(obj);
            return await _context.SaveChangesAsync();
        }
    }
}
