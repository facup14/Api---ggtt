using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class UnidadesChoferes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUnidadChofer { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Actual { get; set; }
        [ForeignKey("idChofer")]
        public long? idChofer { get; set; }
        public virtual Choferes IdChofer { get; set; }
        [ForeignKey("idUnidad")]
        public long? idUnidad { get; set; }
        public virtual Unidades IdUnidad { get; set; }
    }
}
