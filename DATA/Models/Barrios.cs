using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Barrios
    {
        public Barrios()
        {
            Domicilios = new HashSet<Domicilios>();
        }
        public int IdBarrio { get; set; }
        public string Barrio { get; set; }
        public string Obs { get; set; }
        [ForeignKey("IdLocalidad")]
        public long IdLocalidad { get; set; }
        public virtual Localidades idLocalidad { get; set; }
        public virtual ICollection<Domicilios> Domicilios { get; set; }
    }
}
