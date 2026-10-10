namespace WebApplication.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public int AlquilerId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }
}
