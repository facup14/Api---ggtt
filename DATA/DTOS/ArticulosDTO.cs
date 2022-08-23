namespace DATA.DTOS
{
    public class ArticulosDTO
    {
        public long IdArticulo { get; set; }
        public string DetalleArticulo { get; set; }
        public string CodigoFabrica { get; set; }
        public decimal? Costo { get; set; }
        public string Obs { get; set; }
    }
}
