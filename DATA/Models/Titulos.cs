﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Titulos
    {
        public Titulos()
        {
            Choferes = new HashSet<Choferes>();
            Mecanicos = new HashSet<Mecanicos>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTitulo { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public virtual ICollection<Choferes> Choferes { get; set; }
        public virtual ICollection<Mecanicos> Mecanicos { get; set; }
    }
}
