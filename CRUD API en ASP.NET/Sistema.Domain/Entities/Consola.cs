using Sistema.Domain.Core;

namespace Sistema.Entities
{
    public class Consola : BaseEntity
    {

        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public decimal TarifaPorHora { get; set; }
        public bool Disponible { get; set; } = true;

        public ICollection<Alquiler> Alquileres { get; set; }
            = new List<Alquiler>();
    }
}
