using SistemaCA.Application.Core;
using SistemaCA.Application.Contract;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Interfaces;

namespace SistemaCA.Application.Service
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public override async Task<List<Cliente>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.ToList();
        }

        public override async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public override async Task<Cliente> CreateAsync(Cliente entity)
        {
            return await _clienteRepository.AddAsync(entity);
        }

        public override async Task<Cliente?> UpdateAsync(int id, Cliente entity)
        {
            var clienteExistente = await _clienteRepository.GetByIdAsync(id);

            if (clienteExistente == null)
                return null;

            entity.Id = id;

            await _clienteRepository.UpdateAsync(entity);

            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);

            if (cliente == null)
                return false;

            await _clienteRepository.DeleteAsync(id);

            return true;
        }
    }
}