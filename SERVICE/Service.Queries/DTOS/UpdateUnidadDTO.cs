using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Queries.DTOS
{
    public class UpdateUnidadDTO
    {
        public string NroUnidad { get; set; }
        public string Dominio { get; set; }
        public string Motor { get; set; }
        public string Chasis { get; set; }
        public string Titular { get; set; }
        public int idEstadoUnidad { get; set; }
        public int idModelo { get; set; }
        public int idSituacionUnidad { get; set; }
    }
}
