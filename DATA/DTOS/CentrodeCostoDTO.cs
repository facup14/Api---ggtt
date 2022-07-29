namespace DATA.DTOS
{
    public class CentrodeCostoDTO
    {
        public long idCentrodeCosto { get; set; }
        public string CentrodeCosto { get; set; }
        public string Obs { get; set; }
        public int Tipo { get; set; }
        public int CodigoBas { get; set; }
        public long idEstadoUnidad { get; set; }

    }
}