using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
=======
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Mecanicos
    {
<<<<<<< HEAD
        public Mecanicos()
        {
            Rubros = new HashSet<Rubros>();
        }
=======
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
        public long IdMecanico { get; set; }
        public string? ApellidoyNombres { get; set; }
        public string? Legajo { get; set; }
        public string? Especialidad { get; set; }
        public string? Obs { get; set; }
        public string? Foto { get; set; }
        public bool? Activo { get; set; }
        public string? NroDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Empresa { get; set; }
        public string? Funcion { get; set; }
        public string? AgrupacionSindical { get; set; }
        public string? Convenio { get; set; }
        public decimal? CostoHora { get; set; }
        public decimal? ValorHoraInterno { get; set; }
<<<<<<< HEAD
        //[ForeignKey("IdTaller")]
        public long? IdTaller { get; set; }
        //public virtual Talleres idTaller { get; set; }
=======
        [ForeignKey("IdTaller")]
        public long? IdTaller { get; set; }
        public virtual Talleres idTaller { get; set; }
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
        [ForeignKey("IdEmpresa")]
        public int? IdEmpresa { get; set; }
        public virtual Empresas idEmpresa { get; set; }
        [ForeignKey("IdFuncion")]
        public int? IdFuncion { get; set; }
        public virtual Funciones IdFuncionNavigation { get; set; }
        [ForeignKey("IdTitulo")]
        public int? IdTitulo { get; set; }
        public virtual Titulos idTitulo { get; set; }
        [ForeignKey("IdEspecialidad")]
        public int? IdEspecialidad { get; set; }
        public virtual Especialidades idEspecialidad { get; set; }        
        [ForeignKey("IdAgrupacionSindical")]
        public int? IdAgrupacionSindical { get; set; }
        public virtual AgrupacionesSindicales idAgrupacionSindical { get; set; }
        [ForeignKey("IdConvenio")]
        public int? IdConvenio { get; set; }        
        public virtual Convenios idConvenio { get; set; }
<<<<<<< HEAD

        public virtual ICollection<Rubros> Rubros { get; set; }


=======
        
        
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
    }
}
