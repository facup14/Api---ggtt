using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Tareas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdTarea { get; set; }
        [MaxLength(50)]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
    }
}
