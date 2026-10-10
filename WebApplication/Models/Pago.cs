namespace WebApplication.Models
{
    public class Pago
    {
        public int Id { get; set; }

        public int AlquilerId { get; set; }
        public Alquiler? Alquiler { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public string MetodoPago { get; set; } = string.Empty;
    }
}
