
namespace SistemaCA.Application.Dtos.Pago
{
    public class ActualizarPagoDto
    {
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
