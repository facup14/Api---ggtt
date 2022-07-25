using System;
using System.Collections.Generic;
using System.Text;


namespace DATA.DTOS.Updates
{
    public class UpdateAlicuotasIVADTO
    {
        public string Detalle { get; set; }
        public decimal? Alicuota { get; set; }
        public bool? NumeroCUIT { get; set; }
        public decimal AlicuotaRecargo { get; set; }

    }
}
