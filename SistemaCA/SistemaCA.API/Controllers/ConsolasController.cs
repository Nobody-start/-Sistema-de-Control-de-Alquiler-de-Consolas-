using Microsoft.AspNetCore.Mvc;
using SistemaCA.Application.Contract;
using SistemaCA.Application.Dtos.Consola;
using SistemaCA.Application.Dtos.Department.Consola;
using SistemaCA.Domain.Entities;

namespace SistemaCA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolasController : ControllerBase
    {
        private readonly IConsolaService _consolaService;

        public ConsolasController(IConsolaService consolaService)
        {
            _consolaService = consolaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsolaDto>>> GetConsolas()
        {
            var consolas = await _consolaService.GetAllAsync();

            var resultado = consolas.Select(c => new ConsolaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Marca = c.Marca,
                TarifaPorHora = c.TarifaPorHora,
                Disponible = c.Disponible
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsolaDto>> GetConsola(int id)
        {
            var consola = await _consolaService.GetByIdAsync(id);

            if (consola == null)
                return NotFound();

            var dto = new ConsolaDto
            {
                Id = consola.Id,
                Nombre = consola.Nombre,
                Marca = consola.Marca,
                TarifaPorHora = consola.TarifaPorHora,
                Disponible = consola.Disponible
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ConsolaDto>> PostConsola(CrearConsolaDto dto)
        {
            var consola = new Consola
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                TarifaPorHora = dto.TarifaPorHora,
                Disponible = dto.Disponible
            };

            var resultado = await _consolaService.CreateAsync(consola);

            var consolaDto = new ConsolaDto
            {
                Id = resultado.Id,
                Nombre = resultado.Nombre,
                Marca = resultado.Marca,
                TarifaPorHora = resultado.TarifaPorHora,
                Disponible = resultado.Disponible
            };

            return CreatedAtAction(
                nameof(GetConsola),
                new { id = resultado.Id },
                consolaDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsola(int id, ActualizarConsolaDto dto)
        {
            var consola = new Consola
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                TarifaPorHora = dto.TarifaPorHora,
                Disponible = dto.Disponible
            };

            var resultado = await _consolaService.UpdateAsync(id, consola);

            if (resultado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsola(int id)
        {
            var eliminado = await _consolaService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<ConsolaDto>>> GetConsolasDisponibles()
        {
            var consolas = await _consolaService.GetAllAsync();

            var resultado = consolas
                .Where(c => c.Disponible)
                .Select(c => new ConsolaDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Marca = c.Marca,
                    TarifaPorHora = c.TarifaPorHora,
                    Disponible = c.Disponible
                });

            return Ok(resultado);
        }
    }
}