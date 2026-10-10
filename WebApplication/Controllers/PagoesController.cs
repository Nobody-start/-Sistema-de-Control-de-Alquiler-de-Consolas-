using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Data;
using WebApplication.DTOs;

[Route("api/[controller]")]
[ApiController]
public class PagosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PagosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Pago
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagos()
    {
        var pagos = await _context.Pagos
            .Select(p => new PagoDTO
            {
                Id = p.Id,
                AlquilerId = p.AlquilerId,
                Monto = p.Monto,
                FechaPago = p.FechaPago,
                MetodoPago = p.MetodoPago
            })
            .ToListAsync();

        return Ok(pagos);
    }

    // GET: api/Pago/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PagoDTO>> GetPago(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);

        if (pago == null)
        {
            return NotFound();
        }

        var pagoDTO = new PagoDTO
        {
            Id = pago.Id,
            AlquilerId = pago.AlquilerId,
            Monto = pago.Monto,
            FechaPago = pago.FechaPago,
            MetodoPago = pago.MetodoPago
        };

        return Ok(pagoDTO);
    }

    // PUT: api/Pago/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPago(int id, PagoDTO pagoDTO)
    {
        if (id != pagoDTO.Id)
        {
            return BadRequest();
        }

        var pago = await _context.Pagos.FindAsync(id);

        if (pago == null)
        {
            return NotFound();
        }

        var alquiler = await _context.Alquileres.FindAsync(pagoDTO.AlquilerId);

        if (alquiler == null)
        {
            return BadRequest("El alquiler indicado no existe.");
        }

        pago.AlquilerId = pagoDTO.AlquilerId;
        pago.Monto = pagoDTO.Monto;
        pago.FechaPago = pagoDTO.FechaPago;
        pago.MetodoPago = pagoDTO.MetodoPago;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Pago
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PagoDTO>> PostPago(PagoDTO pagoDTO)
    {
        var alquiler = await _context.Alquileres.FindAsync(pagoDTO.AlquilerId);

        if (alquiler == null)
        {
            return BadRequest("El alquiler indicado no existe.");
        }

        var pago = new Pago
        {
            AlquilerId = pagoDTO.AlquilerId,
            Monto = pagoDTO.Monto,
            FechaPago = pagoDTO.FechaPago,
            MetodoPago = pagoDTO.MetodoPago
        };

        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        pagoDTO.Id = pago.Id;

        return CreatedAtAction(
            nameof(GetPago),
            new { id = pago.Id },
            pagoDTO
        );
    }

    // DELETE: api/Pago/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePago(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);

        if (pago == null)
        {
            return NotFound();
        }

        _context.Pagos.Remove(pago);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
