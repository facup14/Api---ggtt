using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class DomiciliosConfiguration
    {
        public DomiciliosConfiguration(EntityTypeBuilder<Domicilios> entity)
        {
            entity.HasKey(e => e.IdDomicilio);

            entity.Property(e => e.IdDomicilio).HasColumnName("idDomicilio");

            entity.Property(e => e.Dpto).HasMaxLength(50);

            entity.Property(e => e.IdBarrio).HasColumnName("idBarrio");

            entity.Property(e => e.IdCalle).HasColumnName("idCalle");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.idBarrio)
                .WithMany(p => p.Domicilios)
                .HasForeignKey(d => d.IdBarrio)
                .HasConstraintName("FK_Domicilios_Barrios");

            entity.HasOne(d => d.idCalle)
                .WithMany(p => p.Domicilios)
                .HasForeignKey(d => d.IdCalle)
                .HasConstraintName("FK_Domicilios_Calles");

            entity.Property(d => d.IdProveedor).HasColumnName("idProveedor");
        }
    }
}
