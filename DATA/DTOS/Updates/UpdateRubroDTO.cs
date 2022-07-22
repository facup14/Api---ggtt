using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace DATA.DTOS.Updates
{
    public class UpdateRubroDTO
    {
        [Required]
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public long IdMecanico { get; set; }
    }
}
