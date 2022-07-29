namespace DATA.DTOS
{
    public class DomiciliosDTO
    {
        public int IdDomicilio { get; set; }
        public bool? Predeterminado { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCalle { get; set; }
        public string? Numero { get; set; }
        public int? IdBarrio { get; set; }
        public string? Dpto { get; set; }
        public int? IdCliente { get; set; }
    }
}
