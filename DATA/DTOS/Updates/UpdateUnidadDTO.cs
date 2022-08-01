using System.ComponentModel.DataAnnotations;


namespace Service.Queries.DTOS
{
    public class UpdateUnidadDTO
    {
        public string? NroUnidad { get; set; }
        [MaxLength(15)]
        public string? Dominio { get; set; }
        public bool? IntExt { get; set; }
        public double? KmAceptadosDesfazados { get; set; }
        public double? HsAceptadasDesfazadas { get; set; }
        [MaxLength(50)]
        public string? Motor { get; set; }
        [MaxLength(50)]
        public string? Chasis { get; set; }
        public long? AñoModelo { get; set; }
        [MaxLength(100)]
        public string? Descripcion { get; set; }
        public string? Foto { get; set; }
        public double? PromedioConsumo { get; set; }
        [MaxLength(50)]
        public string? UnidadMedida { get; set; }
        public int? IdTipoCombustible { get; set; }
        public int? UnidadMedTrabajo { get; set; }
        public double? Capacidad { get; set; }
        public long? IdUnidadDeMedida { get; set; }
        public string? Obs { get; set; }
        [MaxLength(50)]
        public string? Tacografo { get; set; }
        public string? Radicacion { get; set; }
        public string? Titular { get; set; }
        public string? AcreedorPrendario { get; set; }
        public string? MarcaTacografo { get; set; }
        public string? CodigoRadio { get; set; }
        public string? CodigoLlave { get; set; }
        public int? IdModeloChasis { get; set; }
        public int? IdTraza { get; set; }
        public int? IdEsquema { get; set; }
        public bool? TieneConceptosSinRealizar { get; set; }
        public int? IdTipoLlanta { get; set; }
        [MaxLength(500)]
        public string? Potencia { get; set; }
        public double? HsKmsDiaTrabajo { get; set; }
        public double? LtsDiaTrabajo { get; set; }
        public int? LitrosxTraza { get; set; }
        public long? idNombreEquipamiento { get; set; }
        public bool? HabilitaOtraUnidadMedida { get; set; }
        [Required]
        public long idModelo { get; set; }
        [Required]
        public long idEstadoUnidad { get; set; }
        [Required]
        public long idSituacionUnidad { get; set; }
        public string prueba { get; set; }
    }
}
