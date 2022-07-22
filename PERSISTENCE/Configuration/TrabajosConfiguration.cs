
using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class TrabajosConfiguration
    {

        public TrabajosConfiguration(EntityTypeBuilder<Trabajos> entity)
        {
            entity.HasKey(e => e.IdTrabajo);

            entity.Property(e => e.IdTrabajo).HasColumnName("idTrabajo");

            entity.Property(e => e.Descripcion).IsUnicode(false);

            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TipoTrabajo).IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);



            entity.HasOne(d => d.idRubro)
                .WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.IdRubro)
                .HasConstraintName("FK_Trabajos_Rubros");
            


        }
    }
}
