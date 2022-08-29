using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.DTOS
{
    public class UnidadesChoferesDTO
    {
        public long IdChofer { get; set; }
        public string? apellidoynombres { get; set; }
        public string? legajo { get; set; }
        public DateTime? carnetvence { get; set; }
        public string? obs { get; set; }
        public string? Fecha { get; set; }
        public string? Hasta { get; set; }
        public bool actual { get; set; }
    }
}
