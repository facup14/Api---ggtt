using System;
using System.ComponentModel.DataAnnotations;

namespace DATA.DTOS.Updates
{
    public class UpdateMecanicoDTO
    {
        [Required]
        public string ApellidoyNombres { get; set; }
        public string Legajo { get; set; }
        public string Especialidad { get; set; }
        public string Obs { get; set; }
        public string Foto { get; set; }
        public bool? Activo { get; set; }
        public string NroDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Empresa { get; set; }
        public string Funcion { get; set; }
        public string AgrupacionSindical { get; set; }
        public string Convenio { get; set; }
        public decimal? CostoHora { get; set; }
        public decimal? ValorHoraInterno { get; set; }
        public long? IdTaller { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdFuncion { get; set; }
        public int? IdTitulo { get; set; }
        public int? IdEspecialidad { get; set; }
        public int? IdAgrupacionSindical { get; set; }
        public int? IdConvenio { get; set; }
    }
}
