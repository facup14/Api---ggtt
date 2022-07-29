using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class CallesConfiguration
    {
        public CallesConfiguration(EntityTypeBuilder<Calles> entity)
        {
            entity.HasKey(e => e.IdCalle)
                    .HasName("Calles_PK")
                    .IsClustered(false);

            entity.HasIndex(e => e.Calle)
                .HasName("det_descripcioncalleunica")
                .IsUnique();

            entity.Property(e => e.Calle).HasMaxLength(50);

            entity.Property(e => e.Obs).IsUnicode(false);
        }
    }
}
