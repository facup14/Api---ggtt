using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class CambiosCentroDeCosto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCambioCentroDeCosto { get; set; }
        public long? IdCcorigen { get; set; }        
        public DateTime? Fecha { get; set; }
        public string Motivo { get; set; }        
        [ForeignKey("idCcdestino")]
        public long? idCcdestino { get; set; }
        public virtual CentroDeCosto IdCcdestino { get; set; }
        [ForeignKey("idUnidad")]
        public long? idUnidad { get; set; }
        public virtual Unidades IdUnidad { get; set; }
    }
}
