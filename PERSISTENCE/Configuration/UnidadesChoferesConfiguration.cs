using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class UnidadesChoferesConfiguration
    {
        public UnidadesChoferesConfiguration(EntityTypeBuilder<UnidadesChoferes> entity)
        {
            entity.HasKey(e => e.IdUnidadChofer);

            entity.Property(e => e.IdUnidadChofer).HasColumnName("idUnidadChofer");

            entity.Property(e => e.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("datetime");

            entity.Property(e => e.IdChofer).HasColumnName("idChofer");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");

            entity.HasOne(d => d.IdChofer)
                .WithMany(p => p.UnidadesChoferes)
                .HasForeignKey(d => d.idChofer)
                .HasConstraintName("FK_UnidadesChoferes_Choferes");

            entity.HasOne(d => d.IdUnidad)
                .WithMany(p => p.UnidadesChoferes)
                .HasForeignKey(d => d.idUnidad)
                .HasConstraintName("FK_UnidadesChoferes_Unidades");
        }
    }
}
