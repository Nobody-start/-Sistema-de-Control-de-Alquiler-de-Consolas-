

namespace SistemaCA.Application.Dtos.Alquiler
{
    public class CrearAlquilerDto
    {
        public int ConsolaId { get; set; }
        public int ClienteId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaDevolucionPrevista { get; set; }
    }
}
