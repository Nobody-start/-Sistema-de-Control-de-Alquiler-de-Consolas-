using Sistema.Entities;

namespace Sistema.Infrastructure.Interfaces
{
    public interface IAlquilerRepository
    {
        Task<IEnumerable<Alquiler>> GetAllAsync();
        Task<Alquiler?> GetByIdAsync(int id);
        Task<Alquiler> AddAsync(Alquiler alquiler);
        Task UpdateAsync(Alquiler alquiler);
        Task DeleteAsync(int id);
    }
}