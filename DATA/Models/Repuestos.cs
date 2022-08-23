

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace DATA.Models
{
    public class Repuestos
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRepuesto { get; set; }
        public string Detalle { get; set; }
        public decimal? Precio { get; set; }
        public string UnidadMedida { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Obs { get; set; }
        public string CodRepuesto { get; set; }
        public long? IdMarcaRepuesto { get; set; }
        [ForeignKey("IdUnidadMedida")]
        public long? IdUnidadDeMedida { get; set; }
        public virtual UnidadesDeMedida idUnidadDeMedida { get; set; }
        public string NroParte { get; set; }
        public string NroSerie { get; set; }
        public string Descripcion { get; set; }
        public bool? Importado { get; set; }
        public int? IdSubRubroRepuesto { get; set; }
        public int? IdProveedor { get; set; }
        public int? StockMinimo { get; set; }
        public int? StockMaximo { get; set; }
        public int? PuntoPedido { get; set; }
        public bool? Baja { get; set; }
        public decimal? PorcentajeGananciaAplicada { get; set; }
        public int? IdCodigo { get; set; }
        public int? IdRepuestoCwa { get; set; }
        public string CodCwa { get; set; }
        public string TipoCwa { get; set; }
        public byte[] Imagen { get; set; }
        public string NombreImagen { get; set; }
        public string CodArticuloTango { get; set; }
        public long? TiempoReposicion { get; set; }
        public bool? HabilitarEmailStock { get; set; }
        public string CodigoBarras { get; set; }
        public int? TipoValorStock { get; set; }

    }
}
