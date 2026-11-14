using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaCA.Application.Core
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public abstract Task<List<T>> GetAllAsync();

        public abstract Task<T?> GetByIdAsync(int id);

        public abstract Task<T> CreateAsync(T entity);

        public abstract Task<T?> UpdateAsync(int id, T entity);

        public abstract Task<bool> DeleteAsync(int id);
    }
}
