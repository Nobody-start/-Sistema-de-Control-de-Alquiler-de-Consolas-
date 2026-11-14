
namespace SistemaCA.Application.Dtos.Alquiler
{
    public class ActualizarAlquilerDto
    {
        public int ConsolaId { get; set; }
        public int ClienteId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }

        public decimal Total { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}
