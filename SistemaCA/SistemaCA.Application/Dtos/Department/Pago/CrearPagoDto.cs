
namespace SistemaCA.Application.Dtos.Pago
{
    public class CrearPagoDto
    {
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
