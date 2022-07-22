using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace DATA.DTOS.Updates
{
    public class UpdateTrabajoDTO
    {
        [Required]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public int TipoTrabajo { get; set; }
        public long IdRubro { get; set; }
    }
}
