using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class MecanicosConfiguration
    {
        public MecanicosConfiguration(EntityTypeBuilder<Mecanicos> entity)
        {
            entity.HasKey(e => e.IdMecanico);

            entity.HasIndex(e => new { e.IdMecanico, e.ApellidoyNombres })
                .HasName("Mecanicos_IdMecanicoApellidoNombres");

            entity.Property(e => e.AgrupacionSindical).IsUnicode(false);

            entity.Property(e => e.ApellidoyNombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Convenio).IsUnicode(false);

            entity.Property(e => e.CostoHora).HasColumnType("money");

            entity.Property(e => e.Empresa).IsUnicode(false);

            entity.Property(e => e.Especialidad)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

            entity.Property(e => e.Foto).IsUnicode(false);

            entity.Property(e => e.Funcion).IsUnicode(false);

            entity.Property(e => e.Legajo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.NroDocumento).IsUnicode(false);

            entity.Property(e => e.Obs).IsUnicode(false);

            entity.Property(e => e.ValorHoraInterno).HasColumnType("money");

            entity.HasOne(d => d.idAgrupacionSindical)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdAgrupacionSindical)
                .HasConstraintName("FK_Mecanicos_AgrupacionesSindicales");

            entity.HasOne(d => d.idConvenio)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdConvenio)
                .HasConstraintName("FK_Mecanicos_Convenios");

            entity.HasOne(d => d.idEmpresa)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK_Mecanicos_Empresas");

            entity.HasOne(d => d.idEspecialidad)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdEspecialidad)
                .HasConstraintName("FK_Mecanicos_Especialidades");

            entity.HasOne(d => d.IdFuncionNavigation)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdFuncion)
                .HasConstraintName("FK_Mecanicos_Funciones");

<<<<<<< HEAD
            //entity.HasOne(d => d.idTaller)
            //    .WithMany(p => p.Mecanicos)
            //    .HasForeignKey(d => d.IdTaller)
            //    .HasConstraintName("FK_Mecanicos_Talleres");
=======
            entity.HasOne(d => d.idTaller)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdTaller)
                .HasConstraintName("FK_Mecanicos_Talleres");
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)

            entity.HasOne(d => d.idTitulo)
                .WithMany(p => p.Mecanicos)
                .HasForeignKey(d => d.IdTitulo)
                .HasConstraintName("FK_Mecanicos_Titulos");
        }
    }
}
