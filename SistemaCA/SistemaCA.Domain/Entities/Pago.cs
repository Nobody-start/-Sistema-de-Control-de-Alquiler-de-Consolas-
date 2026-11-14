using SistemaCA.Domain.Core;

namespace SistemaCA.Domain.Entities
{
    public class Pago : BaseEntity
    {

        public int AlquilerId { get; set; }
        public Alquiler? Alquiler { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = string.Empty;
    }
}
