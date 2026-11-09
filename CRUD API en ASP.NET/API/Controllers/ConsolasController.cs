using Microsoft.AspNetCore.Mvc;
using Sistema.Entities;
using Sistema.Infrastructure.Interfaces;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolasController : ControllerBase
    {
        private readonly IConsolaRepository _repository;

        public ConsolasController(IConsolaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsolaDTOs>>> GetConsolas()
        {
            var consolas = await _repository.GetAllAsync();

            var resultado = consolas.Select(c => new ConsolaDTOs
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
        public async Task<ActionResult<ConsolaDTOs>> GetConsola(int id)
        {
            var consola = await _repository.GetByIdAsync(id);

            if (consola == null)
                return NotFound();

            var dto = new ConsolaDTOs
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
        public async Task<ActionResult<ConsolaDTOs>> PostConsola(ConsolaDTOs dto)
        {
            var consola = new Consola
            {
                Nombre = dto.Nombre,
                Marca = dto.Marca,
                TarifaPorHora = dto.TarifaPorHora,
                Disponible = dto.Disponible
            };

            await _repository.AddAsync(consola);

            dto.Id = consola.Id;

            return CreatedAtAction(nameof(GetConsola), new { id = consola.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsola(int id, ConsolaDTOs dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var consola = await _repository.GetByIdAsync(id);

            if (consola == null)
                return NotFound();

            consola.Nombre = dto.Nombre;
            consola.Marca = dto.Marca;
            consola.TarifaPorHora = dto.TarifaPorHora;
            consola.Disponible = dto.Disponible;

            await _repository.UpdateAsync(consola);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsola(int id)
        {
            var consola = await _repository.GetByIdAsync(id);

            if (consola == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<ConsolaDTOs>>> GetConsolasDisponibles()
        {
            var consolas = await _repository.GetAllAsync();

            var resultado = consolas
                .Where(c => c.Disponible)
                .Select(c => new ConsolaDTOs
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