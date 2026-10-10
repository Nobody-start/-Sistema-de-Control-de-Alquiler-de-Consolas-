namespace WebApplication.Models
{
    public class Alquiler
    {
        public int Id { get; set; }

        public int ConsolaId { get; set; }
        public Consola? Consola { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }

        public decimal Total { get; set; }
        public string Estado { get; set; } = "Activo";

        public ICollection<Pago> Pagos { get; set; }
            = new List<Pago>();
    }
}
