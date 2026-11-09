using Microsoft.EntityFrameworkCore;
using Sistema.Entities;
using Sistema.Infrastructure.Context;
using Sistema.Infrastructure.Interfaces;

namespace Sistema.Infrastructure.Repositories
{
    public class AlquilerRepository : IAlquilerRepository
    {
        private readonly ApplicationDbContext _context;

        public AlquilerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alquiler>> GetAllAsync()
        {
            return await _context.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Consola)
                .Include(a => a.Pagos)
                .ToListAsync();
        }

        public async Task<Alquiler?> GetByIdAsync(int id)
        {
            return await _context.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Consola)
                .Include(a => a.Pagos)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Alquiler> AddAsync(Alquiler alquiler)
        {
            _context.Alquileres.Add(alquiler);
            await _context.SaveChangesAsync();
            return alquiler;
        }

        public async Task UpdateAsync(Alquiler alquiler)
        {
            _context.Alquileres.Update(alquiler);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alquiler = await _context.Alquileres.FindAsync(id);

            if (alquiler != null)
            {
                _context.Alquileres.Remove(alquiler);
                await _context.SaveChangesAsync();
            }
        }
    }
}