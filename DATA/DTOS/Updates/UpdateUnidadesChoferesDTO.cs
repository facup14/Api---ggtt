using System;

namespace DATA.DTOS.Updates
{
    public class UpdateUnidadesChoferesDTO
    {
        public DateTime? Fecha { get; set; }
        public bool? Actual { get; set; }
        public long? idChofer { get; set; }
        public long? idUnidad { get; set; }
    }
}
