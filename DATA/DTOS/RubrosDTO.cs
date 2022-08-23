using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.DTOS
{
    public class RubrosDTO
    {
        public long IdRubro { get; set; }
        public string Descripcion { get; set; }
        public string Obs { get; set; }
        public long? IdMecanico{ get; set; }
    }
}
