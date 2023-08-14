using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Core.entities; 

namespace BlogApp.Application.persitance.contacts
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(int id); 
        Task AddAsync(T entity); 
        Task UpdateAsync(T entity); 
        Task DeleteAsync(int id); 
    }
}
