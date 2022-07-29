<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
=======
﻿using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)

namespace PERSISTENCE.Configuration
{
    public class ArticulosConfiguration
    {
<<<<<<< HEAD
=======
        public ArticulosConfiguration(EntityTypeBuilder<Articulos> entity)
        {
            entity.HasKey(e => e.IdArticulo);

            entity.Property(e => e.IdArticulo).HasColumnName("idArticulo");

            entity.Property(e => e.CodigoFabrica)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Costo).HasColumnType("money");

            entity.Property(e => e.DetalleArticulo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);
        }
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
    }
}
