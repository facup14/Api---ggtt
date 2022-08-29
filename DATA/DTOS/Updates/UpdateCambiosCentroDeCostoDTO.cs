using System;

namespace DATA.DTOS.Updates
{
    public class UpdateCambiosCentroDeCostoDTO
    {
        public long? IdCcorigen { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Motivo { get; set; }
        public long? idCcdestino { get; set; }
        public long? idUnidad { get; set; }
    }
}
