using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Trabajos
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdTrabajo { get; set; } 
        [MaxLength(50)]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public int TipoTrabajo { get; set; }

        [ForeignKey("IdRubro")]
        public long IdRubro { get; set; }
        public virtual Rubros idRubro { get; set; }

    }
}
