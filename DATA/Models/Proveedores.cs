using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Proveedores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedor { get; set; }
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        [ForeignKey("IdAlicuota")]
        public int IdAlicuota { get; set; }
        public virtual AlicuotasIVA idAlicuota { get; set; }
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
