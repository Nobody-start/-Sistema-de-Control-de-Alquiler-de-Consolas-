namespace WebApplication.DTOs
{
    public class ConsolaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public decimal TarifaPorHora { get; set; }
        public bool Disponible { get; set; }
    }
}
