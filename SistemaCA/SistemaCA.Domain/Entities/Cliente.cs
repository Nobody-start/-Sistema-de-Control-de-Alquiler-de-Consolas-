using SistemaCA.Domain.Core;

namespace SistemaCA.Domain.Entities
{
    public class Cliente : BaseEntity
    {
     
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public ICollection<Alquiler> Alquileres { get; set; }
            = new List<Alquiler>();
    }
}
