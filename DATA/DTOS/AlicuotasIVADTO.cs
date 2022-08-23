
﻿
namespace DATA.DTOS
{
    public class AlicuotasIVADTO
    {
        public int IdAlicuota { get; set; }
        public string Detalle { get; set; }
        public decimal? Alicuota { get; set; }
        public bool? NumeroCUIT { get; set; }
        public decimal AlicuotaRecargo { get; set; }

    }
}