using Microsoft.EntityFrameworkCore;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Context;
using SistemaCA.Infrastructure.Interfaces;

namespace SistemaCA.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDbContext _context;

        public PagoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Pagos
                .Include(p => p.Alquiler)
                .ToListAsync();
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _context.Pagos
                .Include(p => p.Alquiler)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pago> AddAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task UpdateAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago != null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }
}