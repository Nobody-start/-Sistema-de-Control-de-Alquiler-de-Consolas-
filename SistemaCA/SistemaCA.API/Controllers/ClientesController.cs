using Microsoft.AspNetCore.Mvc;
using SistemaCA.Domain.Entities;
using SistemaCA.Infrastructure.Interfaces;
using SistemaCA.API.DTOs;

namespace SistemaCA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repository;

        public ClientesController(IClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTOs>>> GetClientes()
        {
            var clientes = await _repository.GetAllAsync();

            var resultado = clientes.Select(c => new ClienteDTOs
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Telefono = c.Telefono,
                Correo = c.Correo
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTOs>> GetCliente(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDTOs
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTOs>> PostCliente(ClienteDTOs dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            await _repository.AddAsync(cliente);

            dto.Id = cliente.Id;

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDTOs dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var cliente = await _repository.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            cliente.Nombre = dto.Nombre;
            cliente.Telefono = dto.Telefono;
            cliente.Correo = dto.Correo;

            await _repository.UpdateAsync(cliente);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}