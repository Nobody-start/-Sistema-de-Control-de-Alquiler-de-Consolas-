using Sistema.Entities;

namespace Sistema.Infrastructure.Interfaces
{
    public interface IConsolaRepository
    {
        Task<IEnumerable<Consola>> GetAllAsync();
        Task<Consola?> GetByIdAsync(int id);
        Task<Consola> AddAsync(Consola consola);
        Task UpdateAsync(Consola consola);
        Task DeleteAsync(int id);
    }
}