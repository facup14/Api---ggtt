using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Trazas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTraza { get; set; }
        public string Obs { get; set; }
        public int DistanciaKm { get; set; }
        public int Litros { get; set; }
        
        [NotMapped]
        [ForeignKey("idLocalidadDesde")]
        public int idLocalidadDesde  { get; set; }
        public virtual Localidades IdLocalidadDesde { get; set; }
        [NotMapped]
        [ForeignKey("idLocalidadHasta")]
        public int idLocalidadHasta { get; set; }
        public virtual Localidades IdLocalidadHasta { get; set; }
        
    }
}
