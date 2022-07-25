using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    [Table("Alicuotas")]
    public class AlicuotasIVA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAlicuota { get; set; }
        [MaxLength(50)]
        public string? Detalle { get; set; }
        public decimal? Alicuota { get; set; }
        public bool? NumeroCUIT { get; set; }
        public decimal? AlicuotaRecargo { get; set; }

    }
}
