using SistemaCA.Application.Core;
using SistemaCA.Application.Contract;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Interfaces;

namespace SistemaCA.Application.Service
{
    public class AlquilerService : BaseService<Alquiler>, IAlquilerService
    {
        private readonly IAlquilerRepository _alquilerRepository;

        public AlquilerService(IAlquilerRepository alquilerRepository)
        {
            _alquilerRepository = alquilerRepository;
        }

        public override async Task<List<Alquiler>> GetAllAsync()
        {
            var alquileres = await _alquilerRepository.GetAllAsync();
            return alquileres.ToList();
        }

        public override async Task<Alquiler?> GetByIdAsync(int id)
        {
            return await _alquilerRepository.GetByIdAsync(id);
        }

        public override async Task<Alquiler> CreateAsync(Alquiler entity)
        {
            return await _alquilerRepository.AddAsync(entity);
        }

        public override async Task<Alquiler?> UpdateAsync(int id, Alquiler entity)
        {
            var alquilerExistente = await _alquilerRepository.GetByIdAsync(id);

            if (alquilerExistente == null)
                return null;

            entity.Id = id;

            await _alquilerRepository.UpdateAsync(entity);

            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(id);

            if (alquiler == null)
                return false;

            await _alquilerRepository.DeleteAsync(id);

            return true;
        }
    }
}