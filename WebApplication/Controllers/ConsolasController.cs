using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Data;

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
            return await _context.Consolas.ToListAsync();
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

            return consola;
        }

        // PUT: api/Consola/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsola(int id, Consola consola)
        {
            if (id != consola.Id)
            {
                return BadRequest();
            }

            _context.Entry(consola).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsolaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Consola
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consola>> PostConsola(Consola consola)
        {
            _context.Consolas.Add(consola);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsola", new { id = consola.Id }, consola);
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

        private bool ConsolaExists(int id)
        {
            return _context.Consolas.Any(e => e.Id == id);
        }
    }
}