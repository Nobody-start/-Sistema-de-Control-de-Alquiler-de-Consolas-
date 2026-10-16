using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Data;
using WebApplication.DTOs;

[Route("api/[controller]")]
[ApiController]
public class AlquileresController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AlquileresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Alquiler
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlquilerDTO>>> GetAlquileres()
    {
        var alquileres = await _context.Alquileres
            .Select(a => new AlquilerDTO
            {
                Id = a.Id,
                ConsolaId = a.ConsolaId,
                ClienteId = a.ClienteId,
                FechaInicio = a.FechaInicio,
                FechaDevolucionPrevista = a.FechaDevolucionPrevista,
                FechaDevolucionReal = a.FechaDevolucionReal,
                Total = a.Total,
                Estado = a.Estado
            })
            .ToListAsync();

        return Ok(alquileres);
    }

    // GET: api/Alquiler/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AlquilerDTO>> GetAlquiler(int id)
    {
        var alquiler = await _context.Alquileres.FindAsync(id);

        if (alquiler == null)
        {
            return NotFound();
        }

        var alquilerDTO = new AlquilerDTO
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

        return Ok(alquilerDTO);
    }

    // PUT: api/Alquiler/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlquiler(int id, AlquilerDTO alquilerDTO)
    {
        if (id != alquilerDTO.Id)
        {
            return BadRequest();
        }

        var alquiler = await _context.Alquileres.FindAsync(id);

        if (alquiler == null)
        {
            return NotFound();
        }

        alquiler.FechaDevolucionPrevista = alquilerDTO.FechaDevolucionPrevista;
        alquiler.FechaDevolucionReal = alquilerDTO.FechaDevolucionReal;
        alquiler.Total = alquilerDTO.Total;
        alquiler.Estado = alquilerDTO.Estado;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Alquiler
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AlquilerDTO>> PostAlquiler(AlquilerDTO alquilerDTO)
    {
        var consola = await _context.Consolas.FindAsync(alquilerDTO.ConsolaId);

        if (consola == null)
        {
            return BadRequest("La consola indicada no existe.");
        }

        var cliente = await _context.Clientes.FindAsync(alquilerDTO.ClienteId);

        if (cliente == null)
        {
            return BadRequest("El cliente indicado no existe.");
        }

        if (!consola.Disponible)
        {
            return BadRequest("La consola no está disponible.");
        }

        var alquiler = new Alquiler
        {
            ConsolaId = alquilerDTO.ConsolaId,
            ClienteId = alquilerDTO.ClienteId,
            FechaInicio = alquilerDTO.FechaInicio,
            FechaDevolucionPrevista = alquilerDTO.FechaDevolucionPrevista,
            FechaDevolucionReal = alquilerDTO.FechaDevolucionReal,
            Total = alquilerDTO.Total,
            Estado = "Activo"
        };

        consola.Disponible = false;

        _context.Alquileres.Add(alquiler);
        await _context.SaveChangesAsync();

        alquilerDTO.Id = alquiler.Id;
        alquilerDTO.Estado = alquiler.Estado;

        return CreatedAtAction(
            nameof(GetAlquiler),
            new { id = alquiler.Id },
            alquilerDTO
        );
    }

    // DELETE: api/Alquiler/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlquiler(int id)
    {
        var alquiler = await _context.Alquileres.FindAsync(id);

        if (alquiler == null)
        {
            return NotFound();
        }

        _context.Alquileres.Remove(alquiler);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}