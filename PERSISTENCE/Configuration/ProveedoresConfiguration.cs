using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class ProveedoresConfiguration
    {
        public ProveedoresConfiguration(EntityTypeBuilder<Proveedores> entity)
        {
            entity.HasKey(e => e.IdProveedor);

            entity.Property(e => e.IdProveedor)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.IdAlicuota)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.NCuit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Celular)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Contacto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.ChequesA)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Web)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Obs)
                    .HasMaxLength(100)
                    .IsUnicode(false);

        }
    }
}
