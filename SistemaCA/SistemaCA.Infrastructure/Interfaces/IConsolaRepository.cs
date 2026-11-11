using SistemaCA.Domain.Entities;

namespace SistemaCA.Infrastructure.Interfaces
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