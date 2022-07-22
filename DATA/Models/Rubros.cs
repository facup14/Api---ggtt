using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DATA.Models
{
    public class Rubros
    {
        public Rubros()
        {
            Trabajos = new HashSet<Trabajos>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRubro { get; set; }
        [MaxLength(50)]
        public string Descripcion { get; set; }
        public string Obs { get; set; }

        public virtual ICollection<Trabajos> Trabajos { get; set; }

        [ForeignKey("IdMecanico")]
        public long? IdMecanico { get; set; }
        public virtual Mecanicos idMecanico { get; set; }
    }
}
