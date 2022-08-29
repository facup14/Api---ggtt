using System;

namespace DATA.DTOS
{
    public class CambiosCentroDeCostoDTO
    {
        public long IdCambioCentroDeCosto { get; set; }
        public long? IdCcorigen { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Motivo { get; set; }
        public long? idCcdestino { get; set; }
        public long? idUnidad { get; set; }
        public string CCOrigen { get; set; }
        public string CCDestino { get; set; }
    }
}
