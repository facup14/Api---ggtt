using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class BarriosConfiguration
    {
        public BarriosConfiguration(EntityTypeBuilder<Barrios> entity)
        {
            entity.HasKey(e => e.IdBarrio)
                    .HasName("Barrios_PK")
                    .IsClustered(false);

            entity.HasIndex(e => e.Barrio)
                .HasName("det_Barriounica")
                .IsUnique();

            entity.Property(e => e.Barrio).HasMaxLength(50);

            entity.Property(e => e.Obs).IsUnicode(false);

            entity.HasOne(d => d.idLocalidad)
                .WithMany(p => p.Barrios)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("FK_Barrios_Localidades");
        }
    }
}
