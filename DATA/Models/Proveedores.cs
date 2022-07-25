using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Proveedores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProveedor { get; set; }
        [MaxLength(50)]
        public string RazonSocial { get; set; }
        public string IdAlicuota { get; set; }
        public string NCuit { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
        public string ChequesA { get; set; }
        public string Web { get; set; }
        public string Obs { get; set; }
    }
}
