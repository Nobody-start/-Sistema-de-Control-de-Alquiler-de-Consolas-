using SistemaCA.Domain.Entities;

namespace SistemaCA.Infrastructure.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task<Pago> AddAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
    }
}