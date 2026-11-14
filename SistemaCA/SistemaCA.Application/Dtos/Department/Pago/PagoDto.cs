using SistemaCA.Application.Dtos;

namespace SistemaCA.Application.Dto.Pago
{
    internal class PagoDto : DtoBase
    {
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
