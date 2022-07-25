using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class ValoresMediciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdValorMedicion { get; set; }
        [MaxLength(50)]
        public string ValorMedicion { get; set; }
        public string Obs { get; set; }

    }
}
