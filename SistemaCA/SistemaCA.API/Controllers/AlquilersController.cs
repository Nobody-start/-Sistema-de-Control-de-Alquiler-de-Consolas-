using Microsoft.AspNetCore.Mvc;
using SistemaCA.Application.Contract;
using SistemaCA.Application.Dtos.Alquiler;
using SistemaCA.Domain.Entities;

namespace SistemaCA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {
        private readonly IAlquilerService _alquilerService;
        private readonly IClienteService _clienteService;
        private readonly IConsolaService _consolaService;

        public AlquileresController(
            IAlquilerService alquilerService,
            IClienteService clienteService,
            IConsolaService consolaService)
        {
            _alquilerService = alquilerService;
            _clienteService = clienteService;
            _consolaService = consolaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlquilerDto>>> GetAlquileres()
        {
            var alquileres = await _alquilerService.GetAllAsync();

            var resultado = alquileres.Select(a => new AlquilerDto
            {
                Id = a.Id,
                ConsolaId = a.ConsolaId,
                ClienteId = a.ClienteId,
                FechaInicio = a.FechaInicio,
                FechaDevolucionPrevista = a.FechaDevolucionPrevista,
                FechaDevolucionReal = a.FechaDevolucionReal,
                Total = a.Total,
                Estado = a.Estado
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlquilerDto>> GetAlquiler(int id)
        {
            var alquiler = await _alquilerService.GetByIdAsync(id);

            if (alquiler == null)
                return NotFound();

            var dto = new AlquilerDto
            {
                Id = alquiler.Id,
                ConsolaId = alquiler.ConsolaId,
                ClienteId = alquiler.ClienteId,
                FechaInicio = alquiler.FechaInicio,
                FechaDevolucionPrevista = alquiler.FechaDevolucionPrevista,
                FechaDevolucionReal = alquiler.FechaDevolucionReal,
                Total = alquiler.Total,
                Estado = alquiler.Estado
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<AlquilerDto>> PostAlquiler(CrearAlquilerDto dto)
        {
            var consola = await _consolaService.GetByIdAsync(dto.ConsolaId);

            if (consola == null)
                return BadRequest("La consola indicada no existe.");

            var cliente = await _clienteService.GetByIdAsync(dto.ClienteId);

            if (cliente == null)
                return BadRequest("El cliente indicado no existe.");

            if (!consola.Disponible)
                return BadRequest("La consola no está disponible.");

            var alquiler = new Alquiler
            {
                ConsolaId = dto.ConsolaId,
                ClienteId = dto.ClienteId,
                FechaInicio = dto.FechaInicio,
                FechaDevolucionPrevista = dto.FechaDevolucionPrevista,
                Estado = "Activo"
            };

            consola.Disponible = false;

            await _consolaService.UpdateAsync(consola.Id, consola);

            var resultado = await _alquilerService.CreateAsync(alquiler);

            var alquilerDto = new AlquilerDto
            {
                Id = resultado.Id,
                ConsolaId = resultado.ConsolaId,
                ClienteId = resultado.ClienteId,
                FechaInicio = resultado.FechaInicio,
                FechaDevolucionPrevista = resultado.FechaDevolucionPrevista,
                FechaDevolucionReal = resultado.FechaDevolucionReal,
                Total = resultado.Total,
                Estado = resultado.Estado
            };

            return CreatedAtAction(
                nameof(GetAlquiler),
                new { id = resultado.Id },
                alquilerDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlquiler(
            int id,
            ActualizarAlquilerDto dto)
        {
            var alquiler = new Alquiler
            {
                FechaDevolucionPrevista = dto.FechaDevolucionPrevista,
                FechaDevolucionReal = dto.FechaDevolucionReal,
                Total = dto.Total,
                Estado = dto.Estado
            };

            var resultado = await _alquilerService.UpdateAsync(id, alquiler);

            if (resultado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            var eliminado = await _alquilerService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}