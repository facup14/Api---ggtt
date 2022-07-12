using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Choferes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdChofer { get; set; }
        [MaxLength(50)]
        public string ApellidoyNombre { get; set; }
        [MaxLength(20)]
        public string Legajo { get; set; }
        
        public DateTime CarnetVence { get; set; }
        
        public string Obs { get; set; }
        
        public string Foto { get; set; }

        public bool Activo { get; set; }
        public string NroDocumento { get; set; }
        
        public DateTime FechaNacimiento { get; set; }
        
        

        //[ForeignKey("IdTaller")]
        //public int? IdTaller { get; set; }
        //public Talleres Taller { get; set; }
        [ForeignKey("idEmpresa")]
        public string Empresa { get; set; }
        public int idEmpresa { get; set; }
        public virtual Empresas IdEmpresa { get; set; }
        [ForeignKey("idAgrupacionSindical")]
        public string AgrupacionSindical { get; set; }
        public int idAgrupacionSindical { get; set; }
        public virtual AgrupacionesSindicales IdAgrupacionSindical { get; set; }        
        [ForeignKey("idConvenio")]
        public string Convenio { get; set; }
        public int idConvenio { get; set; }
        public virtual Convenios IdConvenio { get; set; }
        [ForeignKey("idFuncion")]
        public string Funcion { get; set; }
        public long idFuncion { get; set; }
        public virtual Funciones IdFuncion { get; set; }
        [ForeignKey("idEspecialidad")]
        public string Especialidad { get; set; }
        public int idEspecialidad { get; set; }
        [MaxLength(50)]
        public virtual Especialidades IdEspecialidad { get; set; }
        [ForeignKey("idTitulo")]
        public string Titulo { get; set; }
        public int idTitulo { get; set; }
        [MaxLength(50)]
        public virtual Titulos IdTitulo { get; set; }
    }
}
