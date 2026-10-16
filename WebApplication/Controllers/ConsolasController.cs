using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Data;
using WebApplication.DTOs;

namespace WebApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ConsolasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConsolasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Consola
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consola>>> GetConsola()
        {
            var consolas = await _context.Consolas
                .Select(c => new ConsolaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Marca = c.Marca,
                    TarifaPorHora = c.TarifaPorHora,
                    Disponible = c.Disponible

                })
                .ToListAsync();

            return Ok(consolas);
        }

        // GET: api/Consola/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consola>> GetConsola(int id)
        {
            var consola = await _context.Consolas.FindAsync(id);

            if (consola == null)
            {
                return NotFound();
            }

            var consolaDTO = new ConsolaDTO
            {
                Id = consola.Id,
                Nombre = consola.Nombre,
                Marca = consola.Marca,
                TarifaPorHora = consola.TarifaPorHora,
                Disponible = consola.Disponible
            };

            return Ok(consolaDTO);
        }

        // PUT: api/Consola/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsola(int id, ConsolaDTO consolaDTO)
        {
            if (id != consolaDTO.Id)
            {
                return BadRequest();
            }

            var consola = await _context.Consolas.FindAsync(id);

            if (consola == null)
            {
                return NotFound();
            }

            consola.Nombre = consolaDTO.Nombre;
            consola.Marca = consolaDTO.Marca;
            consola.TarifaPorHora = consolaDTO.TarifaPorHora;
            consola.Disponible = consolaDTO.Disponible;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Consola
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsolaDTO>> PostConsola(ConsolaDTO consolaDTO)
        {
            var consola = new Consola
            {
                Nombre = consolaDTO.Nombre,
                Marca = consolaDTO.Marca,
                TarifaPorHora = consolaDTO.TarifaPorHora,
                Disponible = consolaDTO.Disponible
            };

            _context.Consolas.Add(consola);
            await _context.SaveChangesAsync();

            consolaDTO.Id = consola.Id;

            return CreatedAtAction(
                nameof(GetConsola),
                new { id = consola.Id },
                consolaDTO
            );
        }

            // DELETE: api/Consola/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteConsola(int id)

            {
                var consola = await _context.Consolas.FindAsync(id);

                if (consola == null)
                {
                    return NotFound();
                }

                _context.Consolas.Remove(consola);
                await _context.SaveChangesAsync();

                return NoContent();
            }

        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<ConsolaDTO>>> GetConsolasDisponibles()
        {
            var consolas = await _context.Consolas
                .Where(c => c.Disponible)
                .Select(c => new ConsolaDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Marca = c.Marca,
                    TarifaPorHora = c.TarifaPorHora,
                    Disponible = c.Disponible
                })
                .ToListAsync();

            return Ok(consolas);
        }
    }

    }
