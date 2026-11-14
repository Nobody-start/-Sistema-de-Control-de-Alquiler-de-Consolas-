using Microsoft.AspNetCore.Mvc;
using SistemaCA.Application.Contract;
using SistemaCA.Application.Dtos.Cliente;
using SistemaCA.Application.Dtos.Department.Clientes;
using SistemaCA.Domain.Entities;

namespace SistemaCA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllAsync();

            var resultado = clientes.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Telefono = c.Telefono,
                Correo = c.Correo
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            var dto = new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Telefono = cliente.Telefono,
                Correo = cliente.Correo
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(CrearClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            var resultado = await _clienteService.CreateAsync(cliente);

            var clienteDto = new ClienteDto
            {
                Id = resultado.Id,
                Nombre = resultado.Nombre,
                Telefono = resultado.Telefono,
                Correo = resultado.Correo
            };

            return CreatedAtAction(
                nameof(GetCliente),
                new { id = resultado.Id },
                clienteDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ActualizarClienteDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            var resultado = await _clienteService.UpdateAsync(id, cliente);

            if (resultado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var eliminado = await _clienteService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}