
namespace SistemaCA.Application.Dtos.Department.Consola
{
    public class CrearConsolaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public Decimal TarifaPorHora { get; set; }
        public bool Disponible { get; set; } = true;
    }

}
