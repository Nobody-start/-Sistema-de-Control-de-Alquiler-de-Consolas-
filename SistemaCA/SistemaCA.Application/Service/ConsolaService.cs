using SistemaCA.Application.Core;
using SistemaCA.Application.Contract;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Interfaces;

namespace SistemaCA.Application.Service
{
    public class ConsolaService : BaseService<Consola>, IConsolaService
    {
        private readonly IConsolaRepository _consolaRepository;

        public ConsolaService(IConsolaRepository consolaRepository)
        {
            _consolaRepository = consolaRepository;
        }

        public override async Task<List<Consola>> GetAllAsync()
        {
            var consolas = await _consolaRepository.GetAllAsync();
            return consolas.ToList();
        }

        public override async Task<Consola?> GetByIdAsync(int id)
        {
            return await _consolaRepository.GetByIdAsync(id);
        }

        public override async Task<Consola> CreateAsync(Consola entity)
        {
            return await _consolaRepository.AddAsync(entity);
        }

        public override async Task<Consola?> UpdateAsync(int id, Consola entity)
        {
            var consolaExistente = await _consolaRepository.GetByIdAsync(id);

            if (consolaExistente == null)
                return null;

            entity.Id = id;

            await _consolaRepository.UpdateAsync(entity);

            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var consola = await _consolaRepository.GetByIdAsync(id);

            if (consola == null)
                return false;

            await _consolaRepository.DeleteAsync(id);

            return true;
        }
    }
}