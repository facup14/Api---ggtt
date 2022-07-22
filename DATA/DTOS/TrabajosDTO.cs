using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.DTOS
{
    public class TrabajosDTO
    {
        public long IdTrabajo { get; set; }
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public int TipoTrabajo { get; set; }
        public int IdRubro { get; set; }
    }
}
