using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DATA.Models
{
    public class Talleres
    {
        public Talleres()
        {
            Choferes = new HashSet<Choferes>();
        }

        public long IdTaller { get; set; }
        public string NombreTaller { get; set; }
        public string Direccion { get; set; }        
        public string Mail { get; set; }
        public string JefeAsignado { get; set; }
        public string Obs { get; set; }
        public string Telefonos { get; set; }
        public string NombreRecibe { get; set; }
        public string RutaLogo { get; set; }
        public string RutaInstalador { get; set; }
        public int? IdLugarAbastecimiento { get; set; }
        public string RutaIcono { get; set; }
        public string FondoPantalla { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreProvincia { get; set; }
        public string Slogan { get; set; }
        public int? IdRecibidoPor { get; set; }
        public int? IdSolicitadoPor { get; set; }
        public bool? RecibePersona { get; set; }
        public bool? SolicitaPersona { get; set; }
        public bool? CargaAutomaticaCombustible { get; set; }
        public string RutaCargaAutomatica { get; set; }
        public string UserIdCombustible { get; set; }
        public string PasswordCombustible { get; set; }
        [ForeignKey("IdLocalidad")]
        public long? IdLocalidad { get; set; }
        public virtual Localidades idLocalidad { get; set; }
        public virtual ICollection<Choferes> Choferes { get; set; }
    }
}
