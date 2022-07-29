<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
=======
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)

namespace DATA.Models
{
    public class Articulos
    {
<<<<<<< HEAD
=======
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
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
    }
}
