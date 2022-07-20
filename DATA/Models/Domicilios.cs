using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DATA.Models
{
    public class Domicilios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDomicilio { get; set; }
        public bool? Predeterminado { get; set; }
        public int? IdProveedor { get; set; }
        [ForeignKey("IdCalle")]
        public int? IdCalle { get; set; }
        public virtual Calles idCalle { get; set; }
        public string? Numero { get; set; }
        [ForeignKey("IdBarrio")]
        public int? IdBarrio { get; set; }
        public virtual Barrios idBarrio { get; set; }
        public string? Dpto { get; set; }
        public int? IdCliente { get; set; }
        
    }
}
