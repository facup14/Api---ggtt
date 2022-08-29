using DATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PERSISTENCE.Configuration
{
    public class CambiosCentroDeCostoConfiguration
    {
        public CambiosCentroDeCostoConfiguration(EntityTypeBuilder<CambiosCentroDeCosto> entity)
        {
            entity.HasKey(e => e.IdCambioCentroDeCosto);

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.Property(e => e.IdCcdestino).HasColumnName("IdCCDestino");

            entity.Property(e => e.IdCcorigen).HasColumnName("IdCCOrigen");

            entity.Property(e => e.Motivo).IsUnicode(false);

            entity.HasOne(d => d.IdCcdestino)
                .WithMany(p => p.CambiosCentroDeCosto)
                .HasForeignKey(d => d.IdCcdestino)
                .HasConstraintName("FK_CambiosCentroDeCosto_CentrodeCosto1");

            entity.HasOne(d => d.IdUnidad)
                .WithMany(p => p.CambiosCentroDeCosto)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("FK_CambiosCentroDeCosto_Unidades");
        }
    }
}
