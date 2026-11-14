using SistemaCA.Application.Dtos;

namespace SistemaCA.Application.Dtos.Department.Clientes
{
    public class ClienteDto : DtoBase
    {
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
