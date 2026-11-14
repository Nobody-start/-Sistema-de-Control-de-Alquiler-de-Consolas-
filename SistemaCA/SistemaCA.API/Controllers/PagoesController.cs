using Microsoft.AspNetCore.Mvc;
using SistemaCA.API.DTOs;
using SistemaCA.Application.Contract;
using SistemaCA.Application.Dtos.Pago;
using SistemaCA.Domain.Entities;

namespace SistemaCA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        private readonly IAlquilerService _alquilerService;

        public PagosController(
            IPagoService pagoService,
            IAlquilerService alquilerService)
        {
            _pagoService = pagoService;
            _alquilerService = alquilerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDTOs>>> GetPagos()
        {
            var pagos = await _pagoService.GetAllAsync();

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
            var pago = await _pagoService.GetByIdAsync(id);

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
        public async Task<ActionResult<PagoDTOs>> PostPago(CrearPagoDto dto)
        {
            var alquiler = await _alquilerService.GetByIdAsync(dto.AlquilerId);

            if (alquiler == null)
                return BadRequest("El alquiler indicado no existe.");

            var pago = new Pago
            {
                AlquilerId = dto.AlquilerId,
                Monto = dto.Monto,
                MetodoPago = dto.MetodoPago
            };

            var resultado = await _pagoService.CreateAsync(pago);

            var pagoDto = new PagoDTOs
            {
                Id = resultado.Id,
                AlquilerId = resultado.AlquilerId,
                Monto = resultado.Monto,
                FechaPago = resultado.FechaPago,
                MetodoPago = resultado.MetodoPago
            };

            return CreatedAtAction(
                nameof(GetPago),
                new { id = resultado.Id },
                pagoDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(
            int id,
            ActualizarPagoDto dto)
        {
            var alquiler = await _alquilerService.GetByIdAsync(dto.AlquilerId);

            if (alquiler == null)
                return BadRequest("El alquiler indicado no existe.");

            var pago = new Pago
            {
                AlquilerId = dto.AlquilerId,
                Monto = dto.Monto,
                MetodoPago = dto.MetodoPago
            };

            var resultado = await _pagoService.UpdateAsync(id, pago);

            if (resultado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var eliminado = await _pagoService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}