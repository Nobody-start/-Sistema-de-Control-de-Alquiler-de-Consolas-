using Microsoft.AspNetCore.Mvc;
using Sistema.Entities;
using Sistema.Infrastructure.Interfaces;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {
        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IConsolaRepository _consolaRepository;

        public AlquileresController(
            IAlquilerRepository alquilerRepository,
            IClienteRepository clienteRepository,
            IConsolaRepository consolaRepository)
        {
            _alquilerRepository = alquilerRepository;
            _clienteRepository = clienteRepository;
            _consolaRepository = consolaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlquilerDTOs>>> GetAlquileres()
        {
            var alquileres = await _alquilerRepository.GetAllAsync();

            var resultado = alquileres.Select(a => new AlquilerDTOs
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
        public async Task<ActionResult<AlquilerDTOs>> GetAlquiler(int id)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(id);

            if (alquiler == null)
                return NotFound();

            var dto = new AlquilerDTOs
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
        public async Task<ActionResult<AlquilerDTOs>> PostAlquiler(AlquilerDTOs dto)
        {
            var consola = await _consolaRepository.GetByIdAsync(dto.ConsolaId);

            if (consola == null)
                return BadRequest("La consola indicada no existe.");

            var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);

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
                FechaDevolucionReal = dto.FechaDevolucionReal,
                Total = dto.Total,
                Estado = "Activo"
            };

            consola.Disponible = false;

            await _consolaRepository.UpdateAsync(consola);
            await _alquilerRepository.AddAsync(alquiler);

            dto.Id = alquiler.Id;
            dto.Estado = alquiler.Estado;

            return CreatedAtAction(nameof(GetAlquiler), new { id = alquiler.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlquiler(int id, AlquilerDTOs dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var alquiler = await _alquilerRepository.GetByIdAsync(id);

            if (alquiler == null)
                return NotFound();

            alquiler.FechaDevolucionPrevista = dto.FechaDevolucionPrevista;
            alquiler.FechaDevolucionReal = dto.FechaDevolucionReal;
            alquiler.Total = dto.Total;
            alquiler.Estado = dto.Estado;

            await _alquilerRepository.UpdateAsync(alquiler);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(id);

            if (alquiler == null)
                return NotFound();

            await _alquilerRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}