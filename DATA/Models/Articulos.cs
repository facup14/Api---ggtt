using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Models
{
    public class Articulos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdArticulo { get; set; }
        [Required]
        [MaxLength(50)]
        public string DetalleArticulo { get; set; }
        [MaxLength(50)]
        public string? CodigoFabrica { get; set; }
        public decimal? Costo { get; set; }
        public string? Obs { get; set; }
    }
}
