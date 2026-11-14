using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCA.Application.Core
{
    public interface IBaseService<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T?> UpdateAsync(int id, T entity);

        Task<bool> DeleteAsync(int id);
    }
}
