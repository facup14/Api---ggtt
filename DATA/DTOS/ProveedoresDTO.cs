using System;

namespace DATA.DTOS
{
    public class ProveedoresDTO
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int IdAlicuota { get; set; }
        public string Ncuit { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string ChequesA { get; set; }
        public string Web { get; set; }
        public bool ConvMulti { get; set; }
        public string NIb { get; set; }
        public string Obs { get; set; }
        public bool RingBrutos { get; set; }
        public bool? AutorizaTrabajos3ros { get; set; }
        public int? NroTimbrado { get; set; }
        public DateTime? FechaVencimiento { get; set; }

    }
}
