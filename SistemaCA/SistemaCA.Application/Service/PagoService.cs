using SistemaCA.Application.Core;
using SistemaCA.Application.Contract;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Interfaces;

namespace SistemaCA.Application.Service
{
    public class PagoService : BaseService<Pago>, IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public override async Task<List<Pago>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllAsync();
            return pagos.ToList();
        }

        public override async Task<Pago?> GetByIdAsync(int id)
        {
            return await _pagoRepository.GetByIdAsync(id);
        }

        public override async Task<Pago> CreateAsync(Pago entity)
        {
            return await _pagoRepository.AddAsync(entity);
        }

        public override async Task<Pago?> UpdateAsync(int id, Pago entity)
        {
            var pagoExistente = await _pagoRepository.GetByIdAsync(id);

            if (pagoExistente == null)
                return null;

            entity.Id = id;

            await _pagoRepository.UpdateAsync(entity);

            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return false;

            await _pagoRepository.DeleteAsync(id);

            return true;
        }
    }
}