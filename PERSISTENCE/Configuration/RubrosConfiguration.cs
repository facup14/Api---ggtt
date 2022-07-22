
using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class RubrosConfiguration
    {

        public RubrosConfiguration(EntityTypeBuilder<Rubros> entity)
        {
            entity.HasKey(e => e.IdRubro);

            entity.Property(e => e.IdRubro).HasColumnName("idRubro");

            entity.Property(e => e.Descripcion).IsUnicode(false);

            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);



            entity.HasOne(d => d.idMecanico)
                .WithMany(p => p.Rubros)
                .HasForeignKey(d => d.IdMecanico)
                .HasConstraintName("FK_Rubros_Mecanicos");



        }
    }
}
