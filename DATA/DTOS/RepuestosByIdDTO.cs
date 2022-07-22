namespace DATA.DTOS
{
    public class RepuestosByIdDTO
    {
        public long IdRepuesto { get; set; }
        public string? Detalle { get; set; }
        public decimal? Precio { get; set; }
        public string? Tipo { get; set; }
        public string? Marca { get; set; }
        public string? Obs { get; set; }
        public string? CodRepuesto { get; set; }
        public string? NroParte { get; set; }
        public string? NroSerie { get; set; }
        public string? Descripcion { get; set; }
        public bool? Importado { get; set; }
        public int? IdProveedor { get; set; }
        public int? StockMinimo { get; set; }
        public int? StockMaximo { get; set; }
        public bool? Baja { get; set; }
        public byte[]? Imagen { get; set; }
        public string? NombreImagen { get; set; }
        public int? TiempoReposicion { get; set; }
        public string? CodigoBarras { get; set; }
    }
}
