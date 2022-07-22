using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class UnidadesDeMedida
    {
        public UnidadesDeMedida()
        {
            Repuestos = new HashSet<Repuestos>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUnidadDeMedida { get; set; }
        [MaxLength(50)]
        public string? UnidadDeMedida { get; set; }
        public virtual ICollection<Repuestos> Repuestos { get; set; }
        
    }
}
