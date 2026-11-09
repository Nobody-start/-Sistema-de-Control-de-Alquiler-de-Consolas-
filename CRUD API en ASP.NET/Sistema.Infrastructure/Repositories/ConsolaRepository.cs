using Microsoft.EntityFrameworkCore;
using Sistema.Entities;
using Sistema.Infrastructure.Context;
using Sistema.Infrastructure.Interfaces;

namespace Sistema.Infrastructure.Repositories
{
    public class ConsolaRepository : IConsolaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsolaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consola>> GetAllAsync()
        {
            return await _context.Consolas.ToListAsync();
        }

        public async Task<Consola?> GetByIdAsync(int id)
        {
            return await _context.Consolas.FindAsync(id);
        }

        public async Task<Consola> AddAsync(Consola consola)
        {
            _context.Consolas.Add(consola);
            await _context.SaveChangesAsync();
            return consola;
        }

        public async Task UpdateAsync(Consola consola)
        {
            _context.Consolas.Update(consola);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var consola = await _context.Consolas.FindAsync(id);

            if (consola != null)
            {
                _context.Consolas.Remove(consola);
                await _context.SaveChangesAsync();
            }
        }
    }
}