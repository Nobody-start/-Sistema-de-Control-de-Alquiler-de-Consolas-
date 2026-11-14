using SistemaCA.Application.Dtos;

namespace SistemaCA.Application.Dtos.Department.Consola
{
    public class ConsolaDto : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public Decimal TarifaPorHora { get; set; }
        public bool Disponible { get; set; } = true;

    }
}
