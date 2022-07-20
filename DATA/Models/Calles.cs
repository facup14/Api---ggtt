using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.Models
{
    public class Calles
    {
        public Calles()
        {
            Domicilios = new HashSet<Domicilios>();
        }
        public int IdCalle { get; set; }
        public string Calle { get; set; }
        public string Obs { get; set; }
        public virtual ICollection<Domicilios> Domicilios { get; set; }
    }
}
