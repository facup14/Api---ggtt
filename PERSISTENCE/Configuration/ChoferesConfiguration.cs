

using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class ChoferesConfiguration
    {
        public ChoferesConfiguration(EntityTypeBuilder<Choferes> entity)
        {
            entity.HasKey(e => e.IdChofer);

            entity.Property(e => e.IdChofer).HasColumnName("idChofer");

            entity.Property(e => e.AgrupacionSindical).IsUnicode(false);

            entity.Property(e => e.ApellidoyNombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CarnetVence).HasColumnType("datetime");

            entity.Property(e => e.Convenio).IsUnicode(false);

            entity.Property(e => e.Empresa).IsUnicode(false);

            entity.Property(e => e.Especialidad).HasMaxLength(50);

            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

            entity.Property(e => e.Foto).IsUnicode(false);

            entity.Property(e => e.Funcion).IsUnicode(false);

            entity.Property(e => e.Legajo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.NroDocumento).IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);

            entity.Property(e => e.Titulo).HasMaxLength(50);

            entity.HasOne(d => d.IdAgrupacionSindical)
                .WithMany(p=>p.Choferes)
                .HasForeignKey(d => d.idAgrupacionSindical)
                .HasConstraintName("FK_Choferes_AgrupacionesSindicales");

            entity.HasOne(d => d.IdConvenio)
                .WithMany(p => p.Choferes)
                .HasForeignKey(d => d.idConvenio)
                .HasConstraintName("FK_Choferes_Convenios");

            entity.HasOne(d => d.IdEmpresa)
                .WithMany(p => p.Choferes)
                .HasForeignKey(d => d.idEmpresa)
                .HasConstraintName("FK_Choferes_Empresas");

            entity.HasOne(d => d.IdEspecialidad)
                .WithMany(p => p.Choferes)
                .HasForeignKey(d => d.idEspecialidad)
                .HasConstraintName("FK_Choferes_Especialidades");

            entity.HasOne(d => d.IdFuncion)
                .WithMany(p => p.Choferes)
                .HasForeignKey(d => d.idFuncion)
                .HasConstraintName("FK_Choferes_Funciones");

            entity.HasOne(d => d.IdTitulo)
                .WithMany(p => p.Choferes)
                .HasForeignKey(d => d.idTitulo)
                .HasConstraintName("FK_Choferes_Titulos");
            
        }
    }
}
