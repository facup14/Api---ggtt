using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PERSISTENCE.Configuration
{
    public class TrazasConfiguration
    {
        public TrazasConfiguration(EntityTypeBuilder<Trazas> entity)
        {
            entity.HasKey(e => e.IdTraza);

                entity.Property(e => e.DistanciaKm).HasColumnName("DistanciaKM");

                entity.Property(e => e.Obs).IsUnicode(false);

                entity.HasOne(d => d.IdLocalidadDesde)
                    .WithMany(p=>p.LocalidadDesde)
                    .HasForeignKey(d => d.idLocalidadDesde)
                    .HasConstraintName("FK_TrazaDesde_Localidades")
                    .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasOne(d => d.IdLocalidadHasta)
                    .WithMany(p=>p.LocalidadHasta)
                    .HasForeignKey(d => d.idLocalidadHasta)
                    .HasConstraintName("FK_TrazaHasta_Localidades")
                    .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
