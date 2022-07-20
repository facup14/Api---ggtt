using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PERSISTENCE.Configuration
{
    public class TalleresConfiguration
    {
        public TalleresConfiguration(EntityTypeBuilder<Talleres> entity)
        {
            entity.HasKey(e => e.IdTaller);

            entity.HasIndex(e => e.NombreTaller)
                .HasName("det_nombretallerunico")
                .IsUnique();

            entity.Property(e => e.Direccion).IsUnicode(false);

            entity.Property(e => e.FondoPantalla).IsUnicode(false);

            entity.Property(e => e.IdLocalidad).HasColumnName("idLocalidad");

            entity.Property(e => e.JefeAsignado).IsUnicode(false);

            entity.Property(e => e.Mail).IsUnicode(false);

            entity.Property(e => e.NombreEmpresa).IsUnicode(false);

            entity.Property(e => e.NombreProvincia).IsUnicode(false);

            entity.Property(e => e.NombreRecibe)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.NombreTaller)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);

            entity.Property(e => e.PasswordCombustible)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RutaCargaAutomatica)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.RutaIcono).IsUnicode(false);

            entity.Property(e => e.RutaInstalador).IsUnicode(false);

            entity.Property(e => e.RutaLogo).IsUnicode(false);

            entity.Property(e => e.Slogan).IsUnicode(false);

            entity.Property(e => e.Telefonos)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.UserIdCombustible)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.idLocalidad)
                .WithMany(p => p.Talleres)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("FK_Talleres_Localidades");
        }
    }
}
