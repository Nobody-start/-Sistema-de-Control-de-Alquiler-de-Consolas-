using Microsoft.AspNetCore.Mvc;
using Sistema.Entities;
using Sistema.Infrastructure.Interfaces;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IAlquilerRepository _alquilerRepository;

        public PagosController(
            IPagoRepository pagoRepository,
            IAlquilerRepository alquilerRepository)
        {
            _pagoRepository = pagoRepository;
            _alquilerRepository = alquilerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDTOs>>> GetPagos()
        {
            var pagos = await _pagoRepository.GetAllAsync();

            var resultado = pagos.Select(p => new PagoDTOs
            {
                Id = p.Id,
                AlquilerId = p.AlquilerId,
                Monto = p.Monto,
                FechaPago = p.FechaPago,
                MetodoPago = p.MetodoPago
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDTOs>> GetPago(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return NotFound();

            var dto = new PagoDTOs
            {
                Id = pago.Id,
                AlquilerId = pago.AlquilerId,
                Monto = pago.Monto,
                FechaPago = pago.FechaPago,
                MetodoPago = pago.MetodoPago
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PagoDTOs>> PostPago(PagoDTOs dto)
        {
            var alquiler = await _alquilerRepository.GetByIdAsync(dto.AlquilerId);

            if (alquiler == null)
                return BadRequest("El alquiler indicado no existe.");

            var pago = new Pago
            {
                AlquilerId = dto.AlquilerId,
                Monto = dto.Monto,
                FechaPago = dto.FechaPago,
                MetodoPago = dto.MetodoPago
            };

            await _pagoRepository.AddAsync(pago);

            dto.Id = pago.Id;

            return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, PagoDTOs dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return NotFound();

            var alquiler = await _alquilerRepository.GetByIdAsync(dto.AlquilerId);

            if (alquiler == null)
                return BadRequest("El alquiler indicado no existe.");

            pago.AlquilerId = dto.AlquilerId;
            pago.Monto = dto.Monto;
            pago.FechaPago = dto.FechaPago;
            pago.MetodoPago = dto.MetodoPago;

            await _pagoRepository.UpdateAsync(pago);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                return NotFound();

            await _pagoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}