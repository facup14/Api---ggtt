using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class TareasConfiguration
    {
        public TareasConfiguration(EntityTypeBuilder<Tareas> entity)
        {
            entity.HasKey(e => e.IdTarea);

            entity.Property(e => e.IdTarea)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Obs)
                .HasMaxLength(100)
                .IsUnicode(false);

        }
    }
}
