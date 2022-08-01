using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class ProveedoresConfiguration
    {
        public ProveedoresConfiguration(EntityTypeBuilder<Proveedores> entity)
        {
            entity.HasKey(e => e.IdProveedor)
                    .HasName("aaaaaProveedores_PK")
                    .IsClustered(false);

            entity.HasIndex(e => new { e.IdProveedor, e.RazonSocial })
                .HasName("IX_Proveedores_IdProveedorRazonSocial");

            entity.Property(e => e.IdProveedor).HasColumnName("iDProveedor");

            entity.Property(e => e.Celular)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ChequesA).HasMaxLength(250);

            entity.Property(e => e.Contacto)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(250);

            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");

            entity.Property(e => e.IdAlicuota).HasColumnName("idAlicuota");

            entity.Property(e => e.NIb)
                .HasColumnName("N_IB")
                .HasMaxLength(50);

            entity.Property(e => e.Ncuit)
                .HasColumnName("NCuit")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Obs).HasMaxLength(50);

            entity.Property(e => e.RazonSocial)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RingBrutos).HasColumnName("RIngBrutos");

            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Web).HasMaxLength(250);

            entity.HasOne(d => d.idAlicuota)
                .WithMany(p => p.Proveedores)
                .HasForeignKey(d => d.IdAlicuota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedores_Alicuotas");
        }
    }
}
